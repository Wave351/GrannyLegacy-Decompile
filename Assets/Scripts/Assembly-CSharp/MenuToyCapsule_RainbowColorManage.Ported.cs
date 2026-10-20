using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MenuToyCapsule_RainbowColorManage.c (Granny Legacy, IL2CPP x64).
public partial class MenuToyCapsule_RainbowColorManage
{
	private void Update()
	{
		Texture texture = Renderer.material.mainTexture;
		RainbowGlow.SetActive(texture != null && texture.name.StartsWith(SpecificName));
	}
}
