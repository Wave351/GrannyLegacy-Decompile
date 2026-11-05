using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PrisonCaptureTrigger.c (Granny Legacy, IL2CPP x64).
public partial class PrisonCaptureTrigger
{
	// The prison cell: closes on the player and beeps until they sprint (Sprints) twice, which breaks the door off.
	private void Update()
	{
	    if (Locked && !StoppedStuff)
	    {
	        if (1 < TimerBeep)
	        {
	            TimerBeep = 0;
	            AudioSource.PlayClipAtPoint(Beep, PrisonDoor.transform.position);
	            CamLamp.material.color = new Color(0, 1, 0, 1);
	            StartCoroutine(ColorLamp());
	        }
	        else TimerBeep += Time.deltaTime;
	    }
	    if (Sprints < 2 || StoppedStuff) return;
	    StoppedStuff = true;
	    var col = PrisonDoor ? PrisonDoor.GetComponent<MeshCollider>() : null; if (!col) return;
	    col.enabled = false;
	    var body = PrisonDoor.GetComponent<Rigidbody>(); if (!body) return;
	    body.isKinematic = false;
	    var anim = PrisonDoor.GetComponent<Animation>(); if (!anim) return;
	    anim.enabled = false;
	    CollFall.SetActive(true);
	}
	public void AttemptOpen()
	{
	    var anim = PrisonDoor.GetComponent<Animation>(); if (!anim) return;
	    anim.Play("prisondoorLocked");
	    var audio = PrisonDoor.GetComponent<AudioSource>(); if (audio) audio.PlayOneShot(LockedSound);
	}
	private IEnumerator ColorLamp()
	{
	    yield return new WaitForSeconds(.5f);
	    if (CamLamp && CamLamp.material) CamLamp.material.color = new Color(1, 0, 0, 1);
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player") || Locked || StoppedStuff) return;
	    Locked = true;
	    Instantiate(NoiseOBJ, transform.position, transform.rotation);
	    Instantiate(NoiseOBJ2, transform.position, transform.rotation);
	    var col = PrisonDoor.GetComponent<MeshCollider>(); if (!col) return;
	    col.convex = true;
	    var anim = PrisonDoor.GetComponent<Animation>(); if (!anim) return;
	    anim.Play("prisondoorClose");
	    var audio = PrisonDoor.GetComponent<AudioSource>(); if (audio) audio.PlayOneShot(DoorClose);
	}
}
