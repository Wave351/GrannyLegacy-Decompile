using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam1_1.c (Granny Legacy, IL2CPP x64).
public partial class EndCam1_1
{
	public void BloodImpactInitial()
	{
		BloodImpact1.SetActive(true);
	}

	public void BloodOnTheGroundAppears()
	{
		BloodOnGroundNear.SetActive(true);
	}

	public void BloodSc()
	{
		BloodScreen.SetActive(true);
		BloodImpact2.SetActive(true);
	}

	public void CamSwitch()
	{
		Cam2.SetActive(true);
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
