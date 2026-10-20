using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerFall.c (Granny Legacy, IL2CPP x64).
public partial class TriggerFall
{
	private void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();
	}

	private void OnTriggerStay(Collider other)
	{
		GameObject obj = other.gameObject;
		if (obj.tag == "Player" || obj.layer == LayerMask.NameToLayer("Item") || obj.tag == "arrow")
		{
			Fall();
		}
		if (other.gameObject.tag == "golv" && IsBumper && !Fell)
		{
			Fall();
		}
	}

	private void Fall()
	{
		if (Fell)
		{
			return;
		}
		if (TriggerSound)
		{
			GetComponent<AudioSource>().PlayOneShot(Falls);
		}
		Fell = true;
		Rigidbody.isKinematic = false;
		Debug.Log("Falling");
	}
}
