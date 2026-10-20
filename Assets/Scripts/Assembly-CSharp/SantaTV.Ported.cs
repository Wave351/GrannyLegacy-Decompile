using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SantaTV.c (Granny Legacy, IL2CPP x64).
public partial class SantaTV
{
	private void Start()
	{
		if (!CanActivate)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && CanActivate)
		{
			TVVid.SetActive(true);
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
