using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Poisoning.c (Granny Legacy, IL2CPP x64).
public partial class Poisoning
{
	// Poison cloud: kills the player (once), hits the spider mom, kills the attic spider, Granny and Grandpa, and knocks out Santa.
	private void OnTriggerEnter(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if (tag == "Player")
	    {
	        var status = GameObject.Find("PlayerManage").GetComponent<PlayerStatus>();
	        var falling = status.Player.GetComponent<FallingHolder>();
	        if (!falling.isFalling && !falling.isLanding) status.NormalDeath();
	        PlayerCaughtInIt = true;
	    }
	    else if (tag == "spiderMom" && !PlayerCaughtInIt)
	    {
	        var mom = other.gameObject.GetComponent<AI_MomSpider>();
	        if (!mom.IsHit) mom.HitMom();
	    }
	    else
	    {
	        var name = other.GetComponent<Collider>().name;
	        if (name == "SpiderAtticParent") other.gameObject.GetComponent<AtticSpider>().Dies();
	        else if (tag == "granny" && !PlayerCaughtInIt)
	        {
	            var ec = GameObject.Find("EnemyController").GetComponent<EnemyController>();
	            if (!ec.GrannyKilled && !other.gameObject.GetComponent<AI_Granny>().CaughtPlayer) ec.GrannyDeath(true);
	        }
	        else if ((tag == "granpa" || name == "GranpaParent") && !PlayerCaughtInIt)
	        {
	            var ec = GameObject.Find("EnemyController").GetComponent<EnemyController>();
	            if (!ec.GrandpaKilled && !other.gameObject.GetComponent<AI_Grandpa>().CaughtPlayer) ec.GrandpaDeath(true);
	        }
	    }
	    if (other.gameObject.tag != "Santa") return;
	    var santa = GameObject.Find("LittleSanta");
	    var lf = santa ? santa.GetComponent<LittleFucker>() : null;
	    if (lf && !lf.Hurt) lf.KnockedOut();
	}
}
