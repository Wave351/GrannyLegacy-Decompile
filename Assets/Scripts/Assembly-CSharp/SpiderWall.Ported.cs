using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpiderWall.c (Granny Legacy, IL2CPP x64).
public partial class SpiderWall
{
	public SpiderWall() { WalkSpeed = 2; RotationSpeed = 8; IdleTimeMin = 1; IdleTimeMax = 3; }
	private void Start() { DecideOnrandomPoint(); }
	// A small spider wandering between points on a wall, pausing for a random while at each.
	private void Update()
	{
	    if (!isWalking) { IdleStatus(); return; }
	    if (Anim == null) return;
	    Anim.CrossFade("spiderRun");
	    FootStepAudio.SetActive(true);
	    transform.position = Vector3.MoveTowards(transform.position, CurrentPoint.position, Time.deltaTime * WalkSpeed);
	    Vector3 dir = CurrentPoint.position - transform.position;
	    if (dir.sqrMagnitude > .001f)
	        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, transform.up), Time.deltaTime * RotationSpeed);
	    if (Vector3.Distance(transform.position, CurrentPoint.position) >= .01f) return;
	    isWalking = false;
	    idleTimer = 0;
	    idleTargetTime = UnityEngine.Random.Range(IdleTimeMin, IdleTimeMax);
	}
	public void IdleStatus()
	{
	    Anim.CrossFade("spiderIdle");
	    FootStepAudio.SetActive(false);
	    idleTimer += Time.deltaTime;
	    if (idleTargetTime <= idleTimer) DecideOnrandomPoint();
	}
	public void DecideOnrandomPoint()
	{
	    CurrentPoint = TravelingPoints[UnityEngine.Random.Range(0, TravelingPoints.Length)];
	    isWalking = true;
	}
	public void DeathInitiates()
	{
	    DeadBodySpider.SetActive(true);
	    DeadBodySpider.transform.parent = null;
	    gameObject.SetActive(false);
	}
}
