using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PickGrandpa.c (Granny Legacy, IL2CPP x64).
public partial class PickGrandpa
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Shotgun2" && !AI.IsDying)
		{
			AI.Shotgun.SetActive(true);
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}
}
