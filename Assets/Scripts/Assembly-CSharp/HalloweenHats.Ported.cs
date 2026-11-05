using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HalloweenHats.c (Granny Legacy, IL2CPP x64).
public partial class HalloweenHats
{
	private void Start() { Apply(); }
	private void Update() { if (ConstantlyChecking) Apply(); }
	// Holiday hats: 1 Halloween, 2 Christmas (the renderer itself), 3 Valentine's, 4 March, 5 Easter, 6 none, 7 beach.
	private void Apply()
	{
	    int h = PlayerPrefs.GetInt("Holiday");
	    if (h < 0 || h > 7) return;
	    DisRenderer.enabled = h == 2;
	    HalloweenObject.SetActive(h == 1);
	    ValentineObject.SetActive(h == 3);
	    MarchObject.SetActive(h == 4);
	    EasterObject.SetActive(h == 5);
	    BeachObject.SetActive(h == 7);
	}
}
