using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Animations;

// Ported from Annotated-Pseudocode/Dart.c (Granny Legacy, IL2CPP x64).
public partial class Dart
{
	public Dart() { BounceForce = 3; }
	private void Start() { EC = GameObject.Find("EnemyController"); StartCoroutine(TimerStop()); }
	// Darts knock out Granny and Grandpa, blind the crow and knock out Santa.
	private void OnTriggerEnter(Collider other)
	{
	    if (DidTheJob) return;
	    var ec = EC.GetComponent<EnemyController>();
	    string tag = other.gameObject.tag;
	    if (tag == "granny")
	    {
	        var ai = ec.Granny.GetComponent<AI_Granny>();
	        if (!ai.CaughtPlayer && !ai.IsDying && !ec.GrannyKilled) { DidTheJob = true; ai.ShootDart(); Stick(other); }
	    }
	    if (tag == "granpa")
	    {
	        var ai = ec.Grandpa.GetComponent<AI_Grandpa>();
	        if (!ai.CaughtPlayer && !ai.IsDying && !ec.GrandpaKilled) { DidTheJob = true; ai.ShootDart(); Stick(other); }
	    }
	    if (other.gameObject.name == "CrowNest")
	    {
	        var crow = other.GetComponentInParent<CrowManage>();
	        if (!crow.HurtTemp) crow.HurtBird();
	    }
	    if (tag == "Santa")
	    {
	        var lf = GameObject.Find("LittleSanta").GetComponent<LittleFucker>();
	        if (!lf.Hurt) lf.KnockedOut();
	    }
	}
	private void Stick(Collider target)
	{
	    GetComponent<Rigidbody>().isKinematic = true;
	    transform.SetParent(target.transform, true);
	    StartCoroutine(TimerGo());
	}
	private void OnCollisionEnter(Collision other)
	{
	    string name = other.gameObject.name;
	    if (name == "WallBumpShelf" && !DidBounce && !DidTheJob)
	    {
	        // Bounces off the shelf to the side.
	        DidBounce = true;
	        Vector3 dir = (-transform.right * .4f + transform.forward * BounceForce + transform.up * .3f).normalized;
	        GetComponent<Rigidbody>().AddForce(dir * BounceForce, ForceMode.VelocityChange);
	    }
	    if (name == "RemoteControlRat") other.gameObject.GetComponent<RemoteRat>().Impact();
	    if (name == "SpiderWall") other.gameObject.GetComponent<SpiderWall>().DeathInitiates();
	    if (!EnergyFilled) return;
	    Instantiate(ElectricBomb, transform.position, transform.rotation);
	    EnergyFilled = false;
	}
	private IEnumerator TimerStop() { yield return new WaitForSeconds(4); DidTheJob = true; }
	private IEnumerator TimerGo()
	{
	    yield return new WaitForSeconds(1.5f);
	    transform.parent = null;
	    var body = GetComponent<Rigidbody>();
	    if (body) body.isKinematic = false;
	}
	public void InitiateDrop()
	{
	    transform.parent = null;
	    var body = GetComponent<Rigidbody>();
	    if (body) body.isKinematic = false;
	}
}
