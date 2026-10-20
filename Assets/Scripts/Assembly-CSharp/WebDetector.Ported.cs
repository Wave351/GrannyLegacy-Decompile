using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/WebDetector.c (Granny Legacy, IL2CPP x64).
public partial class WebDetector
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "WebTrap")
		{
			AudioSource.PlayClipAtPoint(WebEnter, transform.position);
			FPS.InWeb = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "WebTrap")
		{
			FPS.InWeb = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "WebTrap")
		{
			FPS.InWeb = false;
		}
	}
}
