using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Texture_Changing.c (Granny Legacy, IL2CPP x64).
public partial class Texture_Changing
{
	private void Start()
	{
		Material.mainTexture = PlayerPrefs.GetInt("Textures") != 1 ? Default : DefaultNM;
	}

	private void Update()
	{
		Material.mainTexture = PlayerPrefs.GetInt("Textures") != 1 ? Default : DefaultNM;
	}
}
