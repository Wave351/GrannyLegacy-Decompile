using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ChargeBulb.c (Granny Legacy, IL2CPP x64).
public partial class ChargeBulb
{
	private void Start()
	{
		Lamp.material.color = new Color(1f, 0f, 0f, 1f);
	}

	public void ZapIT()
	{
		Zap.SetActive(true);
		Lamp.material.color = new Color(0f, 1f, 0f, 1f);
	}
}
