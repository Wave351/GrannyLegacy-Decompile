using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/BlindSensor_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class BlindSensor_Grandpa
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Enemy.SensorPlayer();
		}
	}
}
