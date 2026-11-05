using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CarManager.c (Granny Legacy, IL2CPP x64).
public partial class CarManager
{
	public CarManager() { WallHealth = 3; Forward = true; StartKey = KeyCode.F; ReverseKey = KeyCode.S; ForwardKey = KeyCode.W; }
	private IEnumerator TimerBackward(AnimationClip Clip)
	{
	    CarAnim.Play(Clip.name);
	    yield return new WaitForSeconds(Clip.length);
	    CarMoving = false;
	}
	private IEnumerator TimerForward(AnimationClip Clip)
	{
	    CarAnim.Play(Clip.name);
	    yield return new WaitForSeconds(Clip.length);
	    if (WallHealth != 0) CarMoving = false;
	}
	public void HitBack() { EngineOffBacked.Play(); PlayerHeadAnim.CrossFade("HeadBackward"); }
	// The garage wall takes three hits; the first two knock the bonnet loose, the last breaks through and ends the run.
	public void HitWall()
	{
	    if (WallHealth == 1)
	    {
	        PlayerHeadAnim.Play("HeadForward");
	        WallR.gameObject.SetActive(false);
	        BrokenWallPieces.SetActive(true);
	        EngineWave.SetActive(false);
	        DustEffect.Play();
	        FrontDamageWall.Play();
	        CarForward.Stop();
	        CarGoes.SetActive(true);
	        CarHitTriggersKillGranny1.SetActive(false);
	        CarHitTriggersKillGranny2.SetActive(false);
	        EscapeManager.EscapeCar();
	        WallHealth -= 1;
	        return;
	    }
	    if (CrashMotorHuv == null) return;
	    CrashMotorHuv.SetActive(true);
	    var box = MotorHuvDown.GetComponent<BoxCollider>();
	    if (!box) return;
	    box.enabled = true;
	    var body = MotorHuvDown ? MotorHuvDown.GetComponent<Rigidbody>() : null;
	    if (!body) return;
	    body.isKinematic = false;
	    MotorHuvDown.name = "MotorhuvIsDanger";
	    PlayerHeadAnim.Play("HeadForward");
	    DustEffect.Play();
	    CarForward.Stop();
	    FrontDamageWall.Play();
	    WallHealth -= 1;
	}
	private void Update()
	{
	    CarHitTriggersKillGranny.SetActive(CarMoving);
	    if (!CarHide.InCar)
	    {
	        ReverseB.SetActive(false); ForwardB.SetActive(false); StartB.SetActive(false);
	    }
	    else
	    {
	        if (!CarOn || CarMoving) { ReverseB.SetActive(false); ForwardB.SetActive(false); }
	        else if (!Forward)
	        {
	            ForwardB.SetActive(true); ReverseB.SetActive(false);
	            if (Input.GetKeyDown(ForwardKey))
	            {
	                CarMoving = true; Forward = true;
	                if (WallHealth == 1) { StartCoroutine(TimerForward(CarAnim.GetClip("CarEscape"))); EscapeMusicP1.SetActive(true); }
	                else StartCoroutine(TimerForward(CarAnim.GetClip("CarForward")));
	                CarForward.Play();
	            }
	        }
	        else
	        {
	            ForwardB.SetActive(false); ReverseB.SetActive(true);
	            if (Input.GetKeyDown(ReverseKey))
	            {
	                CarMoving = true; Forward = false;
	                string clip = "CarReverse2";
	                if (!SmashedWallFirst) { SmashedWallFirst = true; clip = "CarReverse"; }
	                StartCoroutine(TimerBackward(CarAnim.GetClip(clip)));
	                CarReverse.Play();
	            }
	        }
	        if (!HandKeyCheck.activeSelf) StartB.SetActive(false);
	        else
	        {
	            StartB.SetActive(true);
	            // The car starts once the engine part, battery and spark plug are in, the bonnet is open,
	            // all 6 engine screws are in and the tank is filled.
	            var p = Puzzles;
	            bool ready = p.PlacedEnginePart && p.PlacedCarBattery && p.usedSparkPlug && p.openedPort && p.SkruvEngine == 6 && p.TankFuelled;
	            if (Input.GetKeyDown(StartKey))
	            {
	                if (!ready)
	                {
	                    RayP.T_NeedShit.SetActive(true);
	                    RayP.T_NeedShit.GetComponent<TMP_Text>().text = "I cannot start the car yet";
	                    RayP.T_NeedShit.GetComponent<TextManager>().Add();
	                }
	                else
	                {
	                    CarOn = true;
	                    RayP.Drop1.SetActive(false);
	                    KeyInserted.SetActive(true);
	                    HandKeyCheck.SetActive(false);
	                    EngineStartSound.SetActive(true);
	                    EngineWave.SetActive(true);
	                }
	            }
	        }
	    }
	    bool nm = PlayerPrefs.GetInt("Textures") == 1, normal = PlayerPrefs.GetInt("Textures") == 0;
	    Texture2D wall = WallHealth == 3 ? (nm ? Wall1NM : normal ? Wall1 : null) : WallHealth == 2 ? (nm ? Wall2NM : normal ? Wall2 : null)
	        : WallHealth == 1 ? (nm ? Wall3NM : normal ? Wall3 : null) : null;
	    if (wall != null) WallR.material.mainTexture = wall;
	}
}
