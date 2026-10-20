using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MelonBlown.c (Granny Legacy, IL2CPP x64).
public partial class MelonBlown
{
	private void Start()
	{
		ItemStuff.transform.parent = null;
		ItemStuff.transform.position = ItemStuffPoint.position;
		ItemStuff.transform.rotation = ItemStuffPoint.rotation;
	}
}
