using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MelonCut.c (Granny Legacy, IL2CPP x64).
public partial class MelonCut
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "GuillotineBlade")
		{
			return;
		}
		bool whole = Melon.activeSelf;
		UnityEngine.Object.Instantiate(Noice, transform.position, transform.rotation);
		if (whole)
		{
			Melon.SetActive(false);
			DeadMelon.SetActive(true);
		}
		CutArea.SetActive(false);
	}
}
