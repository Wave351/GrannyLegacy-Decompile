using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ExtraTrapsLogic.c (Granny Legacy, IL2CPP x64).
public partial class ExtraTrapsLogic
{
	private void Start()
	{
		StartCoroutine(StartFunction());
		PlayerPrefs.GetInt("MoreTraps");
		if (PlayerPrefs.GetInt("GlitchesAndShit") != 1)
		{
			return;
		}
		PickRay.RaycastCheckItemDis = 0f;
		SpiderCellarTrigger.SetActive(true);
		MCPlayHouse.enabled = false;
		LockerGarage.IfLockKyl = false;
		PlayHouseBackHideOut.SetActive(false);
		BarrelsHideOut.SetActive(false);
		CanScrew.layer = LayerMask.NameToLayer("Default");
		Screwdriver.layer = LayerMask.NameToLayer("Item");
		GECoffin1.RadiusImpact = 4f;
		GECoffin2.RadiusImpact = 4f;
		MomPlace.tag = "golv";
		foreach (GameObject trigger in ItemSoundTriggers)
		{
			if (trigger == null)
			{
				break;
			}
			trigger.SetActive(false);
		}
	}

	private IEnumerator StartFunction()
	{
		yield return new WaitForSeconds(22f);
		PlayerPrefs.GetInt("MoreTraps");
	}
}
