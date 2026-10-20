using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/P_hitGrandpa.c (Granny Legacy, IL2CPP x64).
public partial class P_hitGrandpa
{
	public void HitAttempt()
	{
		if (!Grandpa.PepperedEnemy && !Grandpa.IsDying && !Grandpa.CaughtPlayer)
		{
			Grandpa.PepperHit();
		}
	}
}
