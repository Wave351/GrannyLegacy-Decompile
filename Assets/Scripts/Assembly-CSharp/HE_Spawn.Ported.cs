using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HE_Spawn.c (Granny Legacy, IL2CPP x64).
public partial class HE_Spawn
{
	public void EnterF1()
	{
		SetFloor(true, false, false, false);
	}

	public void EnterF2()
	{
		SetFloor(false, true, false, false);
	}

	public void EnterCellar()
	{
		SetFloor(false, false, true, false);
	}

	public void EnterOldH()
	{
		SetFloor(false, false, false, true);
	}

	private void SetFloor(bool floor1, bool floor2, bool cellar, bool oldHouse)
	{
		Floor1 = floor1;
		Floor2 = floor2;
		CellarFloor = cellar;
		OldHouseFloor = oldHouse;
	}
}
