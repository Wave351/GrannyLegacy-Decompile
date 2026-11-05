using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AI_RobotEnemy.c (Granny Legacy, IL2CPP x64).
public partial class AI_RobotEnemy
{
	public AI_RobotEnemy() { ViewRange = 100; MaxVisionConvinced = 4; TimerStopShoot = 1.5f; }
	private void Start()
	{
	    transform.position = Spawn.position;
	    Agent.enabled = true;
	    ChooseWaypoint();
	}
	// The robot patrols waypoints, stops and shoots at the player it can see, and opens doors in front of it.
	private void Update()
	{
	    if (!Dead)
	    {
	        var audio = GetComponent<AudioSource>();
	        if (PL.activeSelf) { audio.enabled = true; Agent.isStopped = false; }
	        else { audio.enabled = false; Agent.isStopped = true; }
	        if (!IsChasing)
	        {
	            Agent.speed = 3;
	            Anim.Play(Patrol.name);
	            if (Agent.enabled) Agent.destination = CurrentTarget;
	            if (Vector3.Distance(transform.position, Agent.destination) <= 3) ChooseWaypoint();
	        }
	        else
	        {
	            Agent.speed = 0;
	            Vector3 look = Player.transform.position - transform.position;
	            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), Time.deltaTime * 15);
	            if (!Anim.IsPlaying(Shoot.name) && Seeing) TimerShoot += Time.deltaTime;
	            if (TimerStopShoot <= TimerShoot) { TimerShoot = 0; Anim.Play(Shoot.name); }
	        }
	    }
	    if (Dead) return;
	    SquareMagnitude = (Player.transform.position - transform.position).sqrMagnitude;
	    if (!Player || !CameraView) return;
	    Vector3 vp = CameraView.WorldToViewportPoint(Player.transform.position);
	    bool inView = vp.z > 0 && vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1;
	    RaycastHit hit;
	    if (!Physics.Linecast(CameraView.transform.position, Player.transform.position, out hit) || !hit.collider.CompareTag("Player")) Seeing = false;
	    else if (SquareMagnitude < ViewRange && inView)
	    {
	        if (!IsChasing)
	        {
	            int clip = UnityEngine.Random.Range(0, YappingClips.Length);
	            Instantiate(NoiseObjGranny, gameObject.transform.position, gameObject.transform.rotation);
	            Talk.PlayOneShot(YappingClips[clip]);
	        }
	        IsChasing = true; Seeing = true;
	    }
	    if (Physics.Raycast(OpenDoorRay.position, OpenDoorRay.TransformDirection(Vector3.forward), out hit, 4.5f, ExLayer))
	    {
	        string name = hit.collider.gameObject.name;
	        if (name == "Innerdoor" || name == "SmallDoor" || name == "MeatDoor" || name == "OldHouseWoodenDoor")
	            hit.collider.GetComponent<AnimatedStuff>().OpenAction();
	    }
	    if (!IsChasing) return;
	    if (Seeing) { TimerStopVision = 0; return; }
	    TimerStopVision += Time.deltaTime;
	    if (MaxVisionConvinced > TimerStopVision) return;
	    TimerStopVision = 0;
	    IsChasing = false;
	    Talk.PlayOneShot(BeepGo);
	}
	public void KillRobot()
	{
	    if (Dead) return;
	    Dead = true;
	    Anim.Play("die");
	    DeadNoise.SetActive(true);
	    Agent.enabled = false;
	    var audio = GetComponent<AudioSource>();
	    if (!audio) return;
	    audio.volume = 0;
	    gameObject.layer = LayerMask.NameToLayer("Default");
	}
	public void ChooseWaypoint()
	{
	    do WaypointNumb = UnityEngine.Random.Range(0, Waypoints.Count); while (WaypointNumb == CurrentWaypoint);
	    CurrentWaypoint = WaypointNumb;
	    if (Waypoints[WaypointNumb] != null) CurrentTarget = Waypoints[WaypointNumb].position;
	}
}
