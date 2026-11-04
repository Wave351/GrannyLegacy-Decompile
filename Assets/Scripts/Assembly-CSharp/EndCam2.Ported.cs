using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam2.c (Granny Legacy, IL2CPP x64).
public partial class EndCam2
{
	public void GrannyComes() { Granny.CrossFade("GameOver_GuillotineStare"); }
	public void GuillotineAction()
	{
	    var g = GAnim;
	    Instantiate(g.NoiseOBJ, g.CutArea.transform.position, g.CutArea.transform.rotation);
	    AudioSource.PlayClipAtPoint(g.SoundGuillotine, g.Gj.transform.position);
	    g.anim.Play("giljotinOn");
	    g.StartCoroutine(g.WaitForAnimationEnd1());
	}
	public void GrannyPull() { Granny.CrossFade("GameOver_GuillotinePull"); }
	public void Send() { GO.EndProcess(); }
	public void Fade() { GO.Fadin = true; }
	public void FadeOut() { GO.Fadin = false; }
}
