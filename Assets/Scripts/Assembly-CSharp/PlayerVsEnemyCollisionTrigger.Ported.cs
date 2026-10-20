using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PlayerVsEnemyCollisionTrigger.c (Granny Legacy, IL2CPP x64).
public partial class PlayerVsEnemyCollisionTrigger
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			SetIgnore(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			SetIgnore(false);
		}
	}

	public void TurnBackOn()
	{
		SetIgnore(false);
	}

	private void SetIgnore(bool ignore)
	{
		foreach (Collider avoid in collidersAvoid)
		{
			Physics.IgnoreCollision(player, avoid, ignore);
		}
	}
}
