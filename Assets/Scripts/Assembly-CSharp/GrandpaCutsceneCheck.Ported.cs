using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GrandpaCutsceneCheck.c (Granny Legacy, IL2CPP x64).
public partial class GrandpaCutsceneCheck
{
	private void Start()
	{
		int grandpa = PlayerPrefs.GetInt("Grandpa");
		if (grandpa == 1)
		{
			Cane.SetActive(true);
		}
		else if (grandpa == 2)
		{
			Shooty.SetActive(true);
		}
	}
}
