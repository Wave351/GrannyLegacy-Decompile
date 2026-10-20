using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Enter_SC.c (Granny Legacy, IL2CPP x64).
public partial class Enter_SC
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || Entered_SC)
		{
			return;
		}
		Entered_SC = true;
		BG.InCellar = true;
		BG.InAttic = false;
		BG.InOutside = false;
		BG.InHouse = false;
		ExitSC1.Exited_SC = false;
		ExitSC2.Exited_SC = false;
		MomMesh.SetActive(true);
		Mom1.mute = false;
		Mom2.mute = false;
		Mom3.mute = false;
		WaterSplash.CanSplash = true;
		WS.CanSplash = true;
		if (EnemyEyes1.IsChasing)
		{
			EnemyEyes1.TimerStopVision = 100f;
		}
		if (EnemyEyes2.IsChasing)
		{
			EnemyEyes2.TimerStopVision = 100f;
		}
		Renderer hat = null;
		switch (PlayerPrefs.GetInt("Holiday"))
		{
		case 7:
		case 6:
			hat = Glasses;
			break;
		case 5:
			hat = Bunny;
			break;
		case 4:
			hat = Lepra;
			break;
		case 3:
			hat = Valentines;
			break;
		case 2:
			hat = HatChristmas;
			break;
		case 1:
			HatChristmas.enabled = false;
			HalloweenHat.enabled = true;
			break;
		}
		if (hat != null)
		{
			hat.enabled = true;
			HalloweenHat.enabled = false;
		}
	}
}
