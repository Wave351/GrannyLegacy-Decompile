using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TrapTrigger.c (Granny Legacy, IL2CPP x64).
public partial class TrapTrigger
{
	public TrapTrigger() { Enabled = true; }
	private void OnTriggerEnter(Collider o)
	{
	    if (!o.gameObject.CompareTag("Player") || !Enabled) return;
	    var prefab = PlayerPrefs.GetInt("Textures") == 1 ? TrapNM : Trap;
	    foreach (var sp in new[] { SP1, SP2, SP3, SP4, SP5 }) Instantiate(prefab, sp.position, sp.rotation);
	    Enabled = false;
	    StartCoroutine(TimerEnable());
	}
	private IEnumerator TimerEnable() { yield return new WaitForSeconds(100); Enabled = true; }
}
