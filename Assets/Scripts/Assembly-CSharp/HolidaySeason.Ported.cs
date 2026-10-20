using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HolidaySeason.c (Granny Legacy, IL2CPP x64).
public partial class HolidaySeason
{
	private void Start()
	{
		bool on = PlayerPrefs.GetInt("Holiday") != 0;
		foreach (GameObject item in HolidayStuff)
		{
			item.SetActive(on);
		}
	}
}
