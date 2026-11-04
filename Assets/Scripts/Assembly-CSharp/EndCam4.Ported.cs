using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam4.c (Granny Legacy, IL2CPP x64).
public partial class EndCam4
{
	private void Start() { (PlayerPrefs.GetInt("Textures") == 1 ? BearTrapNM : BearTrap).SetActive(true); }
	public void BearTrapTrigger()
	{
	    var a = BearTrap.GetComponent<Animation>(); if (!a) return;
	    a.Play("Beartrap");
	    var s = BearTrap.GetComponent<AudioSource>(); if (!s) return;
	    s.PlayOneShot(C1);
	    a = BearTrapNM.GetComponent<Animation>(); if (!a) return;
	    a.Play("Beartrap");
	    s = BearTrapNM.GetComponent<AudioSource>(); if (!s) return;
	    s.PlayOneShot(C2);
	}
	public void SoundPlay() { Slap.Play(); }
	public void GrannyLaugh() { AudioSource.PlayClipAtPoint(Laugh, transform.position); }
	public void Send() { GO.EndProcess(); }
	public void Fade() { GO.Fadin = true; }
	public void FadeOut() { GO.Fadin = false; }
	public void AtticFanNoiseBegone() { AtticNoise.SetActive(false); }
	public void GrandpaShootEnd() { if (PlayerPrefs.GetInt("Grandpa") == 2) GrandpaAnim.CrossFade("shootGun"); }
}
