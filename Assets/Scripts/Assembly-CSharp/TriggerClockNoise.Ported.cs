using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerClockNoise.c (Granny Legacy, IL2CPP x64).
public partial class TriggerClockNoise
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || Clock == null)
		{
			return;
		}
		AudioSource clockAudio = Clock.GetComponent<AudioSource>();
		if (clockAudio == null || clockAudio.isPlaying)
		{
			return;
		}
		UnityEngine.Object.Instantiate(NoiseObj, transform.position, transform.rotation);
		clockAudio.Play();
		Clock.GetComponent<Animation>().Play("Klocka");
		if (PlayerPrefs.GetInt("Diff") == 5)
		{
			UnityEngine.Object.Instantiate(NoiseObjGrandpa, transform.position, transform.rotation);
		}
	}
}
