using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/DestroyTimer.c (Granny Legacy, IL2CPP x64).
public partial class DestroyTimer
{
	public DestroyTimer()
	{
		TimeW = 10f;
	}

	private void Start()
	{
		StartCoroutine(TimerGoes());
	}

	private IEnumerator TimerGoes()
	{
		yield return new WaitForSeconds(TimeW);
		if (transform.parent != null)
		{
			UnityEngine.Object.Destroy(transform.parent.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
