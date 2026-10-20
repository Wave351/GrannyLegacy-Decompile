using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RemoteRat.c (Granny Legacy, IL2CPP x64).
public partial class RemoteRat
{
	public void Impact()
	{
		if (HasBeenImpacted)
		{
			return;
		}
		HasBeenImpacted = true;
		foreach (Rigidbody body in RigidBodies)
		{
			body.isKinematic = false;
		}
		if (TheOne)
		{
			DisableNoiseRepos.SetActive(false);
			Doors.Play("DoorsOpenSubBasement");
		}
	}
}
