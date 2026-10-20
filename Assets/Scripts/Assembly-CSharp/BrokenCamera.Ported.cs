using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/BrokenCamera.c (Granny Legacy, IL2CPP x64).
public partial class BrokenCamera
{
	private void Start()
	{
		if (Initiated)
		{
			return;
		}
		Initiated = true;
		gameObject.SetActive(false);
	}
}
