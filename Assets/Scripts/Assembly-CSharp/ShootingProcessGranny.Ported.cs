using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ShootingProcessGranny.c (Granny Legacy, IL2CPP x64).
public partial class ShootingProcessGranny
{
	public ShootingProcessGranny() { ForceSendSprayEnergy = 15; }
	private void Start()
	{
	    if (Anim && Anim["UsePepperSpray"] != null) Anim["UsePepperSpray"].speed = 1.1f;
	    int d = Mathf.Clamp(PlayerPrefs.GetInt("Diff"), 0, 5);
	    if (Anim && Anim["ShootDart"] != null) Anim["ShootDart"].speed = new float[] { 1.1f, 1.3f, 1.4f, 1.8f, 2.3f, 2.5f }[d];
	    if (PlayerPrefs.GetInt("GrannyWP") == 0) enabled = false;
	}
	private void Update()
	{
	    if (SpraySP && Player) SpraySP.LookAt(Player.position);
	    if (Anim && Anim.IsPlaying("ShootDart")) return;
	    if (Uncharged) Uncharged.SetActive(false); if (Charged) Charged.SetActive(true); if (DartIn) DartIn.SetActive(true);
	}
	public void ShootDart()
	{
	    if (!DartSP || !DartShoot) return;
	    var target = UnityEngine.Random.Range(0, 2) == 0 ? PlayerLeft : PlayerRight;
	    if (target) DartSP.LookAt(target.position);
	    var dart = Instantiate(DartShoot, DartSP.position, DartSP.rotation);
	    var body = dart.GetComponent<Rigidbody>(); if (body) body.AddForce(dart.transform.forward * ForceSend, ForceMode.Impulse);
	    if (ShootDartS) ShootDartS.Play();
	}
	public void UseSpray()
	{
	    if (!SpraySP || !SprayEnergy) return;
	    var energy = Instantiate(SprayEnergy, SpraySP.position, SpraySP.rotation);
	    var body = energy.GetComponent<Rigidbody>(); if (body) body.AddForce(energy.transform.forward * ForceSendSprayEnergy, ForceMode.Impulse);
	    if (SprayEffect) { SprayEffect.Play(); var sound = SprayEffect.GetComponent<AudioSource>(); if (sound) sound.Play(); }
	}
}
