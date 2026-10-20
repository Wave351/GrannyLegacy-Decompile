using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ExploTrapTrigger.c (Granny Legacy, IL2CPP x64).
public partial class ExploTrapTrigger
{
	private void OnTriggerEnter(Collider o)
	{
		if (o.gameObject.tag == "Player" && !TriggeredBoom)
		{
			TriggeredBoom = true;
			StartCoroutine(TimerExplosion());
		}
	}

	private IEnumerator TimerExplosion()
	{
		float[] waits = { 0.5f, 0.3f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
		foreach (float wait in waits)
		{
			AudioSource.PlayClipAtPoint(BeepWarning, transform.position);
			yield return new WaitForSeconds(wait);
		}
		UnityEngine.Object.Instantiate(ExplosionObj, PointExpl.position, PointExpl.rotation);
		StartCoroutine(WaiterBoom());
	}

	private IEnumerator WaiterBoom()
	{
		yield return new WaitForSeconds(45f);
		TriggeredBoom = false;
	}
}
