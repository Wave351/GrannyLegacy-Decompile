using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AtticSpider.c (Granny Legacy, IL2CPP x64).
public partial class AtticSpider
{
	public AtticSpider() { MeatPos = new Vector3(-4.67f, 0, 38.9f); WalkSpeed = 2; SpeedRotate = 10; DistanceDet = .5f; EatingTime = 8; }
	private void Start() { StartPos = transform.position; }
	// The attic spider guards its spot: it goes to eat meat put on the plate, and once angered walks to the player and stings.
	private void Update()
	{
	    if (Died) return;
	    if (IsTargetingPlayer)
	    {
	        MoveToPos(PlayerP.position);
	        Footsteps.SetActive(true);
	        Animator.Play("Walk");
	        Vector3 p = PlayerP.position;
	        transform.LookAt(new Vector3(p.x, transform.position.y, p.z));
	        if (DistanceDet < Vector3.Distance(transform.position, PlayerP.position)) return;
	        IsTargetingPlayer = false; IsKilling = true;
	        StartCoroutine(StingKill());
	        return;
	    }
	    if (IsKilling) { Footsteps.SetActive(false); return; }
	    if (MeatGoes && !isEating)
	    {
	        MoveToPos(MeatPos);
	        transform.localEulerAngles = new Vector3(0, -7.74f, 0);
	        Footsteps.SetActive(true);
	        Animator.Play("Walk");
	        if (CloseToMeat) StartCoroutine(EatMeat());
	        return;
	    }
	    if (!MeatGoes && !isEating)
	    {
	        if (transform.position != StartPos)
	        {
	            Footsteps.SetActive(true);
	            Animator.Play("Walk");
	            transform.localEulerAngles = new Vector3(0, -181.35f, 0);
	            MoveToPos(StartPos);
	            return;
	        }
	        transform.localEulerAngles = new Vector3(0, 4.514f, 0);
	    }
	    Footsteps.SetActive(false);
	    Animator.Play("idle");
	}
	// Killing the spider makes Granny and Grandpa angry for good.
	public void Dies()
	{
	    if (Died) return;
	    Died = true;
	    var fps = PlayerP.gameObject.GetComponent<MobileFPS>();
	    if (!fps) return;
	    fps.InWeb = false;
	    Blood.SetActive(true);
	    StartCoroutine(KilledSpider());
	    if (PlayerPrefs.GetInt("Granny") == 1 && EC.Granny.activeSelf)
	    {
	        var granny = EC.Granny.GetComponent<AI_Granny>();
	        if (!granny.IsDying) { TH.AlwaysAngry1 = true; granny.ResetAIDecision(); granny.ChaseAction(); }
	    }
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if (g != 1 && (g != 2 || !EC.Grandpa.activeSelf)) return;
	    var grandpa = EC && EC.Grandpa ? EC.Grandpa.GetComponent<AI_Grandpa>() : null;
	    if (!grandpa || grandpa.IsDying) return;
	    TH.AlwaysAngry2 = true;
	    grandpa.ResetAIDecision();
	    grandpa.ChaseAction();
	}
	public void MeatIsThere() { MeatGoes = true; MeatPlate.SetActive(true); }
	public void GetMad()
	{
	    if (KillingProcess || Died) return;
	    ScreenEffects.NormalSee = true;
	    KillingProcess = true;
	    IsTargetingPlayer = true;
	    PlayerP.gameObject.GetComponent<MobileFPS>().InWeb = true;
	    if (PlayerPrefs.GetInt("Granny") == 1 && EC.Granny.activeSelf)
	    {
	        var granny = EC.Granny.GetComponent<AI_Granny>();
	        if (!granny.IsDying) granny.StopAI();
	    }
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if (g == 1 || (g == 2 && EC.Grandpa.activeSelf))
	    {
	        var grandpa = EC.Grandpa.GetComponent<AI_Grandpa>();
	        if (!grandpa.IsDying) grandpa.StopAI();
	    }
	    HissNoise.PlayOneShot(Aggro);
	}
	public void MoveToPos(Vector3 pos) { transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * WalkSpeed); }
	private IEnumerator KilledSpider()
	{
	    ScreenEffects.NormalSee = false;
	    Footsteps.SetActive(false);
	    Animator.Play("die");
	    HissNoise.PlayOneShot(Die);
	    yield return new WaitForSeconds(9);
	    gameObject.SetActive(false);
	}
	private void OnTriggerStay(Collider other) { if (other.gameObject.name == "MeatPlate") CloseToMeat = true; }
	private IEnumerator StingKill()
	{
	    PlayerStatus.LookAtSpider = true;
	    Animator.Play("attack");
	    PlayerStatus.PlayerGettingStopped();
	    yield return new WaitForSeconds(.4f);
	    HissNoise.PlayOneShot(Sting);
	    IsKilling = false;
	    PlayerStatus.NormalDeath();
	    PlayerStatus.DamageSpider();
	    KillingProcess = false;
	    PlayerStatus.LookAtSpider = false;
	}
	private IEnumerator EatMeat()
	{
	    isEating = true;
	    MeatGoes = false;
	    yield return new WaitForSeconds(EatingTime);
	    if (Died) yield break;
	    MeatPlate.SetActive(false);
	    isEating = false;
	}
}
