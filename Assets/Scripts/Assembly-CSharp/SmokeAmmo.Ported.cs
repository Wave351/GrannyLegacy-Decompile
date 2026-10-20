using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SmokeAmmo.c (Granny Legacy, IL2CPP x64).
public partial class SmokeAmmo
{
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1f);
		canGoOff = true;
	}

	private void Update()
	{
		if (canGoOff && rigid.velocity.magnitude < 0.1f && !e.isStopped)
		{
			ParticleSystem.MainModule main = e.main;
			main.loop = false;
		}
	}
}
