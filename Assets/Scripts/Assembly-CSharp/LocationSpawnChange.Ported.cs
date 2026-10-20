using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LocationSpawnChange.c (Granny Legacy, IL2CPP x64).
public partial class LocationSpawnChange
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		if (LocationPoint == 1f)
		{
			HE.EnterF1();
		}
		if (LocationPoint == 2f)
		{
			HE.EnterF2();
		}
		if (LocationPoint == 3f)
		{
			HE.EnterCellar();
		}
		if (LocationPoint == 4f)
		{
			HE.EnterOldH();
		}
	}
}
