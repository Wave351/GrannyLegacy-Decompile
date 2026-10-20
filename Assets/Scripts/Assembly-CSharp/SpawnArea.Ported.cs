using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpawnArea.c (Granny Legacy, IL2CPP x64).
public partial class SpawnArea
{
	public SpawnArea()
	{
		maxItems = 1;
		usedSpots = new List<Transform>();
		allowedItemNames = new List<string>();
	}
}
