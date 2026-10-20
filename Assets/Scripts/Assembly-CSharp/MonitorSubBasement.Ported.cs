using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MonitorSubBasement.c (Granny Legacy, IL2CPP x64).
public partial class MonitorSubBasement
{
	public void HitSwitch()
	{
		bool on = !CameraTarget.activeSelf;
		CameraTarget.SetActive(on);
		Screen.enabled = on;
		Audio.Play();
	}
}
