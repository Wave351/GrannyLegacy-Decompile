using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FallingHolder.c (Granny Legacy, IL2CPP x64).
public partial class FallingHolder
{
	public FallingHolder() { FallDurationHold = 1f; FallChecker = 1f; FallMega = 3.5f; Speed = 0.3f; groundCheckDis = 0.2f; }
	private void Update()
	{
		if (ch == null || FPS == null) return;
		if (DurateCan > 0.5f && Falling != null) Falling.volume = 0.25f;
		bool hardLanding = false;
		if (!ch.isGrounded && !FPS.InElev)
		{
			if (!isLanding)
			{
				isFalling = true; CanFallSound = true;
				fallDuration += Time.deltaTime * Speed; DurateCan += Time.deltaTime * 4f;
				if (fallDuration > FallMega && !DeathFall && !Damaged) { Damaged = true; HandleLanding(); hardLanding = true; }
			}
		}
		else
		{
			DurateCan = 0f;
			if (fallDuration > FallMega)
			{
				if (!Fell && DeathFall)
				{
					ResetFallSound(); fallDuration = 0f;
					if (PlayerStatus != null) { PlayerStatus.NormalDeath(); PlayerStatus.DamageFeet(); }
				}
			}
			else if (fallDuration > FallDurationHold)
			{
				if (!Damaged) { Damaged = true; HandleLanding(); hardLanding = true; }
			}
			else
			{
				if (fallDuration > FallChecker)
				{
					StopCrouching(); if (LandTiny != null) LandTiny.Play();
				}
				ResetFallSound(); fallDuration = 0f;
			}
		}
		if (hardLanding)
		{
			ResetFallSound(); DurateCan = 0f;
			int difficulty = PlayerPrefs.GetInt("Diff", 1);
			if (difficulty == 4 || difficulty == 5)
			{
				if (Ray1 != null) Ray1.CheckItemDropping();
				if (BloodSplashHead != null && CamPivot != null) UnityEngine.Object.Instantiate(BloodSplashHead, CamPivot.position, Quaternion.identity);
			}
		}
		if (isLanding) StopCrouching();
	}
	private void CheckForLanding() { if (Physics.Raycast(transform.position, Vector3.down, out _, groundCheckDis)) HandleLanding(); }
	private void HandleLanding()
	{
		isLanding = true;
		if (FPS != null) { FPS.ReserRotation(); FPS.isAllowedToMove = false; FPS.CanFade = true; }
		if (LandSound != null) LandSound.SetActive(true);
		ResetFallSound(); fallDuration = 0f; CanCamSmooth = true;
		if (playerAnimation != null) playerAnimation.Play("playerLandBad");
		StopCrouching(); StartCoroutine(WaitForAnimationEnd());
	}
	private IEnumerator WaitForAnimationEnd()
	{
		var clip = playerAnimation != null ? playerAnimation.GetClip("playerLandBad") : null;
		yield return new WaitForSeconds(clip != null ? clip.length : 0f);
		if (LandSound != null) LandSound.SetActive(false);
		if (FPS != null) { FPS.isAllowedToMove = true; FPS.ResetRotation(); FPS.CanFade = false; }
		CanCamSmooth = false; fallDuration = 0f;
		yield return new WaitForSeconds(0.2f);
		if (Ray1 != null) { Ray1.buttonClicked = false; Ray1.buttonClicked2 = false; }
		if (Ray2 != null) Ray2.buttonClicked = false;
		isLanding = false; Damaged = false;
	}
	public void TestStop()
	{
		if (LandSound != null) LandSound.SetActive(false);
		CanCamSmooth = false; if (FPS != null) { FPS.ResetRotation(); FPS.CanFade = false; }
		fallDuration = 0f; isLanding = false; Damaged = false;
	}
	private void StopCrouching() { var crouch = GetComponent<CrouchHolder>(); if (crouch != null && crouch.isCrouching) crouch.StopCrouch(); }
	private void ResetFallSound() { CanFallSound = false; isFalling = false; if (Falling != null) Falling.volume = 0f; }
}
