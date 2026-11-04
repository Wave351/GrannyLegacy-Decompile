using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Halloween_Granny.c (Granny Legacy, IL2CPP x64).
public partial class Halloween_Granny
{
	// Holiday skins for Granny: 1 witch, 2 Christmas, 3 Valentine's, 4 leprechaun, 5 bunny, 6 LGBT, 7/other normal.
	private void Update()
	{
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    Texture2D skin = holiday == 1 ? WitchTexture : holiday == 2 ? ChristmasTexture : holiday == 3 ? ValentineTexture
	        : holiday == 4 ? LeprechaunTexture : holiday == 5 ? BunnyTexture : holiday == 6 ? LGBTTexture : Granny;
	    foreach (var t in Ts) t.Default = skin;
	    Bat.SetActive(holiday != 1 && holiday != 2 && holiday != 3);
	    Broom.SetActive(holiday == 1);
	    Candy.SetActive(holiday == 2);
	    Arrow.SetActive(holiday == 3);
	}
}
