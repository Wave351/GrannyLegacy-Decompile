using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Eyes_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class Eyes_Grandpa
{
	public void FixedUpdate()
	{
	    if (!AI || !Player || !CameraView) return;
	    int difficulty = Mathf.Clamp(PlayerPrefs.GetInt("Diff"), 0, 5);
	    bool hiding = AI.PlayerStatus && AI.PlayerStatus.HidingUnderThings;
	    bool onTop = HV && HV.HidingOnTop;
	    // Granny's original branch order makes HidingOnTop use the easy range.
	    if (onTop && difficulty > 1) difficulty = 1;
	    if (onTop && difficulty == 0) hiding = true;
	    ViewRange = new float[] { 500, 900, 1200, 1500, 1500, 2500 }[difficulty];
	    MaxVisionConvinced = (hiding ? new float[] { 3, 4, 5, 5.5f, 6, 7.5f }
	                               : new float[] { 6, 8, 10, 12, 16, 20 })[difficulty];
	    if (Talk)
	    {
	        if (!AI.IsDying) { Talk.pitch = OriginalGrandpaPitch; Talk.volume = .25f; }
	        else { Talk.pitch = Mathf.MoveTowards(Talk.pitch, 0, .2f * Time.deltaTime); Talk.volume -= .25f * Time.deltaTime; }
	    }
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
	            if (!AI.IsDying)
	            {
	                if (NoiseObjGranny) Instantiate(NoiseObjGranny, AI.transform.position, AI.transform.rotation);
	                if (Talk && YappingClips != null && YappingClips.Length > 0)
	                    Talk.PlayOneShot(YappingClips[UnityEngine.Random.Range(0, YappingClips.Length)]);
	            }
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
	public Eyes_Grandpa() { ViewRange = 100; MaxVisionConvinced = 4; OriginalGrandpaPitch = 1; }
}
