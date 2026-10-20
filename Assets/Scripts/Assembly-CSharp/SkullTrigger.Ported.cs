using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SkullTrigger.c (Granny Legacy, IL2CPP x64).
public partial class SkullTrigger
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag != "Player" || !sb.barfing)
		{
			return;
		}
		BoxCollider box = GetComponent<BoxCollider>();
		if (box != null && box.enabled)
		{
			sb.BarfScreenOccur();
			box.enabled = false;
		}
	}
}
