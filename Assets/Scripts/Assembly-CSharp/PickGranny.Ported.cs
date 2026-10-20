using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PickGranny.c (Granny Legacy, IL2CPP x64).
public partial class PickGranny
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "Crossbow" && PlayerPrefs.GetInt("GrannyWP") == 1 && !AI.IsDying)
		{
			AI.Crossbow.SetActive(true);
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.gameObject.name == "PepperSpray" && PlayerPrefs.GetInt("GrannyWP") == 2 && !AI.IsDying)
		{
			AI.Spray.SetActive(true);
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}
}
