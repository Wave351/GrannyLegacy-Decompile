using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/StealDoorHit.c (Granny Legacy, IL2CPP x64).
public partial class StealDoorHit
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "arrow")
		{
			OpenIt();
		}
	}

	public void OpenIt()
	{
		if (Hit)
		{
			return;
		}
		Hit = true;
		GetComponent<AudioSource>().PlayOneShot(Unlocked);
		Target.GetComponent<Animation>().Play("skjutPlatta");
		Handle.GetComponent<Animation>().Play("skjutPlattaDel1");
		StealDoor.name = "StealDoor";
		SlidePlateFog fog = GetComponentInParent<SlidePlateFog>();
		if (fog != null)
		{
			fog.SwitchBack();
		}
	}
}
