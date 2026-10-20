using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CowlegRuin.c (Granny Legacy, IL2CPP x64).
public partial class CowlegRuin
{
	public void RuinMeat()
	{
		RuinParts.SetActive(true);
		RuinParts.transform.parent = null;
		UnityEngine.Object.Instantiate(EffectRuin, RuinParts.transform);
		UnityEngine.Object.Destroy(gameObject);
	}
}
