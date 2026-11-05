using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SunnyGrannyEnemy.c (Granny Legacy, IL2CPP x64).
public partial class SunnyGrannyEnemy
{
	public SunnyGrannyEnemy() { boostUpLightSpeed = 1.5f; }
	private void Start() { TeleportToSpot(); }
	// The sun Granny watches the player; standing in her light for 11 seconds explodes them, and she moves every 25 seconds.
	private void Update()
	{
	    if (!player.activeSelf) inSpot = false;
	    Vector3 p = player.transform.position;
	    sunHead.transform.LookAt(new Vector3(p.x, sunHead.transform.position.y, p.z));
	    if (!inSpot)
	    {
	        if (0 < timerInPlayer) timerInPlayer -= Time.deltaTime * 5;
	        if (timerInPlayer < 0) timerInPlayer = 0;
	        if (.5f < light.intensity) light.intensity -= Time.deltaTime + Time.deltaTime;
	        if (light.intensity < .495f) light.intensity = .5f;
	        if (.45f < sunAudio.pitch) sunAudio.pitch -= Time.deltaTime * 1.1f;
	        if (sunAudio.pitch < .45f) sunAudio.pitch = .45f;
	        timerMoveOn += Time.deltaTime;
	        if (timerMoveOn < 25) return;
	        timerMoveOn = 0;
	        TeleportToSpot();
	        return;
	    }
	    timerInPlayer += Time.deltaTime;
	    if (light.intensity < 6) light.intensity += boostUpLightSpeed * Time.deltaTime;
	    if (sunAudio.pitch < 1.55f) sunAudio.pitch += Time.deltaTime * .13f;
	    if (11 <= timerInPlayer)
	    {
	        timerInPlayer = 0;
	        TeleportToSpot();
	        Instantiate(explosion, player.transform.position, Quaternion.identity);
	    }
	    timerMoveOn = 0;
	}
	public void TeleportToSpot()
	{
	    do spotNumb = UnityEngine.Random.Range(0, spotsToTeleport.Length); while (spotNumb == currentSpot);
	    currentSpot = spotNumb;
	    sunHeadParent.transform.position = spotsToTeleport[currentSpot].position;
	    inSpot = false;
	}
	private void OnTriggerStay(Collider other) { if (other.gameObject.CompareTag("Player")) inSpot = true; }
	private void OnTriggerExit(Collider other) { if (other.gameObject.CompareTag("Player")) inSpot = false; }
}
