using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HideFix.c (Granny Legacy, IL2CPP x64).
public partial class HideFix
{
	public HideFix() { DistancePlayerDetected = 2; }
	private void Update()
	{
	    if (PlayerTransform == null) return;
	    float dist = Vector3.Distance(transform.position, PlayerTransform.position);
	    Vector3 dir = (PlayerTransform.position - transform.position).normalized;
	    bool hit = Physics.Raycast(transform.position, dir, dist, RaycastMask);
	    if (DistancePlayerDetected < dist) hit = false;
	    if (!hit || InTR || (ForBY && !BGM.InOutside)) { if (isPlayerHidden) ExitHideState(); }
	    else if (!isPlayerHidden) EnterHideState();
	}
	public void EnterHideState()
	{
	    isPlayerHidden = true;
	    Granny.PlayerSpot = PosFixPlayerOn.transform;
	    Grandpa.PlayerSpot = PosFixPlayerOn.transform;
	}
	public void ExitHideState()
	{
	    isPlayerHidden = false;
	    Granny.PlayerSpot = Granny.Player.transform;
	    Grandpa.PlayerSpot = Grandpa.Player.transform;
	}
}
