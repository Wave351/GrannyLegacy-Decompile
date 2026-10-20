using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PuzzleDef.c (Granny Legacy, IL2CPP x64).
public partial class PuzzleDef
{
	public PuzzleDef()
	{
		allowedItemNames = new List<string>();
		excludeItemNames = new List<string>();
		Priority = 16;
		requiredItemNames = new List<string>();
		forbiddenCombos = new List<PuzzleForbiddenCombo>();
		containedItems = new List<string>();
	}
}
