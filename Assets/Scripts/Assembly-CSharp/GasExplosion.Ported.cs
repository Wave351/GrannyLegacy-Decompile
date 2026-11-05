using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GasExplosion.c (Granny Legacy, IL2CPP x64).
public partial class GasExplosion
{
	public GasExplosion() { RadiusImpact = 20; PushImpact = 15; CanKillPlayer = true; }
	// The gas can explosion: kills or knocks out everything in range, frees planks and spiders, and pushes rigidbodies.
	private void Start()
	{
	    CM = GameObject.Find("CrowParent");
	    PlayerStatus = GameObject.Find("PlayerManage");
	    var ecObject = GameObject.Find("EnemyController");
	    foreach (var hit in Physics.OverlapSphere(transform.position, RadiusImpact))
	    {
	        string tag = hit.tag;
	        var go = hit.gameObject;
	        if (tag == "spiderMom")
	        {
	            var mom = hit.GetComponent<AI_MomSpider>();
	            if (mom != null) { Debug.Log("Mom pls works yastk"); mom.HitMom(); }
	        }
	        var ec = ecObject ? ecObject.GetComponent<EnemyController>() : null;
	        if (tag == "granny")
	        {
	            if (!ec) return;
	            var ai = ec.Granny ? ec.Granny.GetComponent<AI_Granny>() : null;
	            if (!ec.GrannyKilled) { if (!ai) return; if (!ai.CaughtPlayer) ec.GrannyDeathS(); }
	        }
	        if (tag == "granpa")
	        {
	            if (!ec) return;
	            var ai = ec.Grandpa ? ec.Grandpa.GetComponent<AI_Grandpa>() : null;
	            if (!ec.GrandpaKilled) { if (!ai) return; if (!ai.CaughtPlayer) ec.GrandpaDeathS(); }
	        }
	        var col = hit.GetComponent<Collider>();
	        if (!col) return;
	        if (col.name == "SpiderAtticParent") { var spider = col.GetComponent<AtticSpider>(); if (!spider) return; spider.Dies(); }
	        if (col.name == "CrowNest" || col.name == "CrowEat") { var crow = CM.GetComponent<CrowManage>(); if (!crow) return; crow.KillAssBird(); }
	        if (tag == "Santa")
	        {
	            var lf = GameObject.Find("LittleSanta").GetComponent<LittleFucker>();
	            if (!lf) return;
	            if (!lf.Hurt) lf.KnockedOut();
	        }
	        var body = hit.GetComponent<Rigidbody>();
	        if (CanKillPlayer && tag == "Player")
	        {
	            var status = PlayerStatus.GetComponent<PlayerStatus>();
	            if (!status) return;
	            status.NormalDeath();
	        }
	        if (tag == "granpasoffa")
	        {
	            if (!ec) return;
	            var ai = ec.Grandpa ? ec.Grandpa.GetComponent<AI_Grandpa>() : null;
	            if (!ec.GrandpaKilled) { if (!ai) return; if (!ai.CaughtPlayer) ec.GrandpaDeathSBed(); }
	        }
	        if (tag == "mom") { if (!ec) return; if (!ec.MomKilled) ec.MomShoot(); }
	        if (go.name == "PlankAttic" || go.name == "PlankaOH")
	        {
	            var plank = col.GetComponent<Rigidbody>();
	            if (!plank) return;
	            plank.isKinematic = false;
	            go.AddComponent<ItemSeedData>();
	            var data = hit.GetComponent<ItemSeedData>();
	            if (!data) return;
	            data.itemName = "Plank";
	            go.name = "PlankaKnocked";
	            go.AddComponent<OutlineCheck>();
	        }
	        if (go.name == "Screwdriver") go.layer = LayerMask.NameToLayer("Item");
	        if (go.name == "SpiderWall") { var wall = go.GetComponentInParent<SpiderWall>(); if (!wall) return; wall.DeathInitiates(); }
	        if (hit.GetComponent<ScrewDart>()) hit.GetComponent<ScrewDart>().processKnocked();
	        if (body != null) body.AddExplosionForce(PushImpact, transform.position, RadiusImpact, 1, ForceMode.Impulse);
	    }
	}
	private void Update() { PlayerStatus = GameObject.Find("PlayerManage"); }
}
