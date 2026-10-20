using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ChristmasTexturesSetup.c (Granny Legacy, IL2CPP x64).
public partial class ChristmasTexturesSetup
{
	private void Update()
	{
		if (PlayerPrefs.GetInt("Textures") == 1)
		{
			Material.mainTexture = NM;
			return;
		}
		switch (PlayerPrefs.GetInt("Holiday"))
		{
		case 7:
		case 6:
			Material.mainTexture = DefaultSand;
			break;
		case 5:
			Material.mainTexture = DefaultGrass;
			break;
		case 2:
			Material.mainTexture = DefaultSnow;
			break;
		case 1:
			Material.mainTexture = DefaultHalloween;
			break;
		default:
			Material.mainTexture = Default;
			break;
		}
	}
}
