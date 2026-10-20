using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/DynamicPuzzleItemSwitch.c (Granny Legacy, IL2CPP x64).
public partial class DynamicPuzzleItemSwitch
{
	public DynamicPuzzleItemSwitch()
	{
		DisableChance = 50f;
	}

	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashLUnable") == 1 && UnityEngine.Random.Range(0f, 100f) < DisableChance)
		{
			transform.position = PositionSwitch1.position;
			transform.rotation = PositionSwitch1.rotation;
			OtherObjectToSwitch.transform.position = PositionSwitch2.position;
			OtherObjectToSwitch.transform.rotation = PositionSwitch2.rotation;
		}
		UnityEngine.Object.Destroy(GetComponent<DynamicPuzzleItemSwitch>());
	}
}
