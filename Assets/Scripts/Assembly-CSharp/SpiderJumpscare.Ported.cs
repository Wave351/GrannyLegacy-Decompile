using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpiderJumpscare.c (Granny Legacy, IL2CPP x64).
public partial class SpiderJumpscare
{
	public SpiderJumpscare() { TimerBackSpider = 60; AbleToSpider = true; }
	public void OnTriggerEnter(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player") || !AbleToSpider) return;
	    AbleToSpider = false;
	    Spider.SetActive(true);
	    var anim = Spider ? Spider.GetComponent<Animation>() : null;
	    if (!anim) return;
	    anim.Play("jump");
	    anim["jump"].speed = 2;
	    StartCoroutine(SpiderAction());
	    Source.Play();
	}
	private IEnumerator SpiderAction()
	{
	    yield return new WaitForSeconds(.3f);
	    var anim = Spider.GetComponent<Animation>(); if (!anim) yield break;
	    anim["jump"].speed = .3f;
	    yield return new WaitForSeconds(1.1f);
	    anim = Spider.GetComponent<Animation>(); if (!anim) yield break;
	    anim["jump"].speed = 1.4f;
	    yield return new WaitForSeconds(1);
	    Spider.SetActive(false);
	    yield return new WaitForSeconds(TimerBackSpider);
	    AbleToSpider = true;
	}
	public void SpiderTimerOff() { StartCoroutine(Restart()); AbleToSpider = false; }
	private IEnumerator Restart() { yield return new WaitForSeconds(TimerBackSpider); AbleToSpider = true; }
}
