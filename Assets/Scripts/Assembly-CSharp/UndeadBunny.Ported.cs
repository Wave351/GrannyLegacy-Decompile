using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/UndeadBunny.c (Granny Legacy, IL2CPP x64).
public partial class UndeadBunny
{
	public UndeadBunny() { timerMaxIdle = 4.5f; changeEgg = 20f; }
	// Easter bunny: hops between random NavMesh points; after an idle pause it hops again.
	private void Update()
	{
	    if (!idle) return;
	    timerIdle += Time.deltaTime;
	    if (timerIdle >= timerMaxIdle) { timerIdle = 0; idle = false; anim.CrossFade("HopBunny"); }
	}
	// Picks a point within 15 m; changeEgg% of the time also lays a copy of the egg with a fart.
	public void RandomWaypoint()
	{
	    agent.SetDestination(PointSelect(15f));
	    agent.isStopped = false;
	    if (UnityEngine.Random.Range(0, 100) >= changeEgg) return;
	    var spawned = Instantiate(egg, egg.transform.position, egg.transform.rotation);
	    spawned.name = "eggSpawned";
	    egg.transform.localScale = scale;
	    spawned.SetActive(true);
	    AudioSource.PlayClipAtPoint(fart, transform.position);
	    var bump = egg.GetComponent<EasterEgg>();
	    if (bump) bump.StartEggBumping();
	}
	public void StopTravel() { agent.isStopped = true; idle = true; anim.CrossFade("IdleBunny"); }
	public Vector3 PointSelect(float radius)
	{
	    NavMeshHit hit;
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * radius, out hit, radius, NavMesh.AllAreas);
	    return hit.position;
	}
}
