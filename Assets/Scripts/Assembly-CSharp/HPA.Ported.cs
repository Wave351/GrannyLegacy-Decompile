using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HPA.c (Granny Legacy, IL2CPP x64).
public partial class HPA
{
	private void Update()
	{
		HandlePuzzles p = Puzzles;
		bool routes = p.PlacedEnginePart && p.PlacedCarBattery && p.usedSparkPlug && p.openedPort && p.SkruvEngine == 6f && p.TankFuelled && p.IsDoorFree && p.PlacedStick && p.UsedChainCutter && p.UsedRustyPadlock;
		if (PlayerPrefs.GetInt("Diff") == 3)
		{
			if (PlayerPrefs.GetInt("Extras") == 1)
			{
				if (routes && p.RechargedRobot && PlayerPrefs.GetInt("GlitchesAndShit") == 0)
				{
					PlayerPrefs.SetInt("EX_AllRouteYes", 1);
				}
			}
			else if (routes && PlayerPrefs.GetInt("GlitchesAndShit") == 0)
			{
				PlayerPrefs.SetInt("EX_AllRouteNo", 1);
			}
		}
		if (PlayerPrefs.GetInt("Diff") == 5 && PlayerPrefs.GetInt("Extras") == 1 && routes && p.RechargedRobot && PlayerPrefs.GetInt("Granny") == 1)
		{
			int grandpa = PlayerPrefs.GetInt("Grandpa");
			if (grandpa == 1 || grandpa == 2)
			{
				SetHolRoutesUp = true;
			}
		}
	}
}
