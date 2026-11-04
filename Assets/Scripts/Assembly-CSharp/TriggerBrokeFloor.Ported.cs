using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TriggerBrokeFloor.c (Granny Legacy, IL2CPP x64).
public partial class TriggerBrokeFloor
{
	public void BreakPlanks()
	{
	    if (Broken) return;
	    Broken = true;
	    CollD.SetActive(false);
	    AudioSource.PlayClipAtPoint(Broke, transform.position);
	    foreach (var f in new[] { Fl1, Fl2, Fl3, Fl4 })
	    {
	        var body = f ? f.GetComponent<Rigidbody>() : null;
	        if (!body) return;
	        body.isKinematic = false;
	    }
	    foreach (var f in new[] { Fl1, Fl2, Fl3, Fl4 }) f.layer = LayerMask.NameToLayer("Item");
	    StartCoroutine(NatureResponds());
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (other.gameObject.CompareTag("Player")) BreakPlanks();
	    if (other.gameObject.layer != LayerMask.NameToLayer("Item") && !other.gameObject.CompareTag("arrow")) return;
	    BreakPlanks();
	}
	private IEnumerator NatureResponds()
	{
	    yield return new WaitForSeconds(7);
	    Destroy(Fl1); Destroy(Fl2); Destroy(Fl3); Destroy(Fl4);
	    Destroy(gameObject);
	}
}
