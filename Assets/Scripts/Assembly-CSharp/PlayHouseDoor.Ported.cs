using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PlayHouseDoor.c (Granny Legacy, IL2CPP x64).
public partial class PlayHouseDoor
{
	public PlayHouseDoor()
	{
		AttemptOpen = true;
	}

	public void AttemptLock()
	{
		AttemptOpen = false;
		GetComponent<Animation>().Play("InnerdoorLocked_2");
		GetComponent<AudioSource>().PlayOneShot(LekhusLocked);
		StartCoroutine(LockedAnim());
	}

	public IEnumerator LockedAnim()
	{
		yield return new WaitForSeconds(0.5f);
		AttemptOpen = true;
	}
}
