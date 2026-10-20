using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RandomRadioTrack.c (Granny Legacy, IL2CPP x64).
public partial class RandomRadioTrack
{
	private void Start()
	{
		audio.clip = funnySounds[UnityEngine.Random.Range(0, funnySounds.Length)];
		audio.Play();
	}
}
