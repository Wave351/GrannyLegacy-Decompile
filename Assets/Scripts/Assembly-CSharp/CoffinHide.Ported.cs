using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CoffinHide.c (Granny Legacy, IL2CPP x64).
public partial class CoffinHide
{
	public CoffinHide() { Coffin = 1; MaxDown = 176.7f; MaxUp = -75.669f; YValue = 180; ZValue = -90; ValueCoffinCurrent = -75.669f; SpeedCoffinChange = 45; MaxAgain = 2; }
	private void SetInCoffin(bool value)
	{
	    if (Coffin == 1) PlayerStatus.InCoffinBackyard = value;
	    else if (Coffin == 2) PlayerStatus.InCoffinAttic = value;
	}
	// The coffin lid swings between MaxUp and MaxDown (opposite directions for the two coffins) as the player gets in or out.
	private void Update()
	{
	    if (TimerAgain < MaxAgain) { TimerAgain += Time.deltaTime; ButtonSprite.enabled = false; }
	    else ButtonSprite.enabled = true;
	    CoffinDoor.transform.localEulerAngles = new Vector3(ValueCoffinCurrent, YValue, ZValue);
	    bool notFast = Time.timeScale != 15;
	    if (Coffin == 1)
	    {
	        ButtonSprite.sprite = InCoffin ? OutSprite : InSprite;
	        if (!InCoffin) { if (ValueCoffinCurrent <= MaxUp && notFast) ValueCoffinCurrent += Time.deltaTime * SpeedCoffinChange; }
	        else if (ValueCoffinCurrent > MaxDown && notFast) ValueCoffinCurrent -= Time.deltaTime * SpeedCoffinChange;
	    }
	    else if (Coffin == 2)
	    {
	        ButtonSprite.sprite = InCoffin ? OutSprite : InSprite;
	        if (!InCoffin) { if (ValueCoffinCurrent > MaxUp && notFast) ValueCoffinCurrent -= Time.deltaTime * SpeedCoffinChange; }
	        else if (ValueCoffinCurrent <= MaxDown && notFast) ValueCoffinCurrent += Time.deltaTime * SpeedCoffinChange;
	    }
	    if (NearCoffin && !StatusPL.IsJumpscared && !StatusPL.Killed
	        && Player.GetComponent<CrouchHolder>().Starter && !AISlendrina.PlayerCaughtAttention)
	    {
	        ButtonSprite.gameObject.SetActive(true);
	        if (!Input.GetKeyUp(KeyCode.R) || TimerAgain <= .9f || Paused.IsPaused) return;
	        if (InCoffin) { GetOutM(); return; }
	        HideSpot.Enter(GrannyEye, GrandpaEye);
	        RayP.T_HighlightItem.SetActive(false);
	        Player.SetActive(false);
	        var fps = Player ? Player.GetComponent<MobileFPS>() : null;
	        if (!fps) return;
	        fps.ResetRotation();
	        Player.transform.position = PlayerNewPos.position;
	        Player.transform.rotation = PlayerNewPos.rotation;
	        SetInCoffin(true);
	        Canvas.enabled = false;
	        TimerAgain = 0;
	        Handheld.Head = CamHand;
	        CamInside.SetActive(true);
	        InCoffin = true;
	        Audio.PlayOneShot(GetIn);
	        return;
	    }
	    ButtonSprite.gameObject.SetActive(false);
	}
	public void GetOutM()
	{
	    GrannyEye.PlayerSpotted = false;
	    Player.SetActive(true);
	    Handheld.Head = OriginalCam;
	    TimerAgain = 0;
	    SetInCoffin(false);
	    Canvas.enabled = true;
	    var crouch = Player ? Player.GetComponent<CrouchHolder>() : null;
	    if (!crouch) return;
	    crouch.StopCrouch();
	    CamInside.SetActive(false);
	    InCoffin = false;
	    Audio.PlayOneShot(GetOut);
	    GrannyEye.PlayerSpotted = false;
	    GrandpaEye.PlayerSpotted = false;
	}
	private void OnTriggerEnter(Collider other) { if (other.gameObject.CompareTag("Player")) NearCoffin = true; }
	private void OnTriggerExit(Collider other) { if (other.gameObject.CompareTag("Player")) NearCoffin = false; }
}
