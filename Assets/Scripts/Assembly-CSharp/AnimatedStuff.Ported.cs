using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AnimatedStuff.c (Granny Legacy, IL2CPP x64).
public partial class AnimatedStuff
{
	public AnimatedStuff() { OpeningTimer = 0.5f; ClosingTimer = 0.5f; CanBeLayer = true; EnabledAnim = true; }
	public void OpenAction()
	{
		if (AnimatedObject != null && OpenAnimation != null) AnimatedObject.Play(OpenAnimation.name);
		if (CanBeLayer) SetAnimationLayer("Door");
		gameObject.name = "ProcessState";
		if (Audio != null && OpenSound != null) Audio.PlayOneShot(OpenSound);
		StartCoroutine(OpeningAction());
	}
	public void CloseAction()
	{
		if (AnimatedObject != null && CloseAnimation != null) AnimatedObject.Play(CloseAnimation.name);
		if (CanBeLayer) SetAnimationLayer("Door");
		gameObject.name = "ProcessState";
		if (Audio != null && CloseSound != null) Audio.PlayOneShot(CloseSound);
		StartCoroutine(ClosingAction());
	}
	public void AttemptClose() { if (gameObject.name == OpenName) CloseAction(); }
	private IEnumerator OpeningAction()
	{
		yield return new WaitForSeconds(OpeningTimer);
		if (CanBeLayer && !IfLockKyl) SetAnimationLayer("Non-Ground");
		gameObject.name = OpenName;
	}
	private IEnumerator ClosingAction()
	{
		yield return new WaitForSeconds(ClosingTimer);
		if (CanBeLayer) SetAnimationLayer("Non-Ground");
		gameObject.name = ClosedName;
	}
	private void SetAnimationLayer(string layer) { int index = LayerMask.NameToLayer(layer); if (index >= 0) gameObject.layer = index; }
}
