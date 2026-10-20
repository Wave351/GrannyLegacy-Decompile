using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Ornament_Bomb.c (Granny Legacy, IL2CPP x64).
public partial class Ornament_Bomb
{
	private void OnCollisionEnter(Collision other)
	{
		if (!OrnamentHits.IsBreakSurface(other.gameObject) || Started)
		{
			return;
		}
		Started = true;
		AudioSource.PlayClipAtPoint(TimerBombAnnounce, transform.position);
		StartCoroutine(TimerTick());
	}

	private IEnumerator TimerTick()
	{
		yield return new WaitForSeconds(3f);
		UnityEngine.Object.Instantiate(Explosion, transform.position, transform.rotation);
		UnityEngine.Object.Destroy(gameObject);
	}
}
