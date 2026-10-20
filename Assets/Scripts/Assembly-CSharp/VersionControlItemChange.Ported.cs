using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/VersionControlItemChange.c (Granny Legacy, IL2CPP x64).
public partial class VersionControlItemChange
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashL") != 0)
		{
			return;
		}
		int version = PlayerPrefs.GetInt("CURVERSION");
		if (version != VersionPlayerPrefs1 && version != VersionPlayerPrefs2 && version != VersionPlayerPrefs3)
		{
			return;
		}
		foreach (Transform item in ObjectsToReposition)
		{
			item.transform.position = transform.position;
			item.transform.rotation = transform.rotation;
		}
	}
}
