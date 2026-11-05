using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CarHide.c (Granny Legacy, IL2CPP x64).
public partial class CarHide
{
	public CarHide() { MaxAgain = 2; }
	private void Update()
	{
	    if (TimerAgain < MaxAgain) { TimerAgain += Time.deltaTime; ButtonSprite.enabled = false; }
	    else ButtonSprite.enabled = true;
	    ButtonSprite.sprite = InCar ? OutSprite : InSprite;
	    if (NearCar && !CarShit.CarMoving && !StatusPL.IsJumpscared && !StatusPL.Killed
	        && Player.GetComponent<CrouchHolder>().Starter && !AISlendrina.PlayerCaughtAttention)
	    {
	        ButtonSprite.gameObject.SetActive(true);
	        if (!Input.GetKeyUp(KeyCode.R) || TimerAgain <= .9f || Paused.IsPaused) return;
	        if (InCar) { GetOutM(); return; }
	        HideSpot.Enter(GrannyEye, GrandpaEye);
	        RayP.T_HighlightItem.SetActive(false);
	        Player.SetActive(false);
	        var fps = Player ? Player.GetComponent<MobileFPS>() : null;
	        if (!fps) return;
	        fps.ResetRotation();
	        Player.transform.position = PlayerNewPos.position;
	        Player.transform.rotation = PlayerNewPos.rotation;
	        PlayerStatus.InCar = true;
	        Canvas.enabled = false;
	        TimerAgain = 0;
	        Handheld.Head = CamHand;
	        CamInside.SetActive(true);
	        InCar = true;
	        Audio.PlayOneShot(GetIn);
	        return;
	    }
	    ButtonSprite.gameObject.SetActive(false);
	}
	public void GetOutM()
	{
	    Player.SetActive(true);
	    Handheld.Head = OriginalCam;
	    TimerAgain = 0;
	    PlayerStatus.InCar = false;
	    Player.transform.position = PlayerNewPos.position;
	    Player.transform.rotation = PlayerNewPos.rotation;
	    Canvas.enabled = true;
	    var crouch = Player.GetComponent<CrouchHolder>();
	    if (!crouch) return;
	    crouch.StopCrouch();
	    CamInside.SetActive(false);
	    InCar = false; NearCar = false;
	    Audio.PlayOneShot(GetOut);
	    GrannyEye.PlayerSpotted = false;
	    GrandpaEye.PlayerSpotted = false;
	}
	private void OnTriggerStay(Collider other) { if (other.gameObject.CompareTag("Player")) NearCar = true; }
	private void OnTriggerExit(Collider other) { if (other.gameObject.CompareTag("Player")) NearCar = false; }
}
