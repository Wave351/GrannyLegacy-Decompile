using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Exit_SC.c (Granny Legacy, IL2CPP x64).
public partial class Exit_SC
{
	public void StopNewDay()
	{
		Exited_SC = true;
		Other_ExitSC.Exited_SC = true;
		EnterSC.Entered_SC = false;
		WSplash.CanSplash = false;
		MomMesh.SetActive(false);
		Mom1.mute = true;
		Mom2.mute = true;
		Mom3.mute = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || Exited_SC)
		{
			return;
		}
		if (AI.IsChasing)
		{
			AI.StopChase();
		}
		Exited_SC = true;
		MomMesh.SetActive(false);
		Mom1.mute = true;
		Mom2.mute = true;
		Mom3.mute = true;
		BG.InCellar = false;
		BG.InAttic = false;
		BG.InOutside = false;
		BG.InHouse = true;
		WSplash.CanSplash = false;
		EnterSC.Entered_SC = false;
		Other_ExitSC.Exited_SC = true;
		HatChristmas.enabled = false;
		HalloweenHat.enabled = false;
		Glasses.enabled = false;
		Lepra.enabled = false;
		Valentines.enabled = false;
		Bunny.enabled = false;
	}
}
