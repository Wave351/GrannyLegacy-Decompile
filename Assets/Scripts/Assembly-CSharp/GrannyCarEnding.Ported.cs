using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GrannyCarEnding.c (Granny Legacy, IL2CPP x64).
public partial class GrannyCarEnding
{
	public GrannyCarEnding()
	{
		SpeedWalk = 1.7f;
	}

	public void GrannyWalk()
	{
		GrannyAnim.Play("Granny_Wander");
		GrannyAnim["Granny_Wander"].speed = SpeedWalk;
	}

	public void EndGrannyWalk()
	{
		GrannyAnim.CrossFade("idle_3", 0.8f);
	}
}
