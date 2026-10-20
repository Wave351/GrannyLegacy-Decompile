using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PlankFall.c (Granny Legacy, IL2CPP x64).
public partial class PlankFall
{
	public void BreakPlank()
	{
		Rigidbody body = GetComponent<Rigidbody>();
		if (body == null)
		{
			return;
		}
		body.isKinematic = false;
		AudioSource.PlayClipAtPoint(BreakSound, transform.position);
		gameObject.AddComponent<OutlineCheck>();
		gameObject.name = "BrokePlank";
		gameObject.AddComponent<ItemSeedData>();
		GetComponent<ItemSeedData>().itemName = "Plank";
		gameObject.layer = LayerMask.NameToLayer("Item");
	}
}
