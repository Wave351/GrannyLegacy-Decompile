using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpawnItemVariety.c (Granny Legacy, IL2CPP x64).
public partial class SpawnItemVariety
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("VARIETYOFF") != 1)
		{
			return;
		}
		int index = UnityEngine.Random.Range(0, RepositionSpots.Length);
		transform.position = RepositionSpots[index].position;
		transform.rotation = RepositionSpots[index].rotation;
		UnityEngine.Object.Destroy(GetComponent<SpawnItemVariety>());
		// The shipped code then snaps to spot 0 whenever the array is non-empty (Destroy is deferred).
		if (RepositionSpots.Length != 0)
		{
			transform.position = RepositionSpots[0].position;
			transform.rotation = RepositionSpots[0].rotation;
		}
		UnityEngine.Object.Destroy(GetComponent<SpawnItemVariety>());
	}
}
