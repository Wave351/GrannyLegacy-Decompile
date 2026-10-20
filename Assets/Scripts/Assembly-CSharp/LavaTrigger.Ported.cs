using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LavaTrigger.c (Granny Legacy, IL2CPP x64).
public partial class LavaTrigger
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || PS.Killed || PS.IsJumpscared)
		{
			return;
		}
		PS.NormalDeath();
		if (L != null)
		{
			AudioSource source = L.GetComponent<AudioSource>();
			if (source != null)
			{
				source.PlayOneShot(lavaDieSound);
				FH.TestStop();
				FH.isFalling = false;
			}
		}
	}
}
