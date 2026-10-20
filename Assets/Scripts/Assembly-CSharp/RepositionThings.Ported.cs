using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RepositionThings.c (Granny Legacy, IL2CPP x64).
public partial class RepositionThings
{
	private void Start()
	{
		transform.localPosition = NewPosition;
		if (Coloring)
		{
			Obj.material.color = new Color(0.745283f, 0.745283f, 0.745283f, 1f);
		}
	}
}
