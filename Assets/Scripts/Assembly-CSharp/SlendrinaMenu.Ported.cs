using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SlendrinaMenu.c (Granny Legacy, IL2CPP x64).
public partial class SlendrinaMenu
{
	public SlendrinaMenu()
	{
		TimerMaxTeleport = 5f;
	}

	private void Update()
	{
		TimerTeleport += Time.deltaTime;
		if (TimerTeleport < TimerMaxTeleport)
		{
			return;
		}
		int index = UnityEngine.Random.Range(0, TeleportPositions.Length);
		transform.position = TeleportPositions[index].position;
		transform.rotation = TeleportPositions[index].rotation;
		TimerTeleport = 0f;
		TimerMaxTeleport = UnityEngine.Random.Range(4f, 8.5f);
	}
}
