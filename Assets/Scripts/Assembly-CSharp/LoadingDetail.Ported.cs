using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LoadingDetail.c (Granny Legacy, IL2CPP x64).
public partial class LoadingDetail
{
	private void Start()
	{
		StartCoroutine(TimerGo());
	}

	private IEnumerator TimerGo()
	{
		string[] frames = { "Loading.", "Loading..", "Loading...", "Loading.", "Loading..", "Loading..." };
		foreach (string frame in frames)
		{
			Text.text = frame;
			yield return new WaitForSeconds(0.1f);
		}
		StartCoroutine(TimerGo());
	}
}
