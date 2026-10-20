using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FreeSpotRule.c (Granny Legacy, IL2CPP x64).
public partial class FreeSpotRule
{
	public FreeSpotRule()
	{
		itemsWithinMax = 2f;
		linkedSpots = new FreeSpotRule[0];
	}
}
