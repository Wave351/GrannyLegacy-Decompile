using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/LeprechaunAI.c (Granny Legacy, IL2CPP x64).
public partial class LeprechaunAI
{
	public LeprechaunAI() { angryTimer = 25; forceSpitOut = 3; }
	private void Start() { SelectRandomPointTravel(); }
	private void Update()
	{
	    if (!triggered)
	    {
	        if (!agent.pathPending && agent.remainingDistance <= 1.6f) SelectRandomPointTravel();
	        animLepre.CrossFade("Lepre_TravelAnim");
	        agent.isStopped = false;
	        return;
	    }
	    if (player == null) return;
	    Quaternion look = Quaternion.LookRotation(player.transform.position - transform.position);
	    transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 15);
	    agent.isStopped = true;
	}
	// Bumping into the leprechaun makes it swear and spit out a coin, then it can't be triggered again for angryTimer seconds.
	private void OnTriggerStay(Collider other)
	{
	    if (!other.gameObject.CompareTag("Player") || triggered || !capableOfAngry) return;
	    triggered = true; capableOfAngry = false;
	    var audio = animLepre ? animLepre.GetComponent<AudioSource>() : null;
	    if (!audio) return;
	    audio.PlayOneShot(huh);
	    animLepre.CrossFade("Lepre_Alerted");
	    StartCoroutine(AlertedEvent());
	}
	public void SelectRandomPointTravel()
	{
	    Vector3 random = transform.position + UnityEngine.Random.insideUnitSphere * pointDistanceSelect;
	    NavMeshHit hit;
	    if (NavMesh.SamplePosition(random, out hit, pointDistanceSelect, NavMesh.AllAreas)) agent.SetDestination(hit.position);
	}
	private IEnumerator AlertedEvent()
	{
	    yield return new WaitForSeconds(1);
	    var audio = animLepre ? animLepre.GetComponent<AudioSource>() : null;
	    if (audio)
	    {
	        audio.PlayOneShot(fuckYou);
	        yield return new WaitForSeconds(1);
	        animLepre.CrossFade("Lepre_BarfCoins");
	        yield return new WaitForSeconds(.25f);
	        if (coin != null)
	        {
	            var spat = Instantiate(coin, coin.transform.position, coin.transform.rotation);
	            spat.SetActive(true);
	            var body = spat.GetComponent<Rigidbody>();
	            if (body)
	            {
	                body.AddForce(spat.transform.forward * forceSpitOut, ForceMode.Impulse);
	                audio = animLepre ? animLepre.GetComponent<AudioSource>() : null;
	                if (audio) { audio.PlayOneShot(barfCoin); yield return new WaitForSeconds(1.5f); }
	            }
	        }
	    }
	    triggered = false;
	    yield return new WaitForSeconds(angryTimer);
	    capableOfAngry = true;
	}
}
