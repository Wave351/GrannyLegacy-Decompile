using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LightBulbFall.c (Granny Legacy, IL2CPP x64).
public partial class LightBulbFall
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "GarageDoorDel3" || Fell)
		{
			return;
		}
		Fell = true;
		AudioSource.PlayClipAtPoint(LightOver, transform.position);
		Renderer.materials[1].color = ShutdownColor;
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
		{
			body.isKinematic = false;
		}
	}
}
