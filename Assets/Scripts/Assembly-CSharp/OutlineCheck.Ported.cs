using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/OutlineCheck.c (Granny Legacy, IL2CPP x64).
public partial class OutlineCheck
{
	public OutlineCheck() { StringSparkName = "ParticleFX_Sparkle"; CaseLet = true; }
	// Item outline colours by the "Outlines" option (1-10); off in fog, on Extreme+ difficulties, in the dark mode and with the flashlight.
	private static readonly Color[] Colors = {
	    new Color(1, .897329f, .542453f, 1), new Color(1, .33283f, .089623f, 1), new Color(1, 0, 0, 1), new Color(1, 0, .60728f, 1),
	    new Color(.363837f, 0, 1, 1), new Color(.008893f, 0, 1, 1), new Color(0, .936078f, 1, 1), new Color(.155037f, .490566f, .170827f, 1),
	    new Color(.772119f, 1, .29717f, 1), new Color(0, 0, 0, 1) };
	private void Start()
	{
	    int choice = PlayerPrefs.GetInt("Outlines");
	    if (RenderSettings.fog) return;
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (diff == 3 || diff == 4 || diff == 5 || PlayerPrefs.GetInt("Dark") == 1) return;
	    if (PlayerPrefs.GetInt("Outlines") == 0 || PlayerPrefs.GetInt("FlashLightIsOn") == 1) return;
	    var outline = gameObject.AddComponent<Outline>();
	    outline.OutlineMode = Outline.Mode.OutlineVisible;
	    if (choice >= 1 && choice <= 10) outline.OutlineColor = Colors[choice - 1];
	    outline = gameObject.GetComponent<Outline>();
	    if (!outline) return;
	    outline.OutlineWidth = 2;
	    // The outline replaces the item's sparkle effect.
	    for (int i = transform.childCount - 1; i >= 0; i--)
	    {
	        var child = transform.GetChild(i);
	        bool match = CaseLet ? child.name.ToLower().StartsWith(StringSparkName.ToLower()) : child.name.StartsWith(StringSparkName);
	        if (match && child != null) Destroy(child.gameObject);
	    }
	}
}
