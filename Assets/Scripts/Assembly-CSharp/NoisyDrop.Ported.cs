using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/NoisyDrop.c (Granny Legacy, IL2CPP x64).
public partial class NoisyDrop
{
	private void Start()
	{
		OtherNoises = GameObject.FindGameObjectsWithTag(AppliedTagName);
		foreach (GameObject noise in OtherNoises)
		{
			if (noise != gameObject)
			{
				UnityEngine.Object.Destroy(noise);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		string tag = other.gameObject.tag;
		if (tag != "golv" && tag != "grus" && tag != "water" && tag != "Noice")
		{
			return;
		}
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
		{
			body.isKinematic = true;
		}
	}
}
