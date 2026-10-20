using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TeddyHalloween.c (Granny Legacy, IL2CPP x64).
public partial class TeddyHalloween
{
	private void Start()
	{
		switch (PlayerPrefs.GetInt("Holiday"))
		{
		case 7:
		case 6:
			Glasses.SetActive(true);
			break;
		case 5:
			BunnyEars.SetActive(true);
			break;
		case 4:
			March.SetActive(true);
			break;
		case 3:
			Valentine.SetActive(true);
			break;
		case 1:
			transform.parent.gameObject.GetComponent<Renderer>().material.mainTexture = TeddyHallo;
			MeshRenderer mesh = transform.GetComponent<MeshRenderer>();
			if (mesh != null)
			{
				mesh.enabled = false;
			}
			break;
		}
	}
}
