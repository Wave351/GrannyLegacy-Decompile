using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/KickHatch.c (Granny Legacy, IL2CPP x64).
public partial class KickHatch
{
	public KickHatch() { MoveLimit = 2; MoveSpeed = 2; }
	private void Start() { rb = GetComponent<Rigidbody>(); XStartPosition = transform.position.x; }
	private void FixedUpdate()
	{
	    if (!Kicked) return;
	    if (MoveLimit <= Mathf.Abs(transform.position.x - XStartPosition)) Kicked = false;
	    rb.AddForce(Vector3.right * MoveSpeed, ForceMode.Force);
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player") || Kicked || ISYet) return;
	    foreach (var c in Physics.OverlapSphere(Trigger.transform.position, 5))
	    {
	        if (c.gameObject.name != "HatchSpiderCellar") continue;
	        var body = c.gameObject.GetComponent<Rigidbody>();
	        if (body == null) continue;
	        body.isKinematic = false;
	        body.AddForce((c.transform.position - transform.position).normalized * 3, ForceMode.Impulse);
	    }
	    Trigger.SetActive(true);
	    var audio = GetComponent<AudioSource>();
	    if (!audio) return;
	    audio.PlayOneShot(KickSound);
	    Kicked = true; ISYet = true;
	    rb.isKinematic = false;
	}
}
