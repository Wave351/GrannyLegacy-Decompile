using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FallZeroCheck.c (Granny Legacy, IL2CPP x64).
public partial class FallZeroCheck
{
	public FallZeroCheck()
	{
		MaxTimerBack = 1f;
	}

	private void Update()
	{
		if (!PlayerObj.activeSelf)
		{
			InArea = false;
			TimerBack = 0f;
		}
		if (InArea && PlayerObj.activeSelf)
		{
			FallingHolder holder = PlayerObj.GetComponent<FallingHolder>();
			if (holder != null && !holder.isFalling)
			{
				OnCloset = true;
			}
			return;
		}
		if (OnCloset)
		{
			TimerBack += Time.deltaTime;
			if (TimerBack > MaxTimerBack)
			{
				TimerBack = 0f;
				OnCloset = false;
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			InArea = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			InArea = false;
		}
	}
}
