using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Cauldron.c (Granny Legacy, IL2CPP x64).
public partial class Cauldron
{
	private void OnTriggerEnter(Collider other)
	{
		ItemSeedData item = other.gameObject.GetComponent<ItemSeedData>();
		if (item && item.itemName == "Rat" && !SpectralGranny.activeSelf)
		{
			MagicSpawn.SetActive(true);
			SpectralGranny.SetActive(true);
			UnityEngine.Object.Destroy(other.gameObject);
		}
		item = other.gameObject.GetComponent<ItemSeedData>();
		if (item && item.itemName == "Gas")
		{
			UnityEngine.Object.Instantiate(Explosion, other.transform.position, other.transform.rotation);
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}
}
