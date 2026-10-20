using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CanvasScaleModify.c (Granny Legacy, IL2CPP x64).
public partial class CanvasScaleModify
{
	public CanvasScaleModify()
	{
		ultrawideAspect = 2f;
	}

	private void Awake()
	{
		CS.matchWidthOrHeight = (float)Screen.width / (float)Screen.height < ultrawideAspect ? 0f : 1f;
	}
}
