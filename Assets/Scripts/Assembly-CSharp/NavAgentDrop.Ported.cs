using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/NavAgentDrop.c (Granny Legacy, IL2CPP x64).
public partial class NavAgentDrop
{
	private void Update()
	{
		GameObject parent = ParentDetectable != null ? ParentDetectable : transform.parent.gameObject;
		NavObstacle.carving = parent.layer != LayerMask.GetMask("Door");
	}
}
