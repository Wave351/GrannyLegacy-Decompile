using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FSP_ContinuousRotation.c (Granny Legacy, IL2CPP x64).
public partial class FSP_ContinuousRotation
{
	private void Update()
	{
		transform.Rotate(Time.deltaTime * X, Time.deltaTime * Y, Time.deltaTime * Z, local ? Space.Self : Space.World);
	}
}
