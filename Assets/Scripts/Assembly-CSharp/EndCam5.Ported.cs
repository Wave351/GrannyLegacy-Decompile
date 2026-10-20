using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam5.c (Granny Legacy, IL2CPP x64).
public partial class EndCam5
{
	public void GrannyLaugh()
	{
		AudioSource.PlayClipAtPoint(Laugh, GrannyPoint.position);
	}

	public void Send()
	{
		GO.EndProcess();
	}

	public void Fade()
	{
		GO.Fadin = true;
	}

	public void FadeOut()
	{
		GO.Fadin = false;
	}
}
