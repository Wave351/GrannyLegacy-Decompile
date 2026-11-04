using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AtticSpiderButton.c (Granny Legacy, IL2CPP x64).
public partial class AtticSpiderButton
{
	public void StopSpider()
	{
	    if (SpiderAggro.StopAggro || AtticSpider.IsTargetingPlayer || AtticSpider.IsKilling) return;
	    SpiderAggro.StopAggro = true;
	    StartCoroutine(TimerStop());
	    var audio = WoodPiece ? WoodPiece.GetComponent<AudioSource>() : null;
	    if (!audio) return;
	    audio.Play();
	    WoodPiece.Play("SpiderLuckaClose");
	}
	private void OnTriggerEnter(Collider other) { if (other.gameObject.CompareTag("arrow")) StopSpider(); }
	private IEnumerator TimerStop() { yield return new WaitForSeconds(1); Spider.SetActive(false); }
}
