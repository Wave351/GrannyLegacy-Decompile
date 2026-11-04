using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Elevator.c (Granny Legacy, IL2CPP x64).
public partial class Elevator
{
	public void AttemptUseElevator()
	{
	    if (ElevatorDoor.isPlaying || !ClosedLift) return;
	    StartCoroutine(IsUp ? ElevatorDown() : ElevatorUp());
	}
	public void DownNow() { StartCoroutine(ElevatorDown()); }
	private IEnumerator Ride(string clip, bool up)
	{
	    ElevatorAnim.Play(clip);
	    if (HalloweenThemed) HissSound.clip = HalloweenThemesElevator[UnityEngine.Random.Range(0, HalloweenThemesElevator.Length)];
	    HissSound.Play();
	    yield return new WaitForSeconds(ElevatorAnim[clip].length);
	    IsUp = up;
	}
	private IEnumerator ElevatorUp() { return Ride("HissUpp", true); }
	private IEnumerator ElevatorDown() { return Ride("HissNer", false); }
	public void OpenElevator() { ElevatorDoor.Play("ElevatorDoorsOpen"); Audio.PlayOneShot(DoorsOpen); ClosedLift = false; }
	public void CloseElevator() { ElevatorDoor.Play("ElevatorDoorsClose"); Audio.PlayOneShot(DoorsClose); ClosedLift = true; }
}
