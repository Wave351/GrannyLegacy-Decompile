using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SaunaNoiseFix.c (Granny Legacy, IL2CPP x64).
public partial class SaunaNoiseFix
{
	private void OnTriggerStay(Collider other)
	{
		string tag = other.gameObject.tag;
		if (tag != "noiseobject" && tag != "noiseobiect")
		{
			return;
		}
		Sauna sauna = Sdeath.Sauna;
		Transform target = !sauna.PutHandle && !sauna.Closed ? RespawnPoint : RespawnPointNo;
		other.gameObject.transform.position = target.position;
	}
}
