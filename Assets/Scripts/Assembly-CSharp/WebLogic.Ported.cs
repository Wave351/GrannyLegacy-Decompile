using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/WebLogic.c (Granny Legacy, IL2CPP x64).
public partial class WebLogic
{
	public WebLogic()
	{
		ValueWeb = 0.28357f;
		RemoveSpeed = 0.03f;
	}

	private void Update()
	{
		if (Anim.isPlaying)
		{
			return;
		}
		WebT.localScale = new Vector3(ValueWeb, 0.32825f, 0.283567f);
		if (ValueWeb >= 0f)
		{
			return;
		}
		Player = GameObject.Find("Player");
		if (Player != null && Player.GetComponent<MobileFPS>() != null)
		{
			AudioSource.PlayClipAtPoint(RemoveSound, transform.position);
			UnityEngine.Object.Destroy(Anim.gameObject);
		}
	}

	public void RemoveAttempt()
	{
		ValueWeb -= Time.deltaTime * RemoveSpeed;
	}
}
