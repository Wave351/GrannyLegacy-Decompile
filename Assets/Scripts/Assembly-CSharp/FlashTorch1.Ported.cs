using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FlashTorch1.c (Granny Legacy, IL2CPP x64).
public partial class FlashTorch1
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashL") == 1)
		{
			foreach (GameObject flash in Flash)
			{
				flash.SetActive(true);
			}
		}
	}
}
