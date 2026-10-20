using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ResetFloorPlayer.c (Granny Legacy, IL2CPP x64).
public partial class ResetFloorPlayer
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player" || Player == null)
		{
			return;
		}
		CharacterController controller = Player.GetComponent<CharacterController>();
		if (controller != null)
		{
			controller.enabled = false;
			Player.transform.position = PositionPlayerToBasement.position;
			controller.enabled = true;
		}
	}
}
