using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/VentTriggerPush.c (Granny Legacy, IL2CPP x64).
public partial class VentTriggerPush
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HitI();
		}
	}

	public void HitI()
	{
		Vent.Play("VentSnurra");
		SoundVent.SetActive(true);
		UnityEngine.Object.Destroy(gameObject);
	}
}
