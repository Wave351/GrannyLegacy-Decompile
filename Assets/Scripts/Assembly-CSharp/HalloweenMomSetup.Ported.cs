using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HalloweenMomSetup.c (Granny Legacy, IL2CPP x64).
public partial class HalloweenMomSetup
{
	// Holiday: 1 Halloween, 2 Christmas, 3 Valentine's, 4 March, 5 Easter, 6 (no extra decorations), 7 June.
	private void Start()
	{
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    if (holiday == 7) { CA.SetActive(false); June.SetActive(true); }
	    else if (holiday == 6) CA.SetActive(false);
	    else if (holiday == 5)
	    {
	        CA.SetActive(false); Easter.SetActive(true);
	        int diff = PlayerPrefs.GetInt("Diff");
	        if (diff == 3 || diff == 4 || diff == 5) EasterEggs.SetActive(false);
	    }
	    else if (holiday == 4) { CA.SetActive(false); March.SetActive(true); }
	    else if (holiday == 3) { CA.SetActive(false); Valentines.SetActive(true); }
	    else if (holiday == 2) { AI.MomNormal = MomCH; AI.MomAngry = MomCHAngry; }
	    else if (holiday == 1)
	    {
	        AI.MomNormal = MomHalloween; AI.MomAngry = MomHalloweenAngry;
	        CA.SetActive(false);
	        foreach (var t in Ts) t.Default = Halloween;
	        HalloweenWebs.SetActive(true);
	    }
	    int grandpa = PlayerPrefs.GetInt("Grandpa");
	    GrandpaGingerBread.SetActive(grandpa == 1 || grandpa == 2);
	    if (SEM.lavaAtmosphereOn) { AI.MomNormal = MomLava; AI.MomAngry = MomLavaAngry; }
	}
}
