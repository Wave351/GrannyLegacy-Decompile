using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RobotShoot.c (Granny Legacy, IL2CPP x64).
public partial class RobotShoot
{
	// The robot's bullet travels along the barrel (Distance grows by ShootSpeed); whatever it reaches first is hit.
	private void Update()
	{
	    if (!Shooting) { Distance = 0; return; }
	    Distance += Time.deltaTime * ShootSpeed;
	    Rotater.LookAt(Target);
	    Debug.DrawRay(Rotater.position, Rotater.forward * Distance, Color.black);
	    RaycastHit hit;
	    if (!Physics.Raycast(Rotater.position, Rotater.forward, out hit, Distance, Masks)) return;
	    string tag = hit.collider.gameObject.tag;
	    if (tag == "Player")
	    {
	        var falling = Status.Player.GetComponent<FallingHolder>();
	        if (WJ.IsJumping || Status.IsJumpscared || falling.isFalling || falling.isLanding) return;
	        Instantiate(SpawnImpact, hit.point, Quaternion.identity);
	        Instantiate(Impactus, hit.point, Quaternion.identity);
	        Instantiate(BloodEffect, hit.point, Quaternion.identity);
	        AudioSource.PlayClipAtPoint(ImpactS, hit.point);
	        Shooting = false;
	        Status.NormalDeath();
	        AI.Agent.enabled = false;
	        var audio = AI.GetComponent<AudioSource>();
	        if (!audio) return;
	        audio.mute = true;
	        StartCoroutine(WaitTimer());
	        if (PlayerPrefs.GetInt("Granny") == 1) EC.Granny.GetComponent<AI_Granny>().StopAI();
	        int g = PlayerPrefs.GetInt("Grandpa");
	        if ((g == 1 || g == 2) && EC && EC.Grandpa) { var gp = EC.Grandpa.GetComponent<AI_Grandpa>(); if (gp) gp.StopAI(); }
	        return;
	    }
	    if ((tag == "granny" || tag == "granpa") && !Status.IsJumpscared)
	    {
	        Instantiate(SpawnImpact, hit.point, Quaternion.identity);
	        AudioSource.PlayClipAtPoint(ImpactS, hit.point);
	        Shooting = false;
	        Instantiate(BloodEffect, hit.point, Quaternion.identity);
	        if (tag == "granny") EC.GrannyDeathS(); else EC.GrandpaDeathS();
	        return;
	    }
	    if (tag == "granny" || tag == "granpa") return;
	    Instantiate(SpawnImpact, hit.point, Quaternion.identity);
	    var body = hit.collider.GetComponent<Rigidbody>();
	    if (body != null) body.AddForce(Rotater.forward * 20, ForceMode.Impulse);
	    AudioSource.PlayClipAtPoint(ImpactS, hit.point);
	    Shooting = false;
	}
	// After killing the player the robot goes back to its spawn 23 seconds later.
	private IEnumerator WaitTimer()
	{
	    yield return new WaitForSeconds(23);
	    if (!AI || !AI.Spawn) yield break;
	    AI.transform.position = AI.Spawn.position;
	    AI.Agent.enabled = true;
	    var audio = AI.GetComponent<AudioSource>();
	    if (audio) audio.mute = false;
	}
	public void Shoot()
	{
	    Shooting = true;
	    Shot.Play();
	    Instantiate(Smoke, Spawner.position, Spawner.rotation);
	    Instantiate(Smoke, Spawner2.position, Spawner2.rotation);
	}
}
