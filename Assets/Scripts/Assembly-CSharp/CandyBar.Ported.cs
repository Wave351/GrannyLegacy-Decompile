using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CandyBar.c (Granny Legacy, IL2CPP x64).
public partial class CandyBar
{
	public CandyBar() { patrolling = true; }
	// Walking candy bar: wanders the NavMesh; when it sees the player it shoots a love letter, then turns to
	// face them until it goes back to patrolling. BarfScreenOccur shows the heart overlay for 9 s.
	private void Start() { RandomWaypoint(); }
	private void Update()
	{
	    if (patrolling && !agent.isStopped && agent.remainingDistance < 1.6f) RandomWaypoint();
	    if (!patrolling)
	    {
	        if (loved)
	        {
	            Vector3 dir = player.transform.position - transform.position;
	            dir.y = 0;
	            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15f);
	        }
	    }
	    else
	    {
	        anim.CrossFade("CandyBar_Travel");
	        rayPoint.LookAt(player);
	        RaycastHit hit;
	        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, 10f, layers)
	            && hit.collider.gameObject.tag == "Player" && !fadeHeart && ableToAttack)
	        {
	            ableToAttack = false;
	            loved = true;
	            patrolling = false;
	            anim.CrossFade("CandyBar_Shoot");
	            StartCoroutine(TimerBackIdle());
	            agent.enabled = false;
	        }
	        if (agent.velocity == Vector3.zero && ableToAttack) RandomWaypoint();
	    }
	    if (fadeHeart) { if (valueHeartAlpha < 1f) valueHeartAlpha += Time.deltaTime * 2.5f; }
	    else if (valueHeartAlpha > 0f) valueHeartAlpha -= Time.deltaTime * 0.3f;
	    heartScreen.color = new Color(1, 1, 1, valueHeartAlpha);
	}
	public void RandomWaypoint()
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * 15f, out hit, 15f, NavMesh.AllAreas);
	    agent.SetDestination(hit.position);
	}
	public Vector3 PointSelect(float radius)
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * radius, out hit, radius, NavMesh.AllAreas);
	    return hit.position;
	}
	private IEnumerator BarfEffectStuff() { yield return new WaitForSeconds(0.4f); }
	private IEnumerator TimerBackIdle()
	{
	    yield return new WaitForSeconds(0.6f);
	    yield return new WaitForSeconds(0.3f);
	    patrolling = true;
	    loved = false;
	    agent.enabled = true;
	}
	public void BarfScreenOccur() { StartCoroutine(TimerBarfScreen()); }
	private IEnumerator TimerBarfScreen()
	{
	    fadeHeart = true;
	    yield return new WaitForSeconds(9f);
	    fadeHeart = false;
	}
}
