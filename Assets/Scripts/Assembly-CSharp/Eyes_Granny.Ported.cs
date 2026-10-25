using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Eyes_Granny.c (Granny Legacy, IL2CPP x64).
public partial class Eyes_Granny
{
	public void FixedUpdate()
	{
	    if (!AI || !Player || !CameraView) return;
	    int difficulty = Mathf.Clamp(PlayerPrefs.GetInt("Diff"), 0, 5);
	    bool hiding = AI.PlayerStatus && AI.PlayerStatus.HidingUnderThings;
	    bool onTop = HV && HV.HidingOnTop;
	    // Granny's original branch order makes HidingOnTop use the easy range.
	    if (onTop) difficulty = 0;
	    ViewRange = new float[] { 500, 900, 1200, 1500, 1500, 2500 }[difficulty];
	    MaxVisionConvinced = (hiding ? new float[] { 3, 4, 5, 5.5f, 6, 7.5f }
	                               : new float[] { 6, 8, 10, 12, 16, 20 })[difficulty];

	    Vector3 point = Player.transform.position;
	    SquareMagnitude = (point - transform.position).sqrMagnitude;
	    Vector3 viewport = CameraView.WorldToViewportPoint(point);
	    bool inView = viewport.z > 0 && viewport.x > 0 && viewport.x < 1 && viewport.y > 0 && viewport.y < 1;
	    RaycastHit hit;
	    bool clear = Physics.Linecast(transform.position, point, out hit) && hit.collider.CompareTag("Player")
	                 && !(SC && SC.Entered_SC) && !AI.IsBlind;
	    if (!clear) Seeing = false;
	    else if (SquareMagnitude < ViewRange && inView)
	    {
	        AI.IsChasing = true;
	        if (!IsChasing)
	        {

	            AI.ChaseAction();
	            if ((!AI.Ally || !AI.Ally.gameObject.activeSelf || !AI.Ally.IsChasing)
	                && PlayerPrefs.GetInt("Chase") == 3 && SeeS) SeeS.Play();
	            if (!AI.IsDying && E) E.SeenOnce = true;
	        }
	        IsChasing = Seeing = true;
	    }
	    if (AI.IsChasing && !AI.IsAngry)
	    {
	        if (!Seeing)
	        {
	            TimerStopVision += Time.deltaTime;
	            if (TimerStopVision >= MaxVisionConvinced)
	            {
	                TimerStopVision = 0;
	                AI.StopChase();
	                IsChasing = false;
	            }
	        }
	        else { AI.UnSeenPlayer = false; TimerStopVision = 0; }
	    }
	}
	public Eyes_Granny() { ViewRange = 100; MaxVisionConvinced = 4;  }
}
