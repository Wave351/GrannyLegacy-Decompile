using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Freeze_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class Freeze_Grandpa
{
	private void Start()
	{
	    StartCoroutine(KillAll());
	    EC = GameObject.Find("EnemyController");
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    if (holiday == 2) { CaneM.enabled = false; Wings.SetActive(true); Staff.SetActive(true); }
	    else if (holiday == 1) { CaneM.enabled = false; Bone1.SetActive(true); Bone2.SetActive(true); }
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if (g == 1) Cane.SetActive(true);
	    else if (g == 2 && EC.GetComponent<EnemyController>().ShotgunGrandpa.activeSelf) Shotgun.SetActive(true);
	}
	// The frozen Grandpa shatters into its parts when it hits the floor.
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
	        BodyOriginal.SetActive(false);
	        part.SetActive(true);
	        part.transform.parent = null;
	        part.layer = LayerMask.NameToLayer("Item");
	        if (Shotgun != null) Shotgun.transform.parent = armItem.transform;
	        Cane.transform.parent = armItem.transform;
	        Hat.transform.parent = Head.transform;
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
