using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Animations;

// Ported from Annotated-Pseudocode/SelfDestructGlow.c (Granny Legacy, IL2CPP x64).
public partial class SelfDestructGlow
{
	private void Update()
	{
		if (PC != null && PC.sourceCount != 0)
		{
			Transform source = PC.GetSource(0).sourceTransform;
			if (source != null && source.gameObject.activeInHierarchy)
			{
				return;
			}
			if (source != null)
			{
				RainbowGlowCheck glow = source.gameObject.GetComponent<RainbowGlowCheck>();
				if (glow != null)
				{
					glow.IsActive = false;
				}
			}
		}
		UnityEngine.Object.Destroy(gameObject);
	}
}
