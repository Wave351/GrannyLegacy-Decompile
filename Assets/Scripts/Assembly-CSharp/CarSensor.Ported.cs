using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CarSensor.c (Granny Legacy, IL2CPP x64).
public partial class CarSensor
{
	private void OnTriggerEnter(Collider Obj)
	{
		if (Obj.gameObject.name != "RustyCar")
		{
			return;
		}
		UnityEngine.Object.Instantiate(NoiseObj1, transform.position, transform.rotation);
		if (!Front)
		{
			Car.HitBack();
			return;
		}
		UnityEngine.Object.Instantiate(NoiseObj2, transform.position, transform.rotation);
		Car.HitWall();
	}
}
