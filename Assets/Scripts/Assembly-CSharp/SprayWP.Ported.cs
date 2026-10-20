using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SprayWP.c (Granny Legacy, IL2CPP x64).
public partial class SprayWP
{
	private void Start()
	{
		PlayerStatus = GameObject.Find("PlayerManage");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || PlayerStatus == null)
		{
			return;
		}
		PlayerStatus status = PlayerStatus.GetComponent<PlayerStatus>();
		if (status != null && status.ScreenEffects != null)
		{
			status.ScreenEffects.BlindedAction();
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
