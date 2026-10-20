using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ScrewDart.c (Granny Legacy, IL2CPP x64).
public partial class ScrewDart
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "arrow" || Hit)
		{
			return;
		}
		gameObject.layer = LayerMask.NameToLayer("Item");
		Can.layer = LayerMask.NameToLayer("Default");
		Hit = true;
	}

	public void processKnocked()
	{
		gameObject.layer = LayerMask.NameToLayer("Item");
		if (Can != null)
		{
			Can.layer = LayerMask.NameToLayer("Default");
		}
		Hit = true;
	}
}
