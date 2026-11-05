using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpectralGranny.c (Granny Legacy, IL2CPP x64).
public partial class SpectralGranny
{
	public SpectralGranny() { WanderRadius = 25; WanderSpeed = 3; LungeSpeed = 12; LungeRadius = 10; LungeTimerMax = 10; TimerScream = .35f; IntroMoveSpeed = 1; }
	// The spectral Granny rises out of the floor, wanders slowly, and every LungeTimerMax seconds screams and lunges.
	private IEnumerator Start()
	{
	    Agent.enabled = false;
	    Vector3 start = transform.localPosition, end = start;
	    end.z = -44.4f;
	    float t = 0;
	    while (t < 1)
	    {
	        t += Time.deltaTime * IntroMoveSpeed;
	        transform.localPosition = Vector3.Lerp(start, end, Mathf.Clamp01(t));
	        yield return null;
	    }
	    if (!Agent) yield break;
	    Agent.enabled = true;
	    Agent.speed = WanderSpeed;
	    ChooseWaypoint();
	}
	private void Update()
	{
	    if (!Agent.enabled) return;
	    LungeTimer += Time.deltaTime;
	    if (!IsLunging)
	    {
	        Anim["SpectralGranny_Wander"].speed = .2f;
	        Anim.CrossFade("SpectralGranny_Wander");
	        CanKill = false;
	        if (LungeTimerMax <= LungeTimer) { StartLunge(); return; }
	        if (!Agent.pathPending && Agent.remainingDistance <= 1.5f) ChooseWaypoint();
	        return;
	    }
	    if (Agent.pathPending || 1 < Agent.remainingDistance) return;
	    IsLunging = false;
	    Agent.speed = WanderSpeed;
	    ChooseWaypoint();
	}
	private void OnTriggerStay(Collider other)
	{
	    if (!CanKill) return;
	    string tag = other.gameObject.tag;
	    if (tag == "granny" && !EC.GrannyKilled) { EC.GrannyDeathS(); CanKill = false; }
	    if (tag == "granpa" && !EC.GrandpaKilled) { EC.GrandpaDeathS(); CanKill = false; }
	    if (tag != "Player") return;
	    var status = PS ? PS.GetComponent<PlayerStatus>() : null;
	    if (status) { status.NormalDeath(); CanKill = false; }
	}
	public void ChooseWaypoint()
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * WanderRadius, out hit, WanderRadius, NavMesh.AllAreas);
	    Agent.SetDestination(hit.position);
	}
	public void StartLunge()
	{
	    Anim.CrossFade("SpectralGranny_Forward");
	    LungeTimer = 0;
	    IsLunging = true;
	    StartCoroutine(SpectralNoiseLunge());
	}
	private Vector3 LungePoint()
	{
	    Vector3 dir = (UnityEngine.Random.insideUnitSphere + transform.forward * 2).normalized;
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + dir * LungeRadius * 1.8f, out hit, LungeRadius + LungeRadius, NavMesh.AllAreas);
	    return hit.position;
	}
	// The lunge goes forward, or (5% of the time, when Granny is out) straight at Granny.
	private IEnumerator SpectralNoiseLunge()
	{
	    yield return new WaitForSeconds(TimerScream);
	    if (!ScreamSpec) yield break;
	    ScreamSpec.Play();
	    Agent.speed = LungeSpeed;
	    Vector3 target = UnityEngine.Random.Range(0f, 100f) < 5 && GrannyP.activeSelf ? GrannyPos.transform.position : LungePoint();
	    Agent.SetDestination(target);
	    CanKill = true;
	}
}
