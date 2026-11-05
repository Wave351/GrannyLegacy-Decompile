using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/OpenPresent.c (Granny Legacy, IL2CPP x64).
public partial class OpenPresent
{
	// Christmas present: pops with a sound and puff, then leaves a random surprise and removes itself.
	public void RandomGift()
	{
	    AudioSource.PlayClipAtPoint(Sound, transform.position);
	    Instantiate(Bum, transform.position, transform.rotation);
	    switch (UnityEngine.Random.Range(0, 6))
	    {
	        case 0: Spawn(GranpaRag); break;
	        case 1: Spawn(Dart); break;
	        case 2: Spawn(Explosion); break;
	        case 3: for (int i = 0; i < 7; i++) Spawn(Ammo); break;
	        case 4: Spawn(PlayerPrefs.GetInt("Textures") == 1 ? BearTrapNM : BearTrap); break;
	        case 5: Spawn(Ammo); break;
	    }
	    Destroy(gameObject);
	}
	private void Spawn(GameObject prefab) { Instantiate(prefab, transform.position, transform.rotation); }
}
