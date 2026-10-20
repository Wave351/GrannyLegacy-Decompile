using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemSetAsChild.c (Granny Legacy, IL2CPP x64).
public partial class ItemSetAsChild
{
	private void Start()
	{
		StartCoroutine(SetItOff());
	}

	private IEnumerator SetItOff()
	{
		yield return new WaitForSeconds(5f);
		PlacedItemIn = true;
		if (CodeOnly)
		{
			Anim.Play(CloseDoor.name);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (PlacedItemIn || other.gameObject.layer != LayerMask.NameToLayer("Item"))
		{
			return;
		}
		if (!CodeOnly)
		{
			PlacedItemIn = true;
			other.transform.SetParent(transform);
			return;
		}
		ItemSeedData item = other.gameObject.GetComponent<ItemSeedData>();
		if (item != null && item.itemName == "Code")
		{
			PlacedItemIn = true;
			other.transform.SetParent(Anim.gameObject.transform);
			Anim.Play(CloseDoor.name);
		}
	}
}
