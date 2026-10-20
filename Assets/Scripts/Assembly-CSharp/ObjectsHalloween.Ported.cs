using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ObjectsHalloween.c (Granny Legacy, IL2CPP x64).
public partial class ObjectsHalloween
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("Holiday") != 1)
		{
			UnityEngine.Object.Destroy(GetComponent<ObjectsHalloween>());
			return;
		}
		M.mainTexture = Halloween;
	}
}
