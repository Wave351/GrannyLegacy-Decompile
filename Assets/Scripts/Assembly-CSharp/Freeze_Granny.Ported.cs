using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Freeze_Granny.c (Granny Legacy, IL2CPP x64).
public partial class Freeze_Granny
{
	private void Start()
	{
	    EC = GameObject.Find("EnemyController");
	    StartCoroutine(KillAll());
	    int weapon = PlayerPrefs.GetInt("GrannyWP");
	    if (weapon == 0)
	    {
	        // Holiday props are only shown when Granny holds no weapon.
	        int holiday = PlayerPrefs.GetInt("Holiday");
	        if (holiday == 3) { Bat.SetActive(false); Broom.SetActive(false); Candy.SetActive(false); Arrow.SetActive(true); }
	        else if (holiday == 2) { Bat.SetActive(false); Broom.SetActive(false); Candy.SetActive(true); Arrow.SetActive(false); }
	        else if (holiday == 1) { Broom.SetActive(true); Candy.SetActive(false); Bat.SetActive(false); Arrow.SetActive(false); }
	    }
	    if (weapon != 1 && weapon != 2) return;
	    var ec = EC ? EC.GetComponent<EnemyController>() : null;
	    var ai = ec ? ec.Granny.GetComponent<AI_Granny>() : null;
	    if (!ai) return;
	    if (weapon == 1 && ai.Crossbow.activeSelf) Crossbow.SetActive(true);
	    if (weapon == 2 && ai.Spray.activeSelf) Spray.SetActive(true);
	    foreach (var r in BatsOff) r.enabled = false;
	}
	private void OnTriggerEnter(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if (tag != "golv" && tag != "Noice" && tag != "grus" && tag != "water") return;
	    if (Broke) return;
	    Broke = true;
	    var audio = GetComponent<AudioSource>();
	    if (!audio) return;
	    audio.PlayOneShot(FrostBreak);
	    foreach (var part in Parts)
	    {
	        if (CollTop == null) break;
	        CollTop.enabled = true;
	        var body = part ? part.GetComponent<Rigidbody>() : null;
	        if (!body) break;
	        body.isKinematic = false;
	        part.transform.parent = null;
	        part.layer = LayerMask.NameToLayer("Item");
	        ImpactB.SetActive(true);
	    }
	}
	private IEnumerator KillAll() { yield return new WaitForSeconds(TimerGone); DestructionRagdolls(); }
	public void DestructionRagdolls()
	{
	    foreach (var part in Parts) Destroy(part);
	    Destroy(ParentOfIt);
	}
}
