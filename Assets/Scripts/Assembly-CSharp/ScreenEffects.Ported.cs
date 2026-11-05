using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Ported from Annotated-Pseudocode/ScreenEffects.c (Granny Legacy, IL2CPP x64).
public partial class ScreenEffects
{
	public ScreenEffects() { SpeedFadeUp = 1.5f; SpeedFadeDown = .5f; SpeedBlindnessTransition = .05f; SpeedGo = .1f; SpeedAppear = 1.5f; Max1 = .8f; Max2 = .4f; }
	private void Start()
	{
	    Profile.TryGetSettings(out CA); Profile.TryGetSettings(out V); Profile.TryGetSettings(out DOF); Profile.TryGetSettings(out G);
	    V.intensity.value = 0; G.intensity.value = 0; DOF.focalLength.value = 1;
	    // Seconds until the pepper-spray blindness wears off, by difficulty.
	    switch (PlayerPrefs.GetInt("Diff"))
	    {
	        case 0: TimerMaxBlindnessRecover = 15; break;
	        case 1: TimerMaxBlindnessRecover = 35; break;
	        case 2: TimerMaxBlindnessRecover = 55; break;
	        case 3: TimerMaxBlindnessRecover = 70; break;
	        case 4: TimerMaxBlindnessRecover = 95; break;
	        case 5: TimerMaxBlindnessRecover = 110; break;
	    }
	}
	private void Update()
	{
	    // Chromatic aberration while an enemy is chasing (NormalSee).
	    CA.intensity.value = Scare1;
	    if (!NormalSee) { if (0 <= Scare1) Scare1 -= Time.deltaTime * SpeedGo; }
	    else if (Scare1 <= Max1) Scare1 += Time.deltaTime * SpeedAppear;
	    var focal = DOF.focalLength;
	    if (!Blinded)
	    {
	        if (1 <= focal.value) focal.value -= Time.deltaTime * 1.4f;
	        if (0 < BlindnessValue) BlindnessValue -= Time.deltaTime * SpeedBlindnessTransition;
	        if (0 < V.intensity.value) V.intensity.value -= Time.deltaTime * .35f;
	        if (0 < G.intensity.value) G.intensity.value -= Time.deltaTime * .35f;
	        if (TimerBlindness != 0) TimerBlindness = 0;
	    }
	    else
	    {
	        if (focal.value <= 4) focal.value += Time.deltaTime * 1.2f;
	        if (BlindnessValue < .3f) BlindnessValue += Time.deltaTime * SpeedBlindnessTransition;
	        if (V.intensity.value <= .7f) V.intensity.value += Time.deltaTime * .3f;
	        if (G.intensity.value <= 1.1f) G.intensity.value += Time.deltaTime * .2f;
	        TimerBlindness += Time.deltaTime;
	        if (TimerMaxBlindnessRecover <= TimerBlindness) Blinded = false;
	    }
	    BloodScreen1.color = new Color(1, 0, 1, Blood);
	    FrostScreen.color = new Color(1, 1, 1, FreezeVolume);
	    var c = BlindScreen.color;
	    BlindScreen.color = new Color(c.r, c.g, c.b, BlindnessValue);
	    if (!Frozen) { if (0 < FreezeVolume) FreezeVolume -= Time.deltaTime * SpeedFadeDown; }
	    else if (FreezeVolume < 1) FreezeVolume += Time.deltaTime * SpeedFadeUp;
	    if (!Hurt) { if (0 < Blood) Blood -= Time.deltaTime * SpeedFadeDown; }
	    else if (Blood < 1) Blood += Time.deltaTime * SpeedFadeUp;
	}
	// The native code calls StopCoroutine on a fresh enumerator before restarting these timers, which stops nothing,
	// so earlier timers keep running; that is kept.
	// Frozen by a freeze trap or snowballs: can't move for 7 seconds.
	public void FrozePlayer()
	{
	    Frozen = true;
	    var fps = Player.GetComponent<MobileFPS>();
	    if (!fps) return;
	    fps.AbleToMove = false;
	    StartCoroutine(TimerUnfroze());
	}
	private IEnumerator TimerUnfroze()
	{
	    yield return new WaitForSeconds(7);
	    Frozen = false;
	    if (!Camera) yield break;
	    AudioSource.PlayClipAtPoint(Unfrozen, Camera.gameObject.transform.position);
	    var fps = Player ? Player.GetComponent<MobileFPS>() : null;
	    if (fps) fps.AbleToMove = true;
	}
	public void BlindedAction()
	{
	    if (Blinded) return;
	    Blinded = true;
	    V.center.value = VignetteBlindnessPresets[UnityEngine.Random.Range(0, VignetteBlindnessPresets.Length)];
	}
	public void FanHurt()
	{
	    StartCoroutine(FanFinger());
	    Hurt = true;
	    Camera.Play("playerHurt");
	}
	private IEnumerator FanFinger() { yield return new WaitForSeconds(4.5f); Hurt = false; }
}
