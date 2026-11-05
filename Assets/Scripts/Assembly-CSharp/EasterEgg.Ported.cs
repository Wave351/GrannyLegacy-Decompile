using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EasterEgg.c (Granny Legacy, IL2CPP x64).
public partial class EasterEgg
{
	private void Update()
	{
	    if (EggOpened) return;
	    if (10 < TimesBumped) { EggOpened = true; RandomGift(); return; }
	    var body = GetComponent<Rigidbody>();
	    if (!body || body.velocity.magnitude >= 1.5f || !BumpingOn) return;
	    // Keeps hopping around while it slows down.
	    Vector3 dir = (UnityEngine.Random.onUnitSphere + transform.forward * .3f).normalized;
	    dir.y = Mathf.Clamp(dir.y, -.2f, .5f);
	    body.AddForce(dir * LaunchBumpForce, ForceMode.VelocityChange);
	}
	public void StartEggBumping()
	{
	    BumpingOn = true;
	    var body = GetComponent<Rigidbody>();
	    if (!body) return;
	    body.isKinematic = false;
	    body.AddForce(transform.forward * LaunchBumpForce, ForceMode.VelocityChange);
	}
	private void OnCollisionEnter(Collision collision)
	{
	    if (!BumpingOn) return;
	    TimesBumped += 1;
	    var body = GetComponent<Rigidbody>();
	    if (!body) return;
	    Vector3 dir = Vector3.Reflect(body.velocity.normalized, collision.contacts[0].normal);
	    dir = (dir + UnityEngine.Random.insideUnitSphere * .6f).normalized;
	    dir.y = Mathf.Clamp(dir.y, -.2f, .5f);
	    body.velocity = dir * LaunchBumpForce;
	    AudioSource.PlayClipAtPoint(EasterEggBump, transform.position);
	}
	// After 10 bumps the egg opens: a Grandpa ragdoll, a dart, an explosion, ammo, a bear trap or (on Extreme/Hollow) just a fart.
	public void RandomGift()
	{
	    AudioSource.PlayClipAtPoint(Sound, transform.position);
	    Instantiate(Bum, transform.position, transform.rotation);
	    int diff = PlayerPrefs.GetInt("Diff");
	    bool hard = diff == 4 || diff == 5;
	    switch (UnityEngine.Random.Range(0, 6))
	    {
	        case 0: if (hard) Fart(); else Spawn(GranpaRag); break;
	        case 1: Spawn(Dart); break;
	        case 2: Spawn(Explosion); break;
	        case 3: if (hard) Fart(); else for (int i = 0; i < 7; i++) Spawn(Ammo); break;
	        case 4: Spawn(PlayerPrefs.GetInt("Textures") == 1 ? BearTrapNM : BearTrap); break;
	        case 5: if (hard) Fart(); else Spawn(Ammo); break;
	    }
	    Destroy(gameObject);
	}
	private void Spawn(GameObject prefab) { Instantiate(prefab, transform.position, transform.rotation); }
	private void Fart() { AudioSource.PlayClipAtPoint(fart, transform.position); }
}
