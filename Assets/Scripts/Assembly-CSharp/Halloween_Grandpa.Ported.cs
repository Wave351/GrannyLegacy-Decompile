using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Halloween_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class Halloween_Grandpa
{
	// Holiday skins for Grandpa: 1 skull (bones), 2 Christmas (staff and wings), 3 Valentine's, 4 March, 5 Easter,
	// 6 pride accessories; "LavaFrance" swaps in the lava skin.
	private void Update()
	{
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    bool lava = PlayerPrefs.GetInt("LavaFrance") == 1;
	    Texture2D skin = holiday == 6 ? Pride : holiday == 5 ? EasterTexture : holiday == 4 ? MarchTexture : holiday == 3 ? ValentinesTexture
	        : holiday == 2 ? ChristmasTexture : holiday == 1 ? SkullTexture : Default;
	    if (PlayerPrefs.GetInt("LavaFrance") == 0) foreach (var t in Ts) t.Default = skin;
	    if (holiday == 6)
	    {
	        BoneChest.SetActive(false); Bone.SetActive(false); Cane.enabled = true;
	        Staff.SetActive(false); Wings.SetActive(false);
	        foreach (var a in prideAccessories) a.SetActive(true);
	    }
	    else if (holiday == 2)
	    {
	        BoneChest.SetActive(false); Bone.SetActive(false);
	        Staff.SetActive(true); Wings.SetActive(true); Cane.enabled = false;
	    }
	    else
	    {
	        bool skull = holiday == 1;
	        Bone.SetActive(skull); BoneChest.SetActive(skull);
	        Cane.enabled = !skull;
	        Staff.SetActive(false); Wings.SetActive(false);
	    }
	    if (lava) foreach (var t in Ts) if (Lava != null) t.Default = Lava;
	}
}
