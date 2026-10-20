using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SkullFalls.c (Granny Legacy, IL2CPP x64).
public partial class SkullFalls
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Skull.isKinematic = false;
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
