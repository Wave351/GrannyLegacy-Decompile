using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ScrewdriverCan.c (Granny Legacy, IL2CPP x64).
public partial class ScrewdriverCan
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "arrow" || Hit)
		{
			return;
		}
		Audio.PlayOneShot(Sound);
		Screw.layer = LayerMask.NameToLayer("Item");
		gameObject.layer = LayerMask.NameToLayer("Default");
		Hit = true;
	}
}
