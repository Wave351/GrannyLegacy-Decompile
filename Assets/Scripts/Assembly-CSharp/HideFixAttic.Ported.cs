using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HideFixAttic.c (Granny Legacy, IL2CPP x64).
public partial class HideFixAttic
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		PlayerStatus.HidingUnderThings = true;
		Granny.PlayerPos = PosFixPlayerOn.transform.position;
		Grandpa.PlayerPos = PosFixPlayerOn.transform.position;
		if (!Granny.EnemyVision.Seeing && Granny.EnemyVision.TimerStopVision > 0.7f)
		{
			Granny.UnSeenPlayer = true;
		}
		if (!Grandpa.EnemyVision.Seeing && Grandpa.EnemyVision.TimerStopVision > 0.7f)
		{
			Grandpa.UnSeenPlayer = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		PlayerStatus.HidingUnderThings = false;
		Granny.CanOpenClosets = false;
		Granny.UnSeenPlayer = false;
		Grandpa.CanOpenClosets = false;
		Grandpa.UnSeenPlayer = false;
	}
}
