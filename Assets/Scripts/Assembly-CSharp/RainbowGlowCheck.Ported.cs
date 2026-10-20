using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RainbowGlowCheck.c (Granny Legacy, IL2CPP x64).
public partial class RainbowGlowCheck
{
	private void Update()
	{
		if (PlayerPrefs.GetInt(ItemColoredPref) != 10 || IsActive)
		{
			return;
		}
		if (OnlyParent)
		{
			IsActive = true;
			RainbowGlow.SetActive(true);
			return;
		}
		GameObject glow = UnityEngine.Object.Instantiate(RainbowGlow, transform.position, transform.rotation);
		UnityEngine.Animations.ParentConstraint constraint = glow.GetComponent<UnityEngine.Animations.ParentConstraint>();
		if (constraint != null && constraint.sourceCount > 0)
		{
			UnityEngine.Animations.ConstraintSource source = constraint.GetSource(0);
			source.sourceTransform = transform;
			source.weight = 1f;
			constraint.SetSource(0, source);
			constraint.constraintActive = true;
			constraint.locked = true;
		}
		IsActive = true;
	}
}
