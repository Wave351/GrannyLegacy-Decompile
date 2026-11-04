using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ColorVarietyItems.c (Granny Legacy, IL2CPP x64).
public partial class ColorVarietyItems
{
	// ItemColoredPref: 0 = default white (or the "Textures" alternative), 1-10 = shop colours.
	private void Update()
	{
	    int c = PlayerPrefs.GetInt(ItemColoredPref);
	    if (c == 0)
	    {
	        if (PlayerPrefs.GetInt("Textures") != 0) { Material.mainTexture = NM; return; }
	        Material.mainTexture = White;
	        Destroy(GetComponent<ColorVarietyItems>());
	        return;
	    }
	    var textures = new[] { Brown, Grey, Blue, Green, Orange, Yellow, Red, Purple, Pink, Rainbow };
	    if (c >= 1 && c <= 10) Material.mainTexture = textures[c - 1];
	}
}
