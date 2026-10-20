using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/NoiseDropCheck.c (Granny Legacy, IL2CPP x64).
public partial class NoiseDropCheck
{
	private void OnTriggerEnter(Collider other)
	{
		string tag = other.gameObject.tag;
		if ((tag == "noiseobject" || tag == "noiseobiect") && CanRepos)
		{
			other.gameObject.transform.position = RespawnPoint.position;
		}
	}
}
