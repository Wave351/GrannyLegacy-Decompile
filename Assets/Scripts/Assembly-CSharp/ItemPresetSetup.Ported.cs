using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemPresetSetup.c (Granny Legacy, IL2CPP x64).
public partial class ItemPresetSetup
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashL") != 0)
		{
			return;
		}
		bool extras = PlayerPrefs.GetInt("Extras") == 1;
		float preset = OM.PresetChoosenCurrent;
		if (preset == 0f)
		{
			NewPosition = extras ? PositionExtra_P1 : PositionMain_P1;
		}
		else if (preset == 1f)
		{
			NewPosition = extras ? PositionExtra_P2 : PositionMain_P2;
		}
		else if (preset == 2f)
		{
			NewPosition = extras ? PositionExtra_P3 : PositionMain_P3;
		}
		else if (preset == 3f)
		{
			NewPosition = extras ? PositionExtra_P4 : PositionMain_P4;
		}
		else if (preset == 4f)
		{
			NewPosition = extras ? PositionExtra_P5 : PositionMain_P5;
		}
		transform.position = NewPosition.position;
		transform.rotation = NewPosition.rotation;
	}
}
