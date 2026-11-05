using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MagicalFreeze.c (Granny Legacy, IL2CPP x64).
public partial class MagicalFreeze
{
	private void Start() { EnemyStuff = GameObject.Find("EnemyController"); }
	private void Update() { EnemyStuff = GameObject.Find("EnemyController"); }
	private void PlayEffect() { if (!FrozeEffect.isPlaying) GetComponent<AudioSource>().Play(); }
	// Freeze ornament cloud: freezes the spider mom, Granny, Grandpa and Angelina (mom).
	private void OnTriggerStay(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if (tag == "spiderMom")
	    {
	        var mom = other.GetComponent<AI_MomSpider>();
	        if (!mom.IsHit) { mom.HitMom(); PlayEffect(); }
	    }
	    if (tag == "granny")
	    {
	        var ec = EnemyStuff.GetComponent<EnemyController>();
	        var ai = ec.Granny.GetComponent<AI_Granny>();
	        if (!ec.GrannyKilled && !ai.IsDying && !ai.CaughtPlayer) { ai.FrozenEnemy(); PlayEffect(); }
	    }
	    if (tag == "granpa")
	    {
	        var ec = EnemyStuff.GetComponent<EnemyController>();
	        var ai = ec.Grandpa.GetComponent<AI_Grandpa>();
	        if (!ec.GrandpaKilled && !ai.IsDying && !ai.CaughtPlayer) { ai.FrozenEnemy(); PlayEffect(); }
	    }
	    if (tag != "mom") return;
	    var controller = EnemyStuff ? EnemyStuff.GetComponent<EnemyController>() : null;
	    if (!controller || controller.MomKilled) return;
	    PlayEffect();
	    controller.MomFroze();
	}
}
