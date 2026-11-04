using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Wings.c (Granny Legacy, IL2CPP x64).
public partial class Wings
{
	public Wings() { SpeedWingEnergy = .5f; }
	private void StopAll() { foreach (var p in Electricity) p.Stop(); }
	private void Update()
	{
	    if (!AI.enabled || !AI.IsChasing)
	    {
	        if (!AI.enabled) Anim.CrossFade("Wing_Idle");
	        if (FlapOnce) { FlapOnce = false; Anim.CrossFade("Wing_Close"); }
	        if (!Anim.IsPlaying("Wing_Close")) Anim.CrossFade("Wing_Idle");
	        StopAll();
	        return;
	    }
	    if (!FlapOnce)
	    {
	        FlapOnce = true;
	        Anim.CrossFade("Wing_Open");
	        foreach (var p in Electricity) p.Play();
	    }
	    OffSetWing.x += Time.deltaTime * SpeedWingEnergy;
	    OffSetWing.y += Time.deltaTime * SpeedWingEnergy;
	    Wing1.materials[1].mainTextureOffset = OffSetWing;
	    Wing2.materials[1].mainTextureOffset = OffSetWing;
	}
	public void OnWings() { foreach (var p in Electricity) { if (p == null) break; p.Play(); FlapOnce = true; } }
	public void OffWings() { foreach (var p in Electricity) { if (p == null) break; p.Stop(); FlapOnce = false; } }
}
