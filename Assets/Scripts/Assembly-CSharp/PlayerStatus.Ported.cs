using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PlayerStatus.c (Granny Legacy, IL2CPP x64).
public partial class PlayerStatus
{
	public PlayerStatus() { LookSpeed = 15; MaxFOV = 50; SpeedFOVFade = 4; }
	private void Update()
	{
	    HasTeddy = TeddyHand && TeddyHand.activeSelf;
	    if (LookAtGrandpa) LookToward(GrandpaPLook, 20, true);
	    if (LookAtGranny)
	    {
	        LookToward(GrannyPLook, 20, true);
	        if (GrannyPLook && CameraPivot)
	        { FinalLookDirection = (GrannyPLook.transform.position - CameraPivot.transform.position).normalized; FinalLookDirection.y = 0; }
	    }
	    if (LookAtSpider) LookToward(SpiderPLook, 15, false);
	    if ((!BedAwakenBody || !BedAwakenBody.activeSelf) && !TimerStopped)
	    {
	        TimerCurrentRun += Time.deltaTime;
	        int minutes = Mathf.FloorToInt(TimerCurrentRun / 60);
	        int seconds = Mathf.FloorToInt(TimerCurrentRun % 60);
	        int milliseconds = Mathf.FloorToInt((TimerCurrentRun * 1000) % 1000);
	        if (TimerText) TimerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
	    }
	    else if (TimerStopped && TimerText) TimerText.color = Color.red;
	    if (TimerUI) TimerUI.SetActive(PlayerPrefs.GetInt("GameTimer") == 1);
	    if (Input.GetKeyDown(KeyCode.L)) PlayerPrefs.SetInt("GameTimer", PlayerPrefs.GetInt("GameTimer") == 0 ? 1 : 0);
	}
	private void LookToward(GameObject target, float speed, bool narrowFOV)
	{
	    if (narrowFOV && PlayerCam && PlayerCam.fieldOfView > MaxFOV) PlayerCam.fieldOfView -= SpeedFOVFade * Time.deltaTime;
	    if (!target || !CameraPivot) return;
	    Vector3 direction = target.transform.position - CameraPivot.transform.position;
	    if (direction.sqrMagnitude > .000001f)
	        CameraPivot.transform.rotation = Quaternion.Slerp(CameraPivot.transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
	}
	public void PlayerGettingStopped()
	{
	    IsJumpscared = true;
	    if (!Player) return;
	    var movement = Player.GetComponent<MobileFPS>();
	    if (movement) movement.isAllowedToMove = movement.AbleToMove = false;
	    var crouch = Player.GetComponent<CrouchHolder>();
	    if (crouch)
	    { if (crouch.IsCrouched || crouch.isCrouching) crouch.CrouchStopJumpscare(); crouch.Disabled = true; }
	}
	public void DamageFeet()
	{
	    if (SoundHurt && Player) AudioSource.PlayClipAtPoint(SoundHurt, Player.transform.position);
	    if (ScreenEffects && ScreenEffects.BloodScreenHit) ScreenEffects.BloodScreenHit.gameObject.SetActive(true);
	}
	public void DamageSpider() { if (ScreenEffects && ScreenEffects.BloodBitenScreen) ScreenEffects.BloodBitenScreen.gameObject.SetActive(true); }
	private void ResetEnemyRecovery()
	{
	    if (!EC) return;
	    EC.GrannyKilled = EC.GrandpaKilled = false;
	    if (EC.GrandpaBed) EC.GrandpaBed.SetActive(false);
	}
	public void GrannyCaughtYou() { if (Killed) return; ResetEnemyRecovery(); PlayerGettingStopped(); LookAtGranny = true; IsJumpscared = true; }
	public void GrandpaCaughtYou() { if (Killed) return; PlayerGettingStopped(); ResetEnemyRecovery(); LookAtGrandpa = true; IsJumpscared = true; }
	public void GrannyCaughtYouBed()
	{
	    if (Killed) return;
	    StartCoroutine(DeathCountBed(1.6f)); ResetEnemyRecovery(); LookAtGranny = IsJumpscared = true;
	}
	public void GrandpaCaughtYouBed()
	{
	    if (Killed) return;
	    StartCoroutine(DeathCountBed(2.1f)); ResetEnemyRecovery(); LookAtGrandpa = IsJumpscared = true;
	}
	private void HidePlayerAfterDeath()
	{
	    if (E1) E1.SetActive(false); if (E2) E2.SetActive(false);
	    if (PickRay) PickRay.DeathAction();
	    if (Player) Player.SetActive(false); if (UIMain) UIMain.SetActive(false); if (TextUI) TextUI.SetActive(false);
	}
	private void StopEnemiesAndFlashlight()
	{
	    if (TH) TH.AlwaysAngry1 = TH.AlwaysAngry2 = false;
	    ResetEnemyRecovery();
	    if (PlayerPrefs.GetInt("Granny") == 1 && Granny)
	    { var ai = Granny.GetComponent<AI_Granny>(); if (ai) ai.StopAI(); }
	    int grandpa = PlayerPrefs.GetInt("Grandpa");
	    if ((grandpa == 1 || grandpa == 2) && Grandpa)
	    { var ai = Grandpa.GetComponent<AI_Grandpa>(); if (ai) ai.StopAI(); }
	    if (Escapedo) Escapedo.DiedOnce = true;
	    if (Days && Days.FlashActiv) Days.FlashActiv.SetActive(false);
	    if (PlayerPrefs.GetInt("FlashLightIsOn") == 1 && FLM) FLM.DisableFlashlightStatus();
	}
	private void ClearHidingPositions()
	{
	    foreach (var bed in new[] { BedHide1, BedHide2, BedHide3 }) if (bed) bed.InBed = bed.NearBed = false;
	    if (CarHide) CarHide.NearCar = false;
	    if (CoffinHide1) CoffinHide1.NearCoffin = false;
	    if (CoffinHide2) CoffinHide2.NearCoffin = false;
	    InBed1 = InCoffinBackyard = InCoffinAttic = false;
	    HidingFromMom = false;
	}
	public void MomDeath()
	{
	    if (Killed) return;
	    Killed = true; HidePlayerAfterDeath(); if (MomPlayer) MomPlayer.SetActive(true);
	    HidingFromMom = InBed1 = InCoffinBackyard = InCoffinAttic = false;
	    StartCoroutine(MomDeathCount()); StopEnemiesAndFlashlight();
	    if (ScreenEffects) ScreenEffects.NormalSee = true;
	}
	public void KnockDeath() { BeginRagdollDeath(true); }
	public void NormalDeath() { BeginRagdollDeath(false); }
	private void BeginRagdollDeath(bool knock)
	{
	    if (Killed) return;
	    Killed = true; HidePlayerAfterDeath(); ResetEnemyRecovery();
	    if (ScreenEffects && ScreenEffects.BloodScreenHit) ScreenEffects.BloodScreenHit.gameObject.SetActive(true);
	    Transform spot = knock ? SpawnRagdollPos2 : SpawnRagdollPos;
	    if (RagdollPlayer && spot) Instantiate(RagdollPlayer, spot.position, spot.rotation);
	    StartCoroutine(DeathCount());
	    if (ScreenEffects) ScreenEffects.NormalSee = false;
	    ClearHidingPositions(); StopEnemiesAndFlashlight();
	    if (knock && CanBloodSpatter && BloodSpatterHit && CameraPivot)
	        Instantiate(BloodSpatterHit, CameraPivot.transform.position, BloodSpatterHit.transform.rotation);
	}
	public void SpawnRagdoll()
	{
	    if (!CameraPivot || !RagdollPlayer || !SpawnRagdollPos2) return;
	    Vector3 forward = CameraPivot.transform.forward; forward.y = 0;
	    if (forward.sqrMagnitude < .0001f && Player) forward = Player.transform.forward;
	    Instantiate(RagdollPlayer, SpawnRagdollPos2.position, Quaternion.LookRotation(forward));
	}
	private void FinishDeath(bool mom, bool bed)
	{
	    ClearHidingPositions();
	    LookAtGranny = LookAtGrandpa = LookAtSpider = false; IsJumpscared = false;
	    if (Days) Days.StartNew(); Killed = false;
	    if (ScreenEffects)
	    {
	        ScreenEffects.NormalSee = false;
	        if (ScreenEffects.BloodBitenScreen) ScreenEffects.BloodBitenScreen.gameObject.SetActive(false);
	        if (!mom) ScreenEffects.Blinded = false;
	    }
	    if (mom) HidingFromHouseEnemy = false;
	    foreach (var closet in new[] { CUnh1, CUnh2, CUnh3, CUnh4 }) if (closet) closet.Hid = false;
	    if (HV) HV.HidingOnTop = false;
	    if (EyesGranny) EyesGranny.PlayerSpotted = false; if (EyesGrandpa) EyesGrandpa.PlayerSpotted = false;
	    if (bed && PlayerPrefs.GetInt("FlashLightIsOn") == 1 && FLM) FLM.DisableFlashlightStatus();
	    if (!mom && !bed && PVE) PVE.TurnBackOn();
	}
	private IEnumerator DeathCount()
	{
	    yield return new WaitForSeconds(3.5f);
	    if (BlackScreen) BlackScreen.CrossFadeAlpha(1, 1, false);
	    yield return new WaitForSeconds(1.5f);
	    FinishDeath(false, false);
	}
	private IEnumerator DeathCountBed(float Timer)
	{
	    IsJumpscared = true; yield return new WaitForSeconds(Timer);
	    if (BlackScreen) BlackScreen.CrossFadeAlpha(1, .2f, false);
	    yield return new WaitForSeconds(2);
	    if (Player) Player.SetActive(true); if (PickRay) PickRay.DeathAction();
	    yield return new WaitForSeconds(.01f);
	    if (Player) Player.SetActive(false);
	    yield return new WaitForSeconds(2);
	    foreach (var bed in new[] { BedHide1, BedHide2, BedHide3 }) if (bed) bed.DisableDeath();
	    FinishDeath(false, true);
	}
	private IEnumerator MomDeathCount()
	{
	    yield return new WaitForSeconds(4.5f);
	    if (BlackScreen) BlackScreen.CrossFadeAlpha(1, .5f, false);
	    yield return new WaitForSeconds(4.5f);
	    FinishDeath(true, false);
	}
}
