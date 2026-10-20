using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AtticDoorManage.c (Granny Legacy, IL2CPP x64).
public partial class AtticDoorManage
{
	public void LockedAttempt()
	{
		Door.Play("InnerdoorLocked_1");
		GetComponent<AudioSource>().PlayOneShot(Locked);
	}

	public void OpenDoor()
	{
		Door.Play("InnerdoorOpen_1");
		name = "YastkIsChased";
		GetComponent<AudioSource>().PlayOneShot(UnlockedOpen);
		StartCoroutine(DoorOpenLockProcess());
	}

	private IEnumerator DoorOpenLockProcess()
	{
		yield return new WaitForSeconds(3f);
		Door.Play("InnerdoorClose_1");
		GetComponent<AudioSource>().PlayOneShot(CloseDoor);
		yield return new WaitForSeconds(Door["InnerdoorClose_1"].length);
		name = "InnerdoorVind";
	}
}
