using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CompartmentItemSetup.c (Granny Legacy, IL2CPP x64).
public partial class CompartmentItemSetup
{
	private void Start()
	{
		StartCoroutine(SetItOff());
	}

	private IEnumerator SetItOff()
	{
		yield return new WaitForSeconds(5f);
		PlacedItemIn = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (PlacedItemIn || other.gameObject.layer != LayerMask.NameToLayer("Item"))
		{
			return;
		}
		bool match = false;
		foreach (string tag in SpecificTags)
		{
			if (other.gameObject.GetComponent<ItemSeedData>().itemName == tag)
			{
				match = true;
			}
		}
		if (match)
		{
			PlacedItemIn = true;
			other.transform.SetParent(transform);
			return;
		}
		other.transform.position = RedirectPosition.position;
		Rigidbody body = other.transform.GetComponent<Rigidbody>();
		if (body != null)
		{
			body.isKinematic = false;
		}
	}
}
