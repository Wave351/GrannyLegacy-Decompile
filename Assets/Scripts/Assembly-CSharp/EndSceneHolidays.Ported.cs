using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndSceneHolidays.c (Granny Legacy, IL2CPP x64).
public partial class EndSceneHolidays
{
	private void Update()
	{
		if (PlayerPrefs.GetInt("Holiday") == 2)
		{
			Santa.SetActive(true);
			Santa.GetComponent<Animator>().Play("Idle1");
			SantaRenderer.material.color = NewSantaColor;
			TheEndTR.material.mainTexture = MerryCHText;
		}
		if (PlayerPrefs.GetInt("Holiday") == 6)
		{
			foreach (GameObject item in holidayPrideStuff)
			{
				item.SetActive(true);
			}
		}
	}
}
