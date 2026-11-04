using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Eyes_MomSpider.c (Granny Legacy, IL2CPP x64).
public partial class Eyes_MomSpider
{
	public void FixedUpdate()
	{
	    switch (PlayerPrefs.GetInt("Diff"))
	    {
	        case 0: ViewRange = 500; MaxVisionConvinced = 4; break;
	        case 1: ViewRange = 900; MaxVisionConvinced = 6; break;
	        case 2: ViewRange = 1200; MaxVisionConvinced = 7; break;
	        case 3: ViewRange = 1500; MaxVisionConvinced = 8; break;
	        case 4: ViewRange = 1500; MaxVisionConvinced = 10; break;
	        case 5: ViewRange = 2500; MaxVisionConvinced = 13; break;
	    }
	    SquareMagnitude = (Player.transform.position - transform.position).sqrMagnitude;
	    Vector3 vp = CameraView.WorldToViewportPoint(Player.transform.position);
	    bool inView = vp.z > 0 && vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1;
	    RaycastHit hit;
	    if (Physics.Linecast(transform.position, Player.transform.position, out hit, LayerIgnore) && hit.collider.CompareTag("Player")
	        && !PlayerStatus.HidingFromMom && AI.SC.Entered_SC)
	    {
	        if (SquareMagnitude < ViewRange && inView)
	        {
	            AI.IsChasing = true;
	            if (!IsChasing) { AI.ChaseAction(); if (!AI.IsHit) ScreamSee.SetActive(true); }
	            IsChasing = true; Seeing = true;
	        }
	    }
	    else Seeing = false;
	    if (!AI.IsChasing || AI.IsAngry) return;
	    if (Seeing) { TimerStopVision = 0; return; }
	    TimerStopVision += Time.deltaTime;
	    if (TimerStopVision < MaxVisionConvinced) return;
	    TimerStopVision = 0;
	    if (!AI.IsEating)
	    {
	        AI.IsChasing = false;
	        AI.BC.enabled = false;
	        AI.ScreenEffects.NormalSee = false;
	        AI.BG.MomHunting = false;
	        AI.Eyes.IsChasing = false;
	        AI.Eyes.ScreamSee.SetActive(false);
	        AI.ChooseWaypoint();
	        AI.IsWalking = true; AI.IsIdle = false;
	        AI.RunningToPoint = false;
	        if (AI.CapableOfWebs) AI.WebPregnant = true;
	    }
	    IsChasing = false;
	    ScreamSee.SetActive(false);
	}
}
