using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TChangeRare.c (Granny Legacy, IL2CPP x64).
public partial class TChangeRare
{
	private void Start()
	{
		Material.mainTexture = PlayerPrefs.GetInt("Textures") != 1 ? Default : DefaultNM;
	}
}
