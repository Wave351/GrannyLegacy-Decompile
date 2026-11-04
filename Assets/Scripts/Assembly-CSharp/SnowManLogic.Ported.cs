using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SnowManLogic.c (Granny Legacy, IL2CPP x64).
public partial class SnowManLogic
{
	public SnowManLogic() { RotateSpeed = 10; }
	// The snowman's head turns to follow the player while they are near.
	private void Update()
	{
	    if (!WithinArea) return;
	    Vector3 dir = Player.position - SnowManHead.position; dir.y = 0;
	    if (dir.sqrMagnitude <= .001f) return;
	    Vector3 look = Quaternion.LookRotation(dir, Vector3.up).eulerAngles;
	    Quaternion goal = Quaternion.Euler(SnowManHead.rotation.eulerAngles.x, look.y, look.z);
	    SnowManHead.rotation = Quaternion.Slerp(SnowManHead.rotation, goal, Time.deltaTime * RotateSpeed);
	}
	private void RestartTimer()
	{
	    if (DC != null) StopCoroutine(DC);
	    DC = StartCoroutine(DeactivateSnowManStare());
	}
	private void OnTriggerStay(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player")) return;
	    if (!WithinArea) { WithinArea = true; StareSound.Play(); return; }
	    RestartTimer();
	}
	private void OnTriggerExit(Collider other) { if (other.gameObject.CompareTag("Player") && WithinArea) RestartTimer(); }
	private IEnumerator DeactivateSnowManStare() { yield return new WaitForSeconds(.7f); WithinArea = false; DC = null; }
}
