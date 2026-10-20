using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PlankPlacementTrigger.c (Granny Legacy, IL2CPP x64).
public partial class PlankPlacementTrigger
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && PickRay.H_PL.activeSelf)
		{
			Highlight.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Highlight.SetActive(false);
		}
	}
}
