using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ToggleOn_CertainColor.c (Granny Legacy, IL2CPP x64).
public partial class ToggleOn_CertainColor
{
	private void Start()
	{
	    if (PlayerPrefs.GetInt(ItemColoredPref) != PrefIntSpecific) return;
	    ItemImage.color = SpecificColorChoosen;
	    var toggle = GetComponent<Toggle>();
	    if (toggle) toggle.isOn = true;
	}
	// Rainbow cycle used by the shop's rainbow colour option.
	private static readonly Color[] RainbowFrom = { new Color(0, 1, 1, 1), new Color(1, 0, 0, 1), new Color(1, 0, 1, 1),
	    new Color(1, .921569f, .0156863f, 1), Color.white, new Color(0, 0, 1, 1), new Color(0, 1, 0, 1) };
	private static readonly Color[] RainbowTo = { new Color(1, 0, 0, 1), new Color(1, .9215686f, .01568628f, 1), Color.white,
	    new Color(1, 0, 1, 1), new Color(0, 0, 1, 1), new Color(0, 1, 0, 1), new Color(0, 1, 1, 1) };
	private void Update()
	{
	    var toggle = GetComponent<Toggle>();
	    if (!toggle) return;
	    if (toggle.isOn)
	    {
	        ItemImage.color = SpecificColorChoosen;
	        PlayerPrefs.SetInt(ItemColoredPref, PrefIntSpecific);
	        if (IsRainbow)
	        {
	            TimerRainbow += Time.deltaTime;
	            if (.3f < TimerRainbow)
	            {
	                TimerRainbow = 0;
	                for (int i = 0; i < RainbowFrom.Length; i++)
	                    if (SpecificColorChoosen == RainbowFrom[i]) { SpecificColorChoosen = RainbowTo[i]; break; }
	            }
	        }
	    }
	    if (PlayerPrefs.GetInt(SpecificNameForColorPref) == 1) toggle.interactable = true;
	}
}
