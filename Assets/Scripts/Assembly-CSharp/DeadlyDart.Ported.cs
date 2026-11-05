using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/DeadlyDart.c (Granny Legacy, IL2CPP x64).
public partial class DeadlyDart
{
	// Grandpa's darts: knock Grandpa out when thrown back at him, kill the player, and knock out Santa.
	private void Start()
	{
	    EC = GameObject.Find("EnemyController");
	    PlayerStatus = GameObject.Find("PlayerManage");
	    StartCoroutine(TimerStop());
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (DidTheJob) return;
	    string tag = other.gameObject.tag;
	    if (tag == "granpa")
	    {
	        var ec = EC.GetComponent<EnemyController>();
	        var ai = ec.Grandpa.GetComponent<AI_Grandpa>();
	        if (!ai.CaughtPlayer && !ai.IsDying && !ec.GrandpaKilled)
	        {
	            DidTheJob = true;
	            ai.ShootDart();
	            DartObj.GetComponent<Rigidbody>().isKinematic = true;
	            DartObj.transform.parent = other.transform;
	            StartCoroutine(TimerGo());
	        }
	    }
	    if (tag == "Player")
	    {
	        var status = PlayerStatus.GetComponent<PlayerStatus>();
	        var falling = status.Player.GetComponent<FallingHolder>();
	        if (status.Player.activeSelf && !status.Killed && !falling.isLanding && !falling.isFalling)
	        {
	            status.NormalDeath();
	            DidTheJob = true;
	            status.ScreenEffects.BlindedAction();
	            StartCoroutine(TimerGo());
	        }
	    }
	    if (tag == "Santa")
	    {
	        var lf = GameObject.Find("LittleSanta").GetComponent<LittleFucker>();
	        if (!lf.Hurt) lf.KnockedOut();
	    }
	}
	// A dart that hits nothing within 2 seconds goes harmless and disappears 2 seconds later.
	private IEnumerator TimerStop()
	{
	    yield return new WaitForSeconds(2);
	    DidTheJob = true;
	    yield return new WaitForSeconds(2);
	    Destroy(DartObj);
	}
	private IEnumerator TimerGo() { yield return new WaitForSeconds(3); Destroy(DartObj); }
}
