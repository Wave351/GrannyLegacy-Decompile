using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FanCollider.c (Granny Legacy, IL2CPP x64).
public partial class FanCollider
{
	public void Hurt()
	{
		AudioSource.PlayClipAtPoint(HurtSound, transform.position);
		SE.FanHurt();
		StartCoroutine(FanHand());
		Interactable = false;
	}

	private IEnumerator FanHand()
	{
		yield return new WaitForSeconds(5f);
		Interactable = true;
	}
}
