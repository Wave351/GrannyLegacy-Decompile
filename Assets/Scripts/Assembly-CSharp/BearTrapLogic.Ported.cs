using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/BearTrapLogic.c (Granny Legacy, IL2CPP x64).
public partial class BearTrapLogic
{
	public BearTrapLogic() { RayDistance = 10; removeSpeed = .5f; triggerableByItem = true; }
	private void Start() { StartCoroutine(TimerDes()); }
	// PlayerHit: the player is in direct sight of the trap (rays ignore the EnemyPass layer).
	private void Update()
	{
	    ScEFF = GameObject.Find("ScreenEffects");
	    RayB = GameObject.Find("RemoveBearRay");
	    Player = GameObject.Find("Player");
	    if (Player != null) RayCaster.LookAt(Player.transform);
	    RaycastHit hit;
	    int mask = ~LayerMask.GetMask("EnemyPass");
	    PlayerHit = Physics.Raycast(RayCaster.position, RayCaster.TransformDirection(Vector3.forward), out hit, RayDistance, mask)
	        && hit.collider.gameObject.CompareTag("Player");
	}
	// An unused trap disappears after 90 seconds; a trap that caught something, 10 seconds after.
	private IEnumerator TimerDes() { yield return new WaitForSeconds(90); if (!TrappedPlayer) Destroy(gameObject); }
	private IEnumerator TimerDesItem() { yield return new WaitForSeconds(10); Destroy(gameObject); }
	private void OnTriggerEnter(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if ((tag == "golv" || tag == "grus" || tag == "Noice") && !Placed)
	    {
	        Placed = true;
	        gameObject.tag = "BearTrapActivated";
	        if (PlayerPrefs.GetInt("Textures") == 1) Anim.CrossFade("BeartrapLive");
	        GetComponent<AudioSource>().PlayOneShot(PlacedTrap);
	    }
	    if (tag == "Player" && Placed && !TrappedPlayer && PlayerHit)
	    {
	        TrappedPlayer = true;
	        Instantiate(Noise, transform.position, transform.rotation);
	        var effects = ScEFF.GetComponent<ScreenEffects>();
	        effects.Blood = 1;
	        effects.Hurt = true;
	        Player.GetComponent<MobileFPS>().AbleToMove = false;
	        RayB.GetComponent<RayRemoveTraps>().Trapped = true;
	        GetComponent<AudioSource>().PlayOneShot(AggroSound);
	        Anim.Play(Clip.name);
	        GetComponent<Rigidbody>().isKinematic = true;
	    }
	    if ((other.gameObject.layer == LayerMask.NameToLayer("Item") || tag == "arrow") && !TrappedPlayer)
	    {
	        if (!triggerableByItem)
	        {
	            // Items bounce off and pass through a trap that items cannot spring.
	            var body = other.GetComponent<Rigidbody>();
	            if (body != null) body.AddForce((other.transform.position - transform.position).normalized, ForceMode.Impulse);
	            Physics.IgnoreCollision(interactCollider, other.GetComponent<Collider>());
	        }
	        else
	        {
	            TrappedPlayer = true; ItemCaughtIt = true;
	            Instantiate(Noise, transform.position, transform.rotation);
	            GetComponent<AudioSource>().PlayOneShot(AggroSound);
	            Anim.Play(Clip.name);
	            StartCoroutine(TimerDesItem());
	        }
	    }
	    if (tag != "Santa" || TrappedPlayer) return;
	    var santa = GameObject.Find("LittleSanta");
	    TrappedPlayer = true; ItemCaughtIt = true;
	    Instantiate(Noise, transform.position, transform.rotation);
	    var audio = GetComponent<AudioSource>();
	    if (!audio) return;
	    audio.PlayOneShot(AggroSound);
	    Anim.Play(Clip.name);
	    StartCoroutine(TimerDesItem());
	    var lf = santa ? santa.GetComponent<LittleFucker>() : null;
	    if (lf && !lf.Hurt) lf.KnockedOut();
	}
	public void ShootGun()
	{
	    if (TrappedPlayer) return;
	    TrappedPlayer = true; ItemCaughtIt = true;
	    Instantiate(Noise, transform.position, transform.rotation);
	    var audio = GetComponent<AudioSource>();
	    if (!audio) return;
	    audio.PlayOneShot(AggroSound);
	    Anim.Play(Clip.name);
	    StartCoroutine(TimerDesItem());
	}
}
