using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HideVision.c (Granny Legacy, IL2CPP x64).
public partial class HideVision
{
	public HideVision()
	{
		HidingOnTop = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HidingOnTop = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HidingOnTop = false;
		}
	}
}
