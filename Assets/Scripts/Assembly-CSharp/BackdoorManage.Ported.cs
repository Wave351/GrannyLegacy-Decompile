using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/BackdoorManage.c (Granny Legacy, IL2CPP x64).
public partial class BackdoorManage
{
	public void LockedAttempt()
	{
		Door.Play("InnerdoorLocked");
		GetComponent<AudioSource>().PlayOneShot(Locked);
	}

	public void OpenDoor()
	{
		Door.Play("InnerdoorOpen");
		name = "YastkIsChased";
		GetComponent<AudioSource>().PlayOneShot(UnlockedOpen);
		StartCoroutine(DoorOpenLockProcess());
	}

	private IEnumerator DoorOpenLockProcess()
	{
		yield return new WaitForSeconds(3f);
		Door.Play("InnerdoorClose");
		GetComponent<AudioSource>().PlayOneShot(CloseDoor);
		yield return new WaitForSeconds(Door["InnerdoorClose"].length);
		name = "Backdoor";
	}
}
