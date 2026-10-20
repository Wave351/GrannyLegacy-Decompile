using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HideDisable.c (Granny Legacy, IL2CPP x64).
public partial class HideDisable
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HF.InTR = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HF.InTR = false;
		}
	}
}
