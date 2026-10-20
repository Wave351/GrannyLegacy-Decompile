using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AudioPrewarm.c (Granny Legacy, IL2CPP x64).
public partial class AudioPrewarm
{
	private void Start()
	{
		foreach (AudioClip clip in Clip)
		{
			if (clip == null)
			{
				break;
			}
			clip.LoadAudioData();
		}
		UnityEngine.Object.Destroy(GetComponent<AudioPrewarm>());
	}
}
