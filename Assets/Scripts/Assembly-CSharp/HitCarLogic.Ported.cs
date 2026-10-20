using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HitCarLogic.c (Granny Legacy, IL2CPP x64).
public partial class HitCarLogic
{
	private void OnTriggerEnter(Collider Obj)
	{
		if (Obj.gameObject.tag == "granny")
		{
			EnemyController ec = Status.GetComponent<EnemyController>();
			if (!ec.GrannyKilled && !ec.Granny.GetComponent<AI_Granny>().CaughtPlayer)
			{
				ec.GrannyDeath(false);
			}
		}
		if (Obj.gameObject.tag != "granpa" || Status == null)
		{
			return;
		}
		EnemyController controller = Status.GetComponent<EnemyController>();
		if (controller == null || controller.GrandpaKilled || controller.Grandpa == null)
		{
			return;
		}
		AI_Grandpa grandpa = controller.Grandpa.GetComponent<AI_Grandpa>();
		if (grandpa != null && !grandpa.CaughtPlayer)
		{
			controller.GrandpaDeath(false);
		}
	}
}
