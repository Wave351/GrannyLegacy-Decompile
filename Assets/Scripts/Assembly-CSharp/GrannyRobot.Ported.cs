using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GrannyRobot.c (Granny Legacy, IL2CPP x64).
public partial class GrannyRobot
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("RoboDesigns") != 1)
		{
			return;
		}
		Design.SetActive(true);
		CamGranny.transform.parent = NewPos1;
		OldDesign.SetActive(false);
		Granny.Idle = NewIdle1;
		Granny.WalkAnim = NewWalk1;
		Granny.JumpscarePlayer = NewHit1;
		Granny.Search = NewIdle1;
		Granny.Anim = NewAnim1;
		Jump1.pitch = 2f;
		ISee1.pitch = 1.8f;
		NavGrandpaRename.name = "NavNoMoreSleep";
	}
}
