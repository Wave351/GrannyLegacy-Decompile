using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/UndeadBunnyAnimator.c (Granny Legacy, IL2CPP x64).
public partial class UndeadBunnyAnimator
{
	public void Launch()
	{
		UB.RandomWaypoint();
	}

	public void Stop()
	{
		UB.StopTravel();
	}
}
