using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerBackgroundNoise.c (Granny Legacy, IL2CPP x64).
public partial class TriggerBackgroundNoise
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		if (Attic)
		{
			BGManager.InAttic = true;
			BGManager.InHouse = false;
			BGManager.InOutside = false;
		}
		else if (Outside)
		{
			BGManager.InAttic = false;
			BGManager.InHouse = false;
			BGManager.InOutside = true;
		}
		else if (Inside)
		{
			BGManager.InAttic = false;
			BGManager.InHouse = true;
			BGManager.InOutside = false;
		}
	}
}
