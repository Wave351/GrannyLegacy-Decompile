using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpiderAggro.c (Granny Legacy, IL2CPP x64).
public partial class SpiderAggro
{
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && !AtticSpider.isEating && !AtticSpider.MeatGoes && !StopAggro)
		{
			AtticSpider.GetMad();
		}
	}
}
