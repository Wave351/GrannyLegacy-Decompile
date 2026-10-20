using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SaunaDeath.c (Granny Legacy, IL2CPP x64).
public partial class SaunaDeath
{
	private void Update()
	{
		GrannyPresence = Sauna.PutHandle && Sauna.SwitchOn && GrannyIn;
		GrandpaPresence = Sauna.PutHandle && Sauna.SwitchOn && GrandpaIn;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "granny")
		{
			GrannyIn = true;
			Granny.IsInSauna = true;
		}
		if (other.gameObject.tag == "granpa")
		{
			GrandpaIn = true;
			Grandpa.IsInSauna = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "granny")
		{
			GrannyIn = false;
			Granny.IsInSauna = false;
		}
		if (other.gameObject.tag == "granpa")
		{
			GrandpaIn = false;
			Grandpa.IsInSauna = false;
		}
	}
}
