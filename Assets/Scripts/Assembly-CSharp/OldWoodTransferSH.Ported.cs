using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/OldWoodTransferSH.c (Granny Legacy, IL2CPP x64).
public partial class OldWoodTransferSH
{
	private void Start()
	{
		Renderer renderer = GetComponent<Renderer>();
		if (renderer != null)
		{
			renderer.material.shader = Shader.Find("Standard");
			UnityEngine.Object.Destroy(GetComponent<OldWoodTransferSH>());
		}
	}
}
