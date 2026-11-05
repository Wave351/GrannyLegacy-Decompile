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
			ClearHidingState();
		}
	}

	// Restoration guard: falling out of the map usually happens from a hiding spot (under a bed etc.). Turning the
	// controller off to teleport means the hide triggers never get OnTriggerExit, so the game kept the player "hidden"
	// and Granny/Grandpa chased the saved hiding spot (PlayerPos / PlayerSpot) every time they saw the player.
	private void ClearHidingState()
	{
		foreach (var status in FindObjectsByType<PlayerStatus>(FindObjectsSortMode.None))
		{
			status.HidingUnderThings = false;
			status.HidingInClosets = false;
			status.HidingFromHouseEnemy = false;
		}
		foreach (var fix in FindObjectsByType<HideFix>(FindObjectsSortMode.None)) fix.isPlayerHidden = false;
		Vector3 pos = Player.transform.position;
		foreach (var granny in FindObjectsByType<AI_Granny>(FindObjectsInactive.Include, FindObjectsSortMode.None))
		{
			granny.UnSeenPlayer = false; granny.CanOpenClosets = false;
			granny.PlayerSpot = granny.Player ? granny.Player : Player.transform; granny.PlayerPos = pos;
		}
		foreach (var grandpa in FindObjectsByType<AI_Grandpa>(FindObjectsInactive.Include, FindObjectsSortMode.None))
		{
			grandpa.UnSeenPlayer = false; grandpa.CanOpenClosets = false;
			grandpa.PlayerSpot = grandpa.Player ? grandpa.Player : Player.transform; grandpa.PlayerPos = pos;
		}
	}
}
