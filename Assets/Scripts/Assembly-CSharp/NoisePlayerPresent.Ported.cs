using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/NoisePlayerPresent.c (Granny Legacy, IL2CPP x64).
public partial class NoisePlayerPresent
{
	private void OnTriggerStay(Collider Player)
	{
		if (Player.gameObject.tag == "Player")
		{
			DropCheck.CanRepos = true;
		}
	}

	private void OnTriggerExit(Collider Player)
	{
		if (Player.gameObject.tag == "Player")
		{
			DropCheck.CanRepos = false;
		}
	}
}
