using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TextManager.c (Granny Legacy, IL2CPP x64).
public partial class TextManager
{
	private void Reset()
	{
		TimeF = 5f;
		Max = 1.7f;
		ActualTextAmount = 4f;
	}

	private void Update()
	{
		TimeF -= Time.deltaTime * Max;
		if (TimeF < 0f)
		{
			gameObject.SetActive(false);
		}
	}

	public void Add()
	{
		TimeF = ActualTextAmount;
	}

	public void Stop()
	{
		TimeF = 0f;
		gameObject.SetActive(false);
	}
}
