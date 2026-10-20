using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ICSB.c (Granny Legacy, IL2CPP x64).
public partial class ICSB
{
	private void Update()
	{
		bool shotgun = ShotgunHand.activeSelf || ShotgunDHand.activeSelf;
		if (ObjectCover.activeSelf)
		{
			if (shotgun)
			{
				ObjectCover.SetActive(false);
			}
		}
		else if (!shotgun)
		{
			ObjectCover.SetActive(true);
		}
	}
}
