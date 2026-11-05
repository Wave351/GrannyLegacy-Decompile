using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LittleFucker.c (Granny Legacy, IL2CPP x64).
public partial class LittleFucker
{
	public LittleFucker()
	{
	    WalkSpeed = 2.5f; RunSpeed = 4.5f; IdleMax = 5; DetectionRange = 12; DetectionRangeNear = 2; Interested = true; TOffset = -1.6f;
	    ToffsetChangeTimer = .5f; ToffsetChangeTimerBack = .5f; CrossFadeSpeed = 3.5f; NormalLogicStatus = true; StareWeight = .4f; LookSpeed = 6;
	}
	private void SetAnimation(string name, string crossFade, float speed)
	{
	    Anim.speed = speed;
	    if (newAnimation == name) return;
	    Anim.CrossFade(crossFade, CrossFadeSpeed);
	    newAnimation = name;
	}
	// The little Santa from the present: wanders, chases the player it sees (12 units, 2 when not interested), slaps them
	// so they drop their item, then rests 20 seconds before it gets interested again.
	private void Update()
	{
	    if (!Unwrapped && Present == null) { Unwrapped = true; Active = true; WanderingPathCheck = false; Anim.enabled = true; }
	    if (NormalLogicStatus && newAnimation != "" && newAnimation != currentAnimation)
	    {
	        Anim.ResetTrigger(currentAnimation);
	        Anim.SetTrigger(newAnimation);
	        currentAnimation = newAnimation;
	    }
	    if (Vector3.Distance(transform.position, Player.position) <= 3.5f && Attacking && Agent.velocity == Vector3.zero && CanLook)
	        FacePlayer();
	    if (!Active || Hurt || Attacking) return;
	    RaycastHit hit;
	    if (!Interested)
	    {
	        if (!Chasing)
	        {
	            RaycastH.LookAt(PlayerHead);
	            Debug.DrawRay(RaycastH.position, RaycastH.forward * DetectionRangeNear, Color.blue);
	            StareAtPlayer = Physics.Raycast(RaycastH.position, RaycastH.forward, out hit, DetectionRangeNear, Layers) && hit.collider.gameObject.CompareTag("Player");
	        }
	        else StareAtPlayer = false;
	    }
	    else if (Chasing) StareAtPlayer = false;
	    else
	    {
	        RaycastH.LookAt(PlayerHead);
	        StareAtPlayer = false;
	        Debug.DrawRay(RaycastH.position, RaycastH.forward * DetectionRange, Color.green);
	        if (Physics.Raycast(RaycastH.position, RaycastH.forward, out hit, DetectionRange, Layers) && hit.collider.gameObject.CompareTag("Player"))
	        {
	            Agent.speed = RunSpeed;
	            Agent.SetDestination(Player.position);
	            Anim.speed = 2.1f;
	            newAnimation = "Wander";
	            if (!NormalLogicStatus) Anim.CrossFade("Run", CrossFadeSpeed);
	            Sounds.PlayOneShot(Aggro);
	            Chasing = true;
	        }
	    }
	    if (!Chasing) { WanderLogic(); return; }
	    WanderingPathCheck = false;
	    Agent.speed = RunSpeed;
	    Agent.SetDestination(Player.position);
	    if (2.4f < Vector3.Distance(transform.position, Player.position) || Attacking)
	    {
	        if (Agent.velocity == Vector3.zero) { FacePlayer(); SetAnimation("Idle", "Idle1", 1.4f); }
	        else SetAnimation("Wander", "Run", 2.1f);
	        return;
	    }
	    Sounds.Stop();
	    StartCoroutine(AttackSequence());
	    Interested = false; Chasing = false;
	}
	private void FacePlayer()
	{
	    Vector3 look = Player.transform.position - transform.position;
	    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), Time.deltaTime * 15);
	}
	public void InitiateAI() { Active = true; WanderingPathCheck = false; Anim.enabled = true; }
	public void FootStepPlay() { FootStepAudio.PlayOneShot(Steps[UnityEngine.Random.Range(0, Steps.Length)]); }
	public void WanderLogic()
	{
	    if (!WanderingPathCheck)
	    {
	        for (int i = 0; i < 25; i++)
	        {
	            Vector2 dir = UnityEngine.Random.insideUnitCircle;
	            dir = dir.magnitude > 1E-05f ? dir / dir.magnitude : Vector2.zero;
	            float distance = UnityEngine.Random.Range(5f, 25f);
	            Vector3 target = transform.position + new Vector3(dir.x, 0, dir.y) * distance;
	            NavMeshHit hit;
	            if (!NavMesh.SamplePosition(target, out hit, 3, NavMesh.AllAreas)) continue;
	            DestinationTarget = hit.position;
	            WanderingPathCheck = true;
	            Agent.speed = WalkSpeed;
	            Agent.SetDestination(hit.position);
	            Anim.speed = 1;
	            newAnimation = "Wander";
	            if (!NormalLogicStatus) Anim.CrossFade("Run", CrossFadeSpeed);
	            return;
	        }
	        WanderingPathCheck = false;
	        return;
	    }
	    if (Agent.pathPending || Agent.stoppingDistance + .05f < Agent.remainingDistance) return;
	    Anim.speed = 1;
	    Agent.ResetPath();
	    DestinationTarget = transform.position;
	    if (NormalLogicStatus || newAnimation != "Idle")
	    {
	        if (!NormalLogicStatus) Anim.CrossFade("Idle1", CrossFadeSpeed);
	        newAnimation = "Idle";
	    }
	    IdleTimer += Time.deltaTime;
	    if (IdleTimer < IdleMax) return;
	    IdleTimer = 0;
	    WanderingPathCheck = false;
	}
	private IEnumerator AttackSequence()
	{
	    Attacking = true;
	    Chasing = false;
	    Agent.isStopped = true;
	    Anim.speed = 1;
	    newAnimation = "Attack";
	    if (!NormalLogicStatus) Anim.CrossFade("Bite", CrossFadeSpeed);
	    CanLook = true;
	    yield return new WaitForSeconds(.15f);
	    Sounds.PlayOneShot(Smack);
	    CanLook = false;
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (diff == 4 || diff == 5)
	    {
	        Instantiate(NoiseObjGranny, transform.position, transform.rotation);
	        Instantiate(NoiseObjGrandpa, transform.position, transform.rotation);
	    }
	    yield return new WaitForSeconds(.2f);
	    PickRaycastScript.CheckItemDropping();
	    int slap = UnityEngine.Random.Range(0, 2);
	    if (slap == 0) AnimCam.Play("playerSlapped"); else if (slap == 1) AnimCam.Play("playerSlapped2");
	    yield return new WaitForSeconds(.7f);
	    Agent.isStopped = false;
	    Attacking = false;
	    Sounds.PlayOneShot(Laughter);
	    StartCoroutine(Rest());
	}
	private IEnumerator Rest() { yield return new WaitForSeconds(20); Interested = true; }
	public void KnockedOut()
	{
	    if (Hurt) return;
	    Hurt = true;
	    CollisionTrapObject.SetActive(false);
	    Chasing = false;
	    Agent.isStopped = true;
	    Anim.speed = 1;
	    newAnimation = "KnockedOut";
	    if (!NormalLogicStatus) Anim.CrossFade("FallDown", CrossFadeSpeed);
	    StartCoroutine(KnockoutRoutine());
	}
	private IEnumerator KnockoutRoutine()
	{
	    Anim.SetTrigger("StandUp");
	    yield return new WaitForSeconds(FallDownClip.length);
	    Anim.speed = 1;
	    newAnimation = "StandUp";
	    yield return new WaitForSeconds(6.5f);
	    Hurt = false;
	    Chasing = false;
	    Agent.isStopped = false;
	    WanderingPathCheck = false;
	    CollisionTrapObject.SetActive(true);
	}
	// The head turns to the player it stares at or chases.
	public void OnAnimatorIK(int layerIndex)
	{
	    float goal = (StareAtPlayer || Chasing) && !Attacking && !Hurt ? StareWeight : 0;
	    float t = Mathf.Clamp01(Time.deltaTime * LookSpeed);
	    SmW = Mathf.Lerp(SmW, goal, t);
	    if (SmW < .01f) { Anim.SetLookAtWeight(0); return; }
	    if (PlayerHead == null) return;
	    SLP = Vector3.Lerp(SLP, PlayerHead.position, t);
	    Anim.SetLookAtWeight(SmW);
	    Anim.SetLookAtPosition(SLP);
	}
}
