using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GrannyPlankKill.c (Granny Legacy, IL2CPP x64).
public partial class GrannyPlankKill
{
	// Granny/Grandpa walking into the planked spot break the remaining planks once one has fallen
	// (always with "GlitchesAndShit" on).
	private void OnTriggerEnter(Collider other)
	{
	    bool enemy = other.gameObject.CompareTag("granny") || other.gameObject.CompareTag("granpa");
	    int glitches = PlayerPrefs.GetInt("GlitchesAndShit");
	    if (!enemy || (glitches != 0 && glitches != 1)) return;
	    if (glitches == 0 && !Fallen(P1) && !Fallen(P2) && !Fallen(P3)) return;
	    foreach (var p in new[] { P1, P2, P3 })
	        if (p != null && p.GetComponent<Rigidbody>().isKinematic) p.BreakPlank();
	}
	private static bool Fallen(PlankFall p) { return p == null || !p.GetComponent<Rigidbody>().isKinematic; }
}
