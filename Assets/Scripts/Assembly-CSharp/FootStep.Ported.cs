using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FootStep.c (Granny Legacy, IL2CPP x64).
public partial class FootStep
{
	public FootStep() { HollowFinalTimer = 35f; FadeSwitchHollow = 1.5f; }
	private void Start()
	{
		int holiday = PlayerPrefs.GetInt("Holiday", 0);
		if (holiday == 1) { O1 = HGrass1; O2 = HGrass2; O3 = HGrass3; O4 = HGrass4; O5 = HGrass1; }
		else if (holiday == 2) { O1 = Snow1; O2 = Snow2; O3 = Snow3; O4 = Snow4; O5 = Snow1; }
	}
	private void Update()
	{
		// The native FootStep routine checks difficulty 6, even though the menu's Hollow setting is 5.
		if (PlayerPrefs.GetInt("Diff", 0) != 6) return;
		HollowTimer += Time.deltaTime;
		if (HollowTimer >= (IsHollowed ? 7f : HollowFinalTimer))
		{
			HollowTimer = 0f;
			if (!IsHollowed && FeetManager != null && WindBlow != null) FeetManager.PlayOneShot(WindBlow);
			IsHollowed = !IsHollowed;
		}
		Color target = IsHollowed ? FogHollowed : Color.black;
		FogColoring = Color.Lerp(FogColoring, target, Time.deltaTime * FadeSwitchHollow);
		if (Vector4.Distance(FogColoring, target) < 0.001f) FogColoring = target;
		RenderSettings.fogColor = FogColoring;
	}
	public void PlaySound()
	{
		AudioClip[] clips;
		if (House) clips = PlayerPrefs.GetInt("Textures", 0) == 1 ? new[] { W1M, W2M, W3M, W4M, W5M } : new[] { W1, W2, W3, W4, W5 };
		else if (Outside) clips = new[] { O1, O2, O3, O4, O5 };
		else if (Water) clips = new[] { S1, S2, S3, S4 };
		else return;
		var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
		if (FeetManager != null && clip != null) FeetManager.PlayOneShot(clip);
	}
}
