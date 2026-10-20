using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CameraMenu.c (Granny Legacy, IL2CPP x64).
public partial class CameraMenu
{
	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity) || hit.collider.gameObject.name != "ColliderTraps")
		{
			return;
		}
		hit.collider.gameObject.SetActive(false);
		BearTrap1.GetComponent<Animation>().Play("Beartrap");
		BearTrap1.GetComponent<AudioSource>().Play();
		BearTrap2.GetComponent<Animation>().Play("Beartrap");
		BearTrap2.GetComponent<AudioSource>().Play();
		GrannyLook.CrossFade("Granny_Suspicious", 0.4f);
	}
}
