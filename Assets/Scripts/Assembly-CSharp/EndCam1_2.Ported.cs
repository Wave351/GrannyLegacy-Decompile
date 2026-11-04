using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam1_2.c (Granny Legacy, IL2CPP x64).
public partial class EndCam1_2
{
	public void GrannyComes()
	{
	    AnimGranny.Play("GameOver_EatingPlayer");
	    AnimGranny["GameOver_EatingPlayer"].speed = 3;
	    Granny.transform.position = EatingPosition.position;
	    Granny.transform.rotation = EatingPosition.rotation;
	    Granny.SetActive(true);
	    foreach (var r in GO.GrannyWPs) r.enabled = false;
	    GO.Creepyteeth.SetActive(true);
	    GO.Oldteeth.SetActive(false);
	}
	public void Send() { GO.EndProcess(); }
	public void Fade() { GO.Fadin = true; }
	public void FadeOut() { GO.Fadin = false; }
	public void GrandpaReposition() { GO.BasementSwitchGrandpaPosition(); }
}
