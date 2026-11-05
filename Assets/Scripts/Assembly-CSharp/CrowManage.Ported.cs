using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CrowManage.c (Granny Legacy, IL2CPP x64).
public partial class CrowManage
{
	private void Start() { NestBird.Play("CrowIdle"); }
	private void MakeNoise() { Instantiate(Noise, SPNoise.position, SPNoise.rotation); }
	public void HurtDart()
	{
	    MakeNoise();
	    NestBird.Play("Attack");
	    StartCoroutine(WaitAfterAttack());
	    Audio.PlayOneShot(Crow);
	}
	// Hit by a dart: the crow is blinded for 3.5 seconds and the key under it can be taken.
	public void HurtBird()
	{
	    MakeNoise();
	    ColliderPickUp.SetActive(false);
	    HurtTemp = true;
	    NestBird.Play("BlindedBird");
	    StartCoroutine(WaitAfterAttack2());
	    Audio.PlayOneShot(Crow);
	}
	private IEnumerator WaitAfterAttack2()
	{
	    yield return new WaitForSeconds(3.5f);
	    HurtTemp = false;
	    NestBird.Play("CrowIdle");
	    if (!Killed) ColliderPickUp.SetActive(true);
	}
	// Bird seed on the plate: the crow flies down to eat for 8 seconds (opening the nest door first if needed).
	public void FillPlate()
	{
	    if (Killed) { PutFood.SetActive(true); return; }
	    if (FlyToNest.activeSelf) return;
	    if (DoorBur.name != "OpenedObject")
	    {
	        var anim = DoorBur ? DoorBur.GetComponent<Animation>() : null;
	        if (!anim) return;
	        anim.Play("BurDoorOpen");
	        var audio = DoorBur.GetComponent<AudioSource>();
	        if (!audio) return;
	        audio.Play();
	        DoorBur.name = "OpenedObject";
	    }
	    StartCoroutine(TimerEating());
	}
	// Killing the crow angers Granny and Grandpa for good.
	public void KillAssBird()
	{
	    Killed = true;
	    ColliderPickUp.SetActive(false);
	    if (NestBird.gameObject.activeSelf)
	    {
	        BloodKill.transform.position = NestBird.transform.position;
	        BloodKill.SetActive(true);
	        NestBird.gameObject.SetActive(false);
	    }
	    else if (BirdEat.activeSelf)
	    {
	        BloodKill.transform.position = BirdEat.transform.position;
	        BloodKill.SetActive(true);
	        BirdEat.SetActive(false);
	    }
	    if (PlayerPrefs.GetInt("Granny") == 1 && EC.Granny.activeSelf) TH.AlwaysAngry1 = true;
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if ((g == 1 || g == 2) && EC.Grandpa.activeSelf) TH.AlwaysAngry2 = true;
	}
	private IEnumerator TimerEating()
	{
	    ColliderPickUp.SetActive(false);
	    PutFood.SetActive(true);
	    Flying.Play("FlyEat");
	    FlyToFood.SetActive(true);
	    NestBird.gameObject.SetActive(false);
	    yield return new WaitForSeconds(Flying["FlyEat"].length);
	    FlyToFood.SetActive(false);
	    BirdEat.SetActive(true);
	    yield return new WaitForSeconds(8);
	    if (!Killed)
	    {
	        BirdEat.SetActive(false);
	        FlyToNest.SetActive(true);
	        PutFood.SetActive(false);
	        Flying.Play("CrowFlyBack");
	    }
	    yield return new WaitForSeconds(Flying["CrowFlyBack"].length);
	    if (Killed) yield break;
	    NestBird.gameObject.SetActive(true);
	    NestBird.Play("CrowIdle");
	    FlyToNest.SetActive(false);
	    ColliderPickUp.SetActive(true);
	}
	// Reaching into the nest while the crow is there: it pecks the player.
	public void AttemptSteal()
	{
	    MakeNoise();
	    ScreenEffects.FanHurt();
	    NestBird.Play("Attack");
	    StartCoroutine(WaitAfterAttack());
	    Audio.PlayOneShot(Attack);
	}
	private IEnumerator WaitAfterAttack()
	{
	    yield return new WaitForSeconds(1.5f);
	    if (NestBird) NestBird.Play("CrowIdle");
	}
}
