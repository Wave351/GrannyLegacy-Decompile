using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FlashTorch.c (Granny Legacy, IL2CPP x64).
public partial class FlashTorch
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashL") == 1)
		{
			Flash.SetActive(true);
		}
	}
}
