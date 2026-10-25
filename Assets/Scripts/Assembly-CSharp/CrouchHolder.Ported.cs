using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CrouchHolder.c (Granny Legacy, IL2CPP x64).
public partial class CrouchHolder
{
	public CrouchHolder() { Starter = true; RadiusSphere = 0.5f; RadiusSphere2 = 0.5f; RadiusSphere3 = 2f; }
	private void Start()
	{
		if (controller == null) controller = GetComponent<CharacterController>();
		if (controller != null) { originalCenter = controller.center; originalHeight = controller.height; }
		if (PlayerPrefs.GetInt("GlitchesAndShit", 0) == 1 && Anim != null && ResetAnimation != null)
		{
			Anim.RemoveClip("PlayerReserSig"); Anim.AddClip(ResetAnimation, "PlayerReserSig");
		}
	}
	private void Update()
	{
		if (Input.GetKeyDown(CrouchKey) && (HolderF == null || (!HolderF.isLanding && !HolderF.isFalling)) && !Disabled && !Paused.IsPaused)
		{
			if (!IsCrouched && Starter) { StartCrouch(); IsCrouched = true; }
			else if (IsCrouched && Starter && (PlayerPrefs.GetInt("GlitchesAndShit", 0) == 0 ? CanStandUp() : !IsBelow)) StopCrouch();
		}
		CheckForObstaclesAbove();
		if (checkGround != null && Physics.Raycast(checkGround.position, Vector3.down, out RaycastHit hit, 1f))
		{
			if (hit.collider.CompareTag("grus")) WhichFloor(true, false, false);
			else if (hit.collider.CompareTag("golv")) WhichFloor(false, true, false);
			else if (hit.collider.CompareTag("water")) WhichFloor(false, false, true);
		}
	}
	public bool CanStandUp()
	{
		if (controller == null) return false;
		float radius = controller.radius;
		Vector3 center = transform.position + controller.center;
		return !Physics.CheckCapsule(center + Vector3.up * radius, center + Vector3.up * (originalHeight - radius), radius, MasksCrouch, QueryTriggerInteraction.Ignore);
	}
	public void CheckForObstaclesAbove()
	{
		if (UpCheck == null) return;
		IsBelow = Physics.SphereCast(UpCheck.position, RadiusSphere, Vector3.up, out _, 1f) |
			Physics.SphereCast(UpCheck.position, RadiusSphere2 * 1.5f, Vector3.up, out _, 1f);
	}
	public void WhichFloor(bool outside, bool house, bool water) { if (StepsHold != null) { StepsHold.Outside = outside; StepsHold.House = house; StepsHold.Water = water; } }
	public void StartCrouch()
	{
		if (!Starter) return;
		Starter = false;
		if (Hold != null && C != null) Hold.PlayOneShot(C);
		isCrouching = true;
		if (Anim != null) Anim.Play("PlayerHukarSig");
		StartCoroutine(Crouch());
		if (FPS2 != null) FPS2.IsCrouched = true;
	}
	public void StopCrouch() { if (Starter) BeginStanding(1f); }
	public void CrouchStopJumpscare() { BeginStanding(1.5f); }
	private void BeginStanding(float speed)
	{
		IsCrouched = false; Starter = false; On2 = true; isCrouching = false;
		if (Hold != null && S != null) Hold.PlayOneShot(S);
		if (Anim != null) { var state = Anim["PlayerReserSig"]; if (state != null) state.speed = speed; Anim.Play("PlayerReserSig"); }
		StartCoroutine(Stand());
		if (FPS2 != null) FPS2.IsCrouched = false;
	}
	private IEnumerator Crouch() { yield return new WaitForSeconds(ClipLength("PlayerHukarSig")); Starter = true; }
	private IEnumerator Stand() { yield return new WaitForSeconds(ClipLength("PlayerReserSig")); Starter = true; }
	// Port fix (not in the original): the Crouch/Stand coroutines re-arm Starter, but Unity kills coroutines when
	// the player is disabled (hiding, cutscenes, death). Without this, crouching could stop working for the rest of the run.
	private void OnDisable() { if (!Starter) { StopAllCoroutines(); Starter = true; } }
	private float ClipLength(string name) { var clip = Anim != null ? Anim.GetClip(name) : null; return clip != null ? clip.length : 0f; }
}
