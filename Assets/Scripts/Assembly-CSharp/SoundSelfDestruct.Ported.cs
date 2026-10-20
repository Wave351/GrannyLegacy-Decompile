using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SoundSelfDestruct.c (Granny Legacy, IL2CPP x64).
public partial class SoundSelfDestruct
{
	private void Update()
	{
		if (AbleToDestruct && Audio != null && !Audio.isPlaying)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
