using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SprintRemovesItself.c (Granny Legacy, IL2CPP x64).
public partial class SprintRemovesItself
{
	public void DropSprint()
	{
		Rigidbody body = Sprint.GetComponent<Rigidbody>();
		if (body != null)
		{
			if (body.isKinematic)
			{
				body.isKinematic = false;
			}
			PCT.Sprints += 1f;
		}
	}
}
