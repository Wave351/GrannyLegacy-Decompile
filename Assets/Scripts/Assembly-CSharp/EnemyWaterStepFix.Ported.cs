using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EnemyWaterStepFix.c (Granny Legacy, IL2CPP x64).
public partial class EnemyWaterStepFix
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "granny")
		{
			GrannySteps.InWater = true;
		}
		if (other.gameObject.tag == "granpa")
		{
			GrandpaSteps.InWater = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "granny")
		{
			GrannySteps.InWater = false;
		}
		if (other.gameObject.tag == "granpa")
		{
			GrandpaSteps.InWater = false;
		}
	}
}
