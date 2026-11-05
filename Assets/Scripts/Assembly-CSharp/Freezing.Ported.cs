using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Freezing.c (Granny Legacy, IL2CPP x64).
public partial class Freezing
{
	private void Start() { ScEFF = GameObject.Find("ScreenEffects"); EnemyStuff = GameObject.Find("EnemyController"); }
	private void Update() { ScEFF = GameObject.Find("ScreenEffects"); EnemyStuff = GameObject.Find("EnemyController"); }
	// Freeze trap: armed once it lands on a floor, then freezes the first player or enemy stepping in it.
	private void OnTriggerStay(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if ((tag == "golv" || tag == "grus" || tag == "Noice") && !Prepared)
	    {
	        Prepared = true;
	        FreezeTrap.material.mainTexture = ReadyToFreeze;
	        gameObject.layer = LayerMask.NameToLayer("Default");
	    }
	    if (!Prepared || Triggerd) return;
	    var audio = GetComponent<AudioSource>();
	    var ec = EnemyStuff ? EnemyStuff.GetComponent<EnemyController>() : null;
	    if (tag == "Player")
	    {
	        var effects = ScEFF.GetComponent<ScreenEffects>();
	        if (effects.Frozen) return;
	        Fire(audio);
	        gameObject.layer = LayerMask.NameToLayer("Item");
	        effects.FrozePlayer();
	        StartCoroutine(WaitRetagged());
	        return;
	    }
	    if (tag == "spiderMom")
	    {
	        Fire(audio);
	        var mom = other.GetComponent<AI_MomSpider>();
	        if (!mom.IsHit) mom.HitMom();
	    }
	    else if (tag == "granny")
	    {
	        Fire(audio);
	        var ai = ec.Granny.GetComponent<AI_Granny>();
	        if (!ec.GrannyKilled && !ai.IsDying && !ai.CaughtPlayer) ai.FrozenEnemy();
	    }
	    else if (tag == "granpa" || other.name == "GranpaParent")
	    {
	        Fire(audio);
	        var ai = ec.Grandpa.GetComponent<AI_Grandpa>();
	        if (!ec.GrandpaKilled && !ai.IsDying && !ai.CaughtPlayer) ai.FrozenEnemy();
	    }
	    else if (tag == "mom")
	    {
	        if (ec.MomKilled) return;
	        Fire(audio);
	        ec.MomFroze();
	    }
	    else return;
	    gameObject.layer = LayerMask.NameToLayer("Item");
	    StartCoroutine(WaitRetagged());
	}
	private void Fire(AudioSource audio)
	{
	    Triggerd = true;
	    FrozeEffect.Play();
	    audio.Play();
	    FreezeTrap.material.mainTexture = OldText;
	}
	// The used trap can be picked up again 5 seconds later.
	private IEnumerator WaitRetagged()
	{
	    Destroy(gameObject.GetComponent<ItemSeedData>());
	    yield return new WaitForSeconds(5);
	    gameObject.AddComponent<ItemSeedData>();
	    var data = gameObject.GetComponent<ItemSeedData>();
	    if (data) data.itemName = "FreezeTrap";
	}
}
