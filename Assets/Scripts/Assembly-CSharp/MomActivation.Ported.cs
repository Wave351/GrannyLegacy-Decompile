using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MomActivation.c (Granny Legacy, IL2CPP x64).
public partial class MomActivation
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		Mom.SetActive(true);
		MomAppearS.SetActive(true);
		MusicCellar.SetActive(true);
		UnityEngine.Object.Destroy(gameObject);
	}
}
