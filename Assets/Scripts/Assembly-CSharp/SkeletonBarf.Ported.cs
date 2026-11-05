using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SkeletonBarf.c (Granny Legacy, IL2CPP x64).
public partial class SkeletonBarf
{
	private void Start() { RandomWaypoint(); }
	// Halloween skeleton: wanders; when it sees the player within 5.6 m it stops, turns to them and vomits.
	private void Update()
	{
	    if (patrolling && !agent.isStopped && agent.remainingDistance < 1.6f) RandomWaypoint();
	    if (!patrolling)
	    {
	        if (barfing)
	        {
	            Vector3 delta = player.position - transform.position; delta.y = 0;
	            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(delta), Time.deltaTime * 15f);
	        }
	    }
	    else
	    {
	        anim.CrossFade("BarfSkeletonIdle");
	        rayPoint.LookAt(player);
	        RaycastHit hit;
	        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, 5.6f, layers)
	            && hit.collider.gameObject.CompareTag("Player") && !fadeBarf && !barfing)
	        {
	            patrolling = false; barfing = true;
	            barfEffect.Play();
	            anim["BarfSkeletonBarfing"].speed = 1.5f;
	            anim.CrossFade("BarfSkeletonBarfing");
	            barfEffect.GetComponent<AudioSource>().PlayOneShot(barfClip);
	            StartCoroutine(TimerBackIdle());
	            StartCoroutine(BarfEffectStuff());
	            agent.enabled = false;
	        }
	        if (agent.velocity.sqrMagnitude < 1e-10f && !barfing) RandomWaypoint();
	    }
	    if (fadeBarf) { if (valueBarfAlpha < 1f) valueBarfAlpha += Time.deltaTime * 2.5f; }
	    else if (valueBarfAlpha > 0f) valueBarfAlpha -= Time.deltaTime * .3f;
	    barfScreen.color = new Color(1, 1, 1, valueBarfAlpha);
	}
	public void RandomWaypoint() { agent.SetDestination(PointSelect(15f)); }
	public Vector3 PointSelect(float radius)
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * radius, out hit, radius, NavMesh.AllAreas);
	    return hit.position;
	}
	// The vomit hit-collider switches on 0.4 s into the barf.
	private IEnumerator BarfEffectStuff() { yield return new WaitForSeconds(.4f); collider.enabled = true; }
	// Note: the original never clears "barfing", so each skeleton barfs once and then only wanders.
	private IEnumerator TimerBackIdle()
	{
	    yield return new WaitForSeconds(.7f);
	    patrolling = true;
	    collider.enabled = false;
	    barfEffect.Stop();
	    agent.enabled = true;
	}
	// Called when the vomit hits the player: splash sound and a screen overlay that fades in, holds 9 s, then fades out.
	public void BarfScreenOccur()
	{
	    StartCoroutine(TimerBarfScreen());
	    barfEffect.Stop();
	    var audio = barfEffect.GetComponent<AudioSource>();
	    if (audio) audio.PlayOneShot(splashBarf);
	}
	private IEnumerator TimerBarfScreen() { fadeBarf = true; yield return new WaitForSeconds(9f); fadeBarf = false; }
}
