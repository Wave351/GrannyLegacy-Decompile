using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SlidePlateFog.c (Granny Legacy, IL2CPP x64).
public partial class SlidePlateFog
{
	private void Start()
	{
		int diff = PlayerPrefs.GetInt("Diff");
		if (diff != 4 && diff != 5)
		{
			return;
		}
		Renderer renderer = GetComponent<Renderer>();
		if (renderer != null && renderer.material != null)
		{
			renderer.material.shader = ShaderSwitch;
			renderer.material.color = diff == 4 ? SwitchColor1 : SwitchColor2;
		}
	}

	public void SwitchBack()
	{
		Renderer renderer = GetComponent<Renderer>();
		if (renderer != null)
		{
			renderer.material.shader = ShaderReturn;
			renderer.material.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
