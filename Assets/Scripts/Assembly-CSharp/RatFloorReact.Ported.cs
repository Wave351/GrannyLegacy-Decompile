using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RatFloorReact.c (Granny Legacy, IL2CPP x64).
public partial class RatFloorReact
{
	private void OnTriggerEnter(Collider other)
	{
		string tag = other.gameObject.tag;
		if (tag == "golv" || tag == "Noice")
		{
			AudioSource.PlayClipAtPoint(Scream, transform.position);
			Anim.Play("RatDropped");
		}
	}
}
