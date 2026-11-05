using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SnowManAI.c (Granny Legacy, IL2CPP x64).
public partial class SnowManAI
{
	public SnowManAI() { Interested = true; DistanceWander = 20; CloseDis = 8; PRCT = 1.2f; TimerMaxThrow = .7f; }
	// The Christmas snowman only roams on Extreme/Hollow in game versions 1.3 and newer.
	private void Start()
	{
	    if (!TestingScene)
	    {
	        int diff = PlayerPrefs.GetInt("Diff"), version = PlayerPrefs.GetInt("CURVERSION");
	        if (diff <= 3 && diff >= 0 || version >= 0 && version <= 2)
	        {
	            var self = GetComponent<SnowManAI>();
	            if (!self) return;
	            self.enabled = false;
	            Agent.enabled = false;
	            return;
	        }
	    }
	    NavObstacle.SetActive(false);
	    SnowManBody.localEulerAngles = new Vector3(-90, 0, 0);
	    WanderPointChoose();
	}
	private void Update()
	{
	    if (ItsOff) return;
	    if (!Agent.pathPending && Agent.remainingDistance <= PRCT) WanderPointChoose();
	    if (Interested && SML.WithinArea)
	    {
	        // Stops and pelts the player with snowballs while they are close.
	        Agent.speed = 0;
	        Quaternion look = Quaternion.LookRotation(Player.transform.position - transform.position);
	        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 10);
	        TimerThrow += Time.deltaTime;
	        if (TimerThrow < TimerMaxThrow) return;
	        TimerThrow = 0;
	        var ball = Instantiate(SnowBall, SnowBallThrowPoint.position, SnowBallThrowPoint.rotation);
	        var body = ball.GetComponent<Rigidbody>();
	        if (!body) return;
	        body.velocity = Vector3.zero;
	        body.angularVelocity = Vector3.zero;
	        body.AddForce(SnowBallThrowPoint.forward * 15, ForceMode.Impulse);
	        return;
	    }
	    Agent.speed = 2;
	    TimerThrow = 0;
	}
	public void WanderPointChoose()
	{
	    if (ItsOff) return;
	    NavMeshHit hit;
	    for (int i = 0; i < 20; i++)
	    {
	        Vector3 random = transform.position + UnityEngine.Random.insideUnitSphere * DistanceWander;
	        if (NavMesh.SamplePosition(random, out hit, 5, NavMesh.AllAreas) && CloseDis <= Vector3.Distance(transform.position, hit.position))
	        {
	            CurTarget = hit.position;
	            Agent.SetDestination(hit.position);
	            return;
	        }
	    }
	    NavMesh.SamplePosition(transform.position + UnityEngine.Random.insideUnitSphere * DistanceWander, out hit, 5, NavMesh.AllAreas);
	    CurTarget = hit.position;
	    Agent.SetDestination(hit.position);
	}
	// Every snowball adds frost; the seventh freezes the player and the snowman loses interest for 10 seconds.
	public void HitSnowBall()
	{
	    ScreenEffects.FreezeVolume += .6f;
	    if (AmountOfTimesHit != 6) { AmountOfTimesHit += 1; return; }
	    AmountOfTimesHit = 0;
	    ScreenEffects.FrozePlayer();
	    Interested = false;
	    StartCoroutine(TimerInterestedAgain());
	}
	private IEnumerator TimerInterestedAgain() { yield return new WaitForSeconds(10); Interested = true; }
}
