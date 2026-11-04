using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TrapPoison.c (Granny Legacy, IL2CPP x64).
public partial class TrapPoison
{
	// Poison trap: armed once it lands, then releases its cloud on the first player or enemy.
	private IEnumerator Waiter()
	{
	    yield return new WaitForSeconds(.3f);
	    Trigger.SetActive(true);
	    yield return new WaitForSeconds(14);
	    Trigger.SetActive(false);
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (Used) return;
	    string tag = other.gameObject.tag;
	    if (!Poisoning)
	    {
	        if (tag == "golv" || tag == "Noice" || tag == "water" || tag == "grus")
	        { Poisoning = true; gameObject.layer = LayerMask.NameToLayer("Default"); }
	        return;
	    }
	    if (tag != "Player" && tag != "granny" && tag != "spiderMom" && tag != "granpa") return;
	    Sus.Play(); Sys.Play();
	    Used = true;
	    gameObject.layer = LayerMask.NameToLayer("EnemyPass");
	    StartCoroutine(Waiter());
	}
}
