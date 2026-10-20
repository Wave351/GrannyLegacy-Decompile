using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ParentStatusObject.c (Granny Legacy, IL2CPP x64).
public partial class ParentStatusObject
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("CURVERSION") != VersionPlayerPrefs1 || PlayerPrefs.GetInt("FlashL") != 0)
		{
			return;
		}
		if (!Assign)
		{
			o.parent = null;
			return;
		}
		UnityEngine.Animations.ParentConstraint constraint = o.GetComponent<UnityEngine.Animations.ParentConstraint>();
		if (constraint != null)
		{
			UnityEngine.Object.Destroy(constraint);
		}
		o.parent = ParentAssign;
	}
}
