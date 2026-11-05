using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TeddyHunt.c (Granny Legacy, IL2CPP x64).
public partial class TeddyHunt
{
	public TeddyHunt() { fadeSpeed = .5f; NeedPlay = true; }
	private void Start()
	{
	    audioSource.loop = true;
	    audioSource.volume = 0;
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (diff == 3 || diff == 4 || diff == 5) { GrannyEyesNormal = GrannyEyesSetDark; GrandpaEyesNormal = GrandpaEyesSetDark; }
	    GrannyEyeM.material.color = GrannyEyesNormal;
	    GrandpaEyeM.material.color = GrandpaEyesNormal;
	}
	public void StopAll() { SupposedToFade = false; }
	public void StopEnding() { SupposedToFade = false; AlreadyHunted = false; TimerGone = 5; TeddyH.SetActive(false); }
	// While the teddy is out (or AlwaysAngry), every enemy is angry with red eyes and the hunt music plays;
	// 5 seconds after it is put away they calm down.
	private void Update()
	{
	    if (!SupposedToFade)
	    {
	        audioSource.volume -= fadeSpeed * Time.deltaTime;
	        if (NeedPlay && audioSource.isPlaying && audioSource.volume == 0) audioSource.Stop();
	    }
	    else
	    {
	        if (audioSource.volume < .7f) audioSource.volume += fadeSpeed * Time.deltaTime;
	        if (NeedPlay && !audioSource.isPlaying) audioSource.Play();
	    }
	    if (AlwaysAngry1 || AlwaysAngry2)
	    {
	        SupposedToFade = true; AlreadyHunted = true;
	        if (AlwaysAngry1) { Granny.IsAngry = true; GrannyEyeM.material.color = GrannyEyesAngry; }
	        if (AlwaysAngry2) { GrandpaEyeM.material.color = GrandpaEyesAngry; Grandpa.IsAngry = true; }
	        TimerGone = 0;
	        return;
	    }
	    if (TeddyH.activeSelf)
	    {
	        SupposedToFade = true; AlreadyHunted = true;
	        GrannyEyeM.material.color = GrannyEyesAngry; GrandpaEyeM.material.color = GrandpaEyesAngry;
	        Granny.IsAngry = true; Grandpa.IsAngry = true;
	        if (AI_Mom.gameObject.activeSelf) AI_Mom.IsAngry = true;
	        TimerGone = 0;
	        return;
	    }
	    if (AlreadyHunted) TimerGone += Time.deltaTime;
	    if (TimerGone < 5) return;
	    TimerGone = 0;
	    if (AI_Mom.gameObject.activeSelf) { AI_Mom.IsAngry = false; if (!AI_Mom.Eyes.Seeing) AI_Mom.StopChase(); }
	    Granny.IsAngry = false; if (!Granny.EnemyVision.Seeing) Granny.StopChase();
	    Grandpa.IsAngry = false; if (!Grandpa.EnemyVision.Seeing) Grandpa.StopChase();
	    GrannyEyeM.material.color = GrannyEyesNormal;
	    GrandpaEyeM.material.color = GrandpaEyesNormal;
	    SupposedToFade = false; AlreadyHunted = false;
	}
}
