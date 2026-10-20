using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FurnitureUnit.c (Granny Legacy, IL2CPP x64).
public partial class FurnitureUnit
{
	private void Start()
	{
		InitialPosition = transform.position;
		InitialRotation = transform.rotation;
	}

	public void CleanUpFurniture()
	{
		transform.position = InitialPosition;
		transform.rotation = InitialRotation;
		if (!WithRigidbody)
		{
			gameObject.layer = LayerMask.NameToLayer("Moves");
			return;
		}
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
		{
			body.isKinematic = true;
			TriggerFall fall = GetComponent<TriggerFall>();
			if (fall != null)
			{
				fall.Fell = false;
			}
		}
	}
}
