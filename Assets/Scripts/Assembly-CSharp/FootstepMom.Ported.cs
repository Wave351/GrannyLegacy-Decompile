using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FootstepMom.c (Granny Legacy, IL2CPP x64).
public partial class FootstepMom
{
	public void Step()
	{
		Audio.PlayOneShot(Steps[UnityEngine.Random.Range(0, Steps.Length)]);
	}
}
