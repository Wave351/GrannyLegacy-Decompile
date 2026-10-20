using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/NormalFeet.c (Granny Legacy, IL2CPP x64).
public partial class NormalFeet
{
	public void PlayThem()
	{
		int index = UnityEngine.Random.Range(0, Sounds.Length);
		AudioSource source = GetComponent<AudioSource>();
		if (source != null)
		{
			source.PlayOneShot(Sounds[index]);
		}
	}
}
