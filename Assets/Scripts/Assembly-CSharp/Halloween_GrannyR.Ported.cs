using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Halloween_GrannyR.c (Granny Legacy, IL2CPP x64).
public partial class Halloween_GrannyR
{
	private void Update()
	{
		switch (PlayerPrefs.GetInt("Holiday"))
		{
		case 1:
			TC.Default = WitchTexture;
			break;
		case 2:
			TC.Default = ChristmasTexture;
			break;
		case 3:
			TC.Default = ValentineTexture;
			break;
		case 4:
			TC.Default = MarchTexture;
			break;
		case 5:
			TC.Default = BunnyTexture;
			break;
		case 6:
			TC.Default = LGBTTexture;
			break;
		}
	}
}
