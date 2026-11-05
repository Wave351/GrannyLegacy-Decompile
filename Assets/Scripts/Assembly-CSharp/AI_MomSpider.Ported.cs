using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.ootii.Actors.BoneControllers;

// Ported from Annotated-Pseudocode/AI_MomSpider.c (Granny Legacy, IL2CPP x64).
public partial class AI_MomSpider
{
	public AI_MomSpider() { Walk_Speed = 3; Run_Speed = 5; Walk_Anim = 1.1f; Run_Anim = 1.2f; TimerMaxNewNav = 5; DistanceFromNav = 2; }
	private void Start()
	{
	    Agent.enabled = true;
	    switch (PlayerPrefs.GetInt("Diff"))
	    {
	        case 0: Run_Speed = 3.4f; Run_Anim = 1.35f; break;
	        case 1: Run_Speed = 4.8f; Run_Anim = 1.6f; break;
	        case 2: Run_Speed = 5.5f; Run_Anim = 1.83f; break;
	        case 3: Run_Speed = 8.5f; Run_Anim = 2.83f; break;
	        case 4: Run_Speed = 11; Run_Anim = 3.4f; CapableOfWebs = true; break;
	        case 5: Run_Speed = 13.5f; Run_Anim = 3.75f; CapableOfWebs = true; break;
	    }
	    int run = UnityEngine.Random.Range(0, 2);
	    if (run == 0) CurrentTarget = Run1.position; else if (run == 1) CurrentTarget = Run2.position;
	    RunningToPoint = true;
	}
	private void Chase()
	{
	    Agent.speed = Run_Speed;
	    ScreenEffects.NormalSee = true;
	    TimerChoosing = 0;
	    if (Agent.velocity == Vector3.zero)
	    {
	        Anim.CrossFade(Idle.name);
	        if (!Eyes.ScreamSee.activeSelf && !Eyes.PlayerStatus.HidingFromMom)
	        {
	            Vector3 look = Player.position - transform.position;
	            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), Time.deltaTime * 15);
	        }
	    }
	    else
	    {
	        Anim.CrossFade(WalkAnim.name);
	        Anim[WalkAnim.name].speed = Run_Anim;
	    }
	    if (Eyes.PlayerStatus.HidingFromMom) return;
	    Agent.SetDestination(Player.position);
	}
	// The spider mom in the secret cellar: runs to a spot, then patrols; she chases (and eats) the player inside the
	// cellar, can be stunned, and on Extreme/Hollow leaves three webs behind after losing the player.
	private void Update()
	{
	    if (PlayerClose && !IsHit && !IsEating && SC.Entered_SC && !Eyes.PlayerStatus.HidingFromMom)
	    {
	        if (!IsChasing) ChaseAction();
	        else if (Eyes.Seeing) MomCaughtPlayer();
	    }
	    int textures = PlayerPrefs.GetInt("Textures");
	    if (!TH.AlreadyHunted)
	    {
	        if (textures == 1 || textures == 0)
	        {
	            Mom.mainTexture = textures == 1 ? MomNormalNM : MomNormal;
	            Mom.SetColor("_GlowColor", MomNormalEyes);
	        }
	    }
	    else if (textures == 1 || textures == 0)
	    {
	        Mom.mainTexture = textures == 1 ? MomAngryNM : MomAngry;
	        Mom.SetColor("_GlowColor", MomAngryEyes);
	    }
	    if (!WebPregnant) TimerAddWeb = 0;
	    else
	    {
	        TimerAddWeb += Time.deltaTime;
	        bool spawn = true;
	        if (2.5f <= TimerAddWeb && TimerMaxWeb == 0) TimerMaxWeb = 1;
	        else if (7 <= TimerAddWeb && TimerMaxWeb == 1) TimerMaxWeb = 2;
	        else if (13 <= TimerAddWeb && TimerMaxWeb == 2) { TimerAddWeb = 0; WebPregnant = false; }
	        else spawn = false;
	        if (spawn) Instantiate(Web, WebSpawnP.transform.position, WebSpawnP.rotation);
	    }
	    var look = BC.GetMotor<LookAtMotor>();
	    if (look != null) look.TargetTransform = IsEating ? PlayerHeadHand.transform : PlayerHead.transform; BoneLookFallback.SetTarget(BC, IsEating ? PlayerHeadHand.transform : PlayerHead.transform);
	    if (IsHit)
	    {
	        if (BC && Agent)
	        {
	            BC.enabled = false;
	            Agent.speed = 0;
	            ScreenEffects.NormalSee = false;
	            TimerChoosing = 0;
	        }
	        return;
	    }
	    if (IsEating) { PlayerHeadHand.LookAt(LookAtP.position); return; }
	    if (IsAngry)
	    {
	        ChaseAction();
	        if (!IsChasing) return;
	        BC.enabled = true;
	        if (Eyes.ScreamSee.activeSelf) BG.MomHunting = SC.Entered_SC;
	        Chase();
	        return;
	    }
	    if (IsChasing)
	    {
	        if (Eyes.ScreamSee.activeSelf) { BC.enabled = true; BG.MomHunting = SC.Entered_SC; }
	        Chase();
	        return;
	    }
	    if (IsIdle)
	    {
	        TimerChoosing += Time.deltaTime;
	        Anim.CrossFade(Idle.name);
	        if (TimerChoosing < TimerMaxNewNav) return;
	        TimerChoosing = 0;
	        ChooseWaypoint();
	        return;
	    }
	    if (RunningToPoint)
	    {
	        if (!Agent) return;
	        Agent.speed = 20;
	        Anim.Play(WalkAnim.name);
	        Anim[WalkAnim.name].speed = 2.5f;
	        Agent.SetDestination(CurrentTarget);
	        if (DistanceFromNav < Vector3.Distance(transform.position, CurrentTarget)) return;
	        RunningToPoint = false;
	        IsIdle = true;
	        return;
	    }
	    if (!IsWalking) return;
	    Agent.speed = Walk_Speed;
	    Anim.CrossFade(WalkAnim.name);
	    Anim[WalkAnim.name].speed = .7f;
	    Agent.SetDestination(CurrentTarget);
	    if (DistanceFromNav < Vector3.Distance(transform.position, CurrentTarget)) return;
	    IsWalking = false; IsIdle = true;
	}
	public void MomCaughtPlayer()
	{
	    BC.enabled = true;
	    BG.HouseMusic.gameObject.SetActive(false);
	    BG.MomHunting = false;
	    BG.CellarMusic.gameObject.SetActive(false);
	    Anim[EatPlayer.name].speed = .7f;
	    Anim.CrossFade(EatPlayer.name);
	    var scream = Eyes.ScreamSee.GetComponent<AudioSource>();
	    if (!scream) return;
	    scream.Stop();
	    Eyes.PlayerStatus.MomDeath();
	    MomEat.Play();
	    IsEating = true;
	    SC.MomMesh.SetActive(true);
	    Agent.destination = transform.position;
	    PlayerClose = false;
	    StartCoroutine(ResetEntireThing());
	}
	private IEnumerator ResetEntireThing()
	{
	    yield return new WaitForSeconds(19);
	    IsEating = false;
	    IsChasing = false;
	    BC.enabled = false;
	    ScreenEffects.NormalSee = false;
	    BG.MomHunting = false;
	    if (!Eyes || !Eyes.PlayerStatus || !Eyes.PlayerStatus.MomPlayer) yield break;
	    Eyes.PlayerStatus.MomPlayer.SetActive(false);
	    Eyes.IsChasing = false;
	    Eyes.ScreamSee.SetActive(false);
	    if (!BG || !BG.CellarMusic) yield break;
	    BG.CellarMusic.gameObject.SetActive(true);
	    ChooseWaypoint();
	    IsWalking = true;
	    RunningToPoint = false;
	}
	public void HitMom()
	{
	    if (IsEating) return;
	    IsChasing = false; IsWalking = false; IsIdle = false; IsHit = true;
	    var scream = Eyes && Eyes.ScreamSee ? Eyes.ScreamSee.GetComponent<AudioSource>() : null;
	    if (!scream) return;
	    scream.Stop();
	    HitScream.Play();
	    RunningToPoint = false;
	    Anim.CrossFade(Stunned.name);
	    StopAllCoroutines();
	    StartCoroutine(MomHit());
	}
	private IEnumerator MomHit()
	{
	    if (!Stunned || !Anim) yield break;
	    yield return new WaitForSeconds(Anim[Stunned.name].length);
	    IsHit = false;
	    if (SC.Entered_SC)
	    {
	        if (!PlayerClose) ChaseAction();
	        else if (!IsEating && SC.Entered_SC && !Eyes.PlayerStatus.HidingFromMom) MomCaughtPlayer();
	        yield break;
	    }
	    IsChasing = false;
	    BC.enabled = false;
	    ScreenEffects.NormalSee = false;
	    BG.MomHunting = false;
	    Eyes.IsChasing = false;
	    Eyes.ScreamSee.SetActive(false);
	    ChooseWaypoint();
	    IsWalking = true;
	    RunningToPoint = false;
	}
	public void ChaseAction()
	{
	    if (IsHit || IsEating) return;
	    IsChasing = true; IsWalking = false; IsIdle = false;
	    RunningToPoint = false;
	    Agent.SetDestination(Player.position);
	}
	public void StopChase()
	{
	    if (IsEating) return;
	    IsChasing = false;
	    BC.enabled = false;
	    ScreenEffects.NormalSee = false;
	    BG.MomHunting = false;
	    Eyes.IsChasing = false;
	    Eyes.ScreamSee.SetActive(false);
	    ChooseWaypoint();
	    IsWalking = true; IsIdle = false;
	    RunningToPoint = false;
	    if (CapableOfWebs) WebPregnant = true;
	}
	public void ChooseWaypoint()
	{
	    do WaypointNumb = UnityEngine.Random.Range(0, Waypoints.Count); while (WaypointNumb == CurrentWaypoint);
	    CurrentWaypoint = WaypointNumb;
	    if (Waypoints[WaypointNumb] == null) return;
	    CurrentTarget = Waypoints[WaypointNumb].position;
	    IsWalking = true; IsIdle = false;
	}
	private void OnTriggerStay(Collider other) { if (other.gameObject.CompareTag("Player")) PlayerClose = true; }
	private void OnTriggerExit(Collider other) { if (other.gameObject.CompareTag("Player")) PlayerClose = false; }
	public void AddNewNav() { Waypoints.Add(NewNav); }
}
