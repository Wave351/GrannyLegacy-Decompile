using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MomCrawlHandle.c (Granny Legacy, IL2CPP x64).
public partial class MomCrawlHandle
{
	public void DetachKey()
	{
		if (PlayerPrefs.GetInt("CURVERSION") != 8)
		{
			SpiderKey.SetActive(false);
		}
		SpiderKey.transform.parent = null;
	}

	public void MomOFF()
	{
		MomDisable.SetActive(false);
		EnemyController.PutDownMom = false;
	}
}
