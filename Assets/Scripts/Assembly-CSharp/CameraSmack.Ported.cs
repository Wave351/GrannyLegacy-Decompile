using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CameraSmack.c (Granny Legacy, IL2CPP x64).
public partial class CameraSmack
{
	public void SmackCam()
	{
		Smacked.SetActive(true);
		SmackComponents.SetActive(true);
		Smacked.GetComponent<Animation>().Play("CameraBroken");
		OldTrigger.GetComponent<Collider>().enabled = false;
		ScreenMonitor.SetActive(false);
		Camera.SetActive(false);
	}
}
