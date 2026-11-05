using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Pumpkin.c (Granny Legacy, IL2CPP x64).
public partial class Pumpkin
{
	public Pumpkin() { patrolling = true; }
	private void Start() { RandomWaypoint(); }
	// Halloween pumpkin: wanders the NavMesh until something bumps it, then gets launched like a ball.
	private void Update()
	{
	    if (!agent.isOnNavMesh || !patrolling) return;
	    if (!agent.isStopped && agent.remainingDistance < 1.6f) RandomWaypoint();
	    if (patrolling && !animPumpkin.IsPlaying("WalkPumpkin")) animPumpkin.CrossFade("WalkPumpkin");
	}
	public void RandomWaypoint() { agent.SetDestination(PointSelect(25f)); }
	public Vector3 PointSelect(float radius)
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * radius, out hit, radius, NavMesh.AllAreas);
	    return hit.position;
	}
	// Becomes a physics item and flies off in a random upward direction.
	public void TriggerLaunch()
	{
	    if (launched) return;
	    patrolling = false; launched = true; hitCount = 0;
	    agent.enabled = false;
	    gameObject.layer = LayerMask.NameToLayer("Item");
	    var col = GetComponent<Collider>(); if (!col) return;
	    col.isTrigger = false;
	    var body = GetComponent<Rigidbody>(); if (!body) return;
	    body.isKinematic = false;
	    Vector3 dir = UnityEngine.Random.onUnitSphere;
	    dir.y = Mathf.Clamp(dir.y, .2f, .6f);
	    body.AddForce(dir * forceLaunch, ForceMode.VelocityChange);
	    animPumpkin.Play("PumpkinIdleLaunch");
	    var audio = GetComponent<AudioSource>(); if (!audio) return;
	    audio.Play();
	    AudioSource.PlayClipAtPoint(fartClip, transform.position);
	}
	private void OnTriggerEnter(Collider other)
	{
	    var o = other.gameObject;
	    if (o.CompareTag("Player") || o.CompareTag("granny") || o.CompareTag("granpa") || o.layer == LayerMask.NameToLayer("Item"))
	        TriggerLaunch();
	}
	private void OnCollisionEnter(Collision other)
	{
	    if (!launched) return;
	    hitCount += 1;
	    if (hitCount >= hitsNeeded) StartCoroutine(RecoverAndStandUp());
	}
	// Brakes to a stop over 0.4 s, then goes back to walking on the NavMesh.
	private IEnumerator RecoverAndStandUp()
	{
	    var body = GetComponent<Rigidbody>();
	    Vector3 startVel = body.linearVelocity;
	    float t = 0;
	    while (t < .4f)
	    {
	        body.linearVelocity = Vector3.Lerp(startVel, Vector3.zero, Mathf.Clamp01(t / .4f));
	        yield return null;
	        t += Time.deltaTime;
	    }
	    body.linearVelocity = Vector3.zero;
	    body.isKinematic = true;
	    agent.enabled = true;
	    yield return new WaitForSeconds(.01f);
	    GetComponent<Collider>().isTrigger = true;
	    gameObject.layer = LayerMask.NameToLayer("Default");
	    patrolling = true;
	    RandomWaypoint();
	}
}
