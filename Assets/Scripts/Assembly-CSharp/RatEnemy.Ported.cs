using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RatEnemy.c (Granny Legacy, IL2CPP x64).
public partial class RatEnemy
{
	public RatEnemy() { CanHit = true; RotMax = 3; }
	private void Start() { WaitingTime = 120; }
	// The rat waits, then runs to a random nearby point; it bites the player (hurt and a noise) and squeaks when an enemy steps on it.
	private void Update()
	{
	    if (WaitStatus)
	    {
	        WaitingTime += Time.deltaTime;
	        Agent.speed = 0;
	        if (WaitingTimer <= WaitingTime)
	        {
	            NavMeshHit hit;
	            Vector3 random = transform.position + UnityEngine.Random.insideUnitSphere * Radius;
	            if (NavMesh.SamplePosition(random, out hit, Radius, NavMesh.AllAreas) && 1 < Vector3.Distance(hit.position, transform.position))
	            {
	                Agent.SetDestination(hit.position);
	                Agent.speed = 5;
	                WaitStatus = false; HasToTravel = true;
	                WaitingTime = 0;
	            }
	        }
	    }
	    if (!HasToTravel)
	    {
	        if (!WaitStatus) { GetComponent<BoxCollider>().enabled = true; WaitStatus = true; }
	    }
	    else if (Agent.stoppingDistance < Agent.remainingDistance && .01f < Agent.velocity.sqrMagnitude)
	    {
	        GetComponent<BoxCollider>().enabled = false;
	    }
	    else if (!Agent.pathPending && Agent.remainingDistance <= Agent.stoppingDistance) { WaitStatus = true; HasToTravel = false; }
	    Anim.Play(Agent.velocity == Vector3.zero ? "Idle" : "Run");
	    if (Agent.isOnNavMesh) AlignToGround();
	}
	public void OnTriggerEnter(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if (tag == "Player" && CanHit)
	    {
	        ScreenEffects.FanHurt();
	        Instantiate(NoiseOBJ, transform.position, transform.rotation);
	        WaitingTime = 120;
	        GetComponent<AudioSource>().PlayOneShot(RatSound);
	        CanHit = false;
	        StartCoroutine(ResetCanHit());
	    }
	    if (tag != "granny" && tag != "granpa") return;
	    WaitingTime = 100;
	    var audio = GetComponent<AudioSource>();
	    if (audio) audio.PlayOneShot(RatDie);
	}
	private IEnumerator ResetCanHit() { yield return new WaitForSeconds(2); CanHit = true; }
	public void AlignToGround()
	{
	    RaycastHit hit;
	    if (!Physics.Raycast(Target.position + Vector3.up * .5f, Vector3.down, out hit, 2, Layer)) return;
	    Vector3 e = (Quaternion.FromToRotation(Target.up, hit.normal) * Target.rotation).eulerAngles;
	    Target.rotation = Quaternion.Lerp(Target.rotation, Quaternion.Euler(e.x, Target.eulerAngles.y, e.z), Time.deltaTime * RotMax);
	}
}
