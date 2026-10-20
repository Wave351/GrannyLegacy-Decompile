using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerMomCrawl.c (Granny Legacy, IL2CPP x64).
public partial class TriggerMomCrawl
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			EnemyController.StartCrawlingMom();
		}
	}
}
