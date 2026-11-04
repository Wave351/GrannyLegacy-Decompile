using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerCreakNoise.c (Granny Legacy, IL2CPP x64).
public partial class TriggerCreakNoise
{
	public TriggerCreakNoise() { CanCreak = true; Number = 3; }
	private void OnTriggerEnter(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player") || !CanCreak) return;
	    CanCreak = false;
	    StartCoroutine(WaitForNextCreak());
	    Instantiate(NoiseObj, transform.position, transform.rotation);
	    var audio = GetComponent<AudioSource>();
	    switch (UnityEngine.Random.Range(0, 3))
	    {
	        case 0: audio.pitch = .95f; break;
	        case 1: audio.pitch = 1; break;
	        case 2: audio.pitch = 1.05f; break;
	    }
	    if (audio) audio.Play();
	}
	private IEnumerator WaitForNextCreak() { yield return new WaitForSeconds(Number); CanCreak = true; }
}
