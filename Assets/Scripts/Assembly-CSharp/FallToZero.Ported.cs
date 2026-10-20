using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FallToZero.c (Granny Legacy, IL2CPP x64).
public partial class FallToZero
{
	public FallToZero()
	{
		Working = true;
		DurationMaxedOut = 0.35f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || !FZC.OnCloset || !Working)
		{
			return;
		}
		FH.DurateCan = DurationMaxedOut;
		FH.fallDuration = DurationMaxedOut;
		Working = false;
		StartCoroutine(TimerFunction());
	}

	private IEnumerator TimerFunction()
	{
		yield return new WaitForSeconds(1f);
		Working = true;
	}
}
