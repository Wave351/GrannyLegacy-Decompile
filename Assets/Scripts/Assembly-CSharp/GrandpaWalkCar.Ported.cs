using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GrandpaWalkCar.c (Granny Legacy, IL2CPP x64).
public partial class GrandpaWalkCar
{
	public GrandpaWalkCar()
	{
		CurrentWalkV = 31.81f;
		MaxWalkReach = 123.42f;
		SpeedWalkThere = 15f;
	}

	private void Start()
	{
		Anim.Play("Grandpa_Wander");
	}

	private void Update()
	{
		Anim["Grandpa_Wander"].speed = 1.7f;
		transform.localPosition = new Vector3(CurrentWalkV, -52.24f, -9.84f);
		CurrentWalkV += Time.deltaTime * SpeedWalkThere;
	}
}
