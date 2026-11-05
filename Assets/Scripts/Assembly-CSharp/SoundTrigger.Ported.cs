using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SoundTrigger.c (Granny Legacy, IL2CPP x64).
public partial class SoundTrigger
{
	private void Start() { SpawnBack = GameObject.Find("ObjectResetPoint"); }
	// Item landing sounds. "golv" floors also make a noise Granny (and with AlertsGrandpa, Grandpa) hears; "Noice" floors
	// only make the sound; water silences the item; the "ResetFloor" puts an item that fell out of the map back.
	private void OnTriggerEnter(Collider other)
	{
	    string tag = other.gameObject.tag;
	    if (tag == "water")
	    {
	        Stopped = true;
	        if (!WaterDes) return;
	        gameObject.AddComponent<DestroyTimer>();
	        var timer = GetComponent<DestroyTimer>();
	        if (timer) timer.TimeW = 15;
	        return;
	    }
	    if (tag == "golv" || tag == "Noice")
	    {
	        if (Stopped) return;
	        bool noise = tag == "golv";
	        if (noise) Instantiate(NoiseOBJ_Granny, transform.position, transform.rotation);
	        if (!OnlyOnce)
	        {
	            if (DestroyOnSound) gameObject.AddComponent<DestroyTimer>();
	            PlayRandomSound();
	        }
	        else if (TimesDropped == 0)
	        {
	            PlayRandomSound();
	            if (DestroyOnSound)
	            {
	                gameObject.AddComponent<DestroyTimer>();
	                if (!noise) GetComponent<DestroyTimer>().TimeW = 15;
	            }
	            if (transform.parent) transform.parent.gameObject.layer = LayerMask.NameToLayer("Door");
	            TimesDropped = 1;
	        }
	        if (noise && AlertsGrandpa) Instantiate(NoiseOBJ_Grandpa, transform.position, transform.rotation);
	        return;
	    }
	    if (other.gameObject.name != "ResetFloor") return;
	    var parent = transform.parent;
	    var body = parent ? parent.GetComponent<Rigidbody>() : null;
	    if (!body) return;
	    body.isKinematic = true;
	    body.velocity = Vector3.zero;
	    if (!SpawnBack) return;
	    parent.position = SpawnBack.transform.position;
	    body.isKinematic = false;
	    var ray = GameObject.Find("PickRay");
	    if (ray != null) { var pick = ray.GetComponent<PickRay>(); if (pick) pick.FellObjectBackyard(); }
	}
	// Plays one of Sounds from a separate, self-destroying object so the sound outlives the item.
	public void PlayRandomSound()
	{
	    if (Sounds == null || Sounds.Length == 0) return;
	    int i = UnityEngine.Random.Range(0, Sounds.Length);
	    if (!transform.parent) return;
	    var go = new GameObject("SoundDrop" + transform.parent.name);
	    go.transform.position = transform.position;
	    var source = go.AddComponent<AudioSource>();
	    source.clip = Sounds[i];
	    var mine = GetComponent<AudioSource>();
	    if (!mine) return;
	    source.volume = mine.volume; source.pitch = mine.pitch; source.spatialBlend = mine.spatialBlend;
	    source.minDistance = mine.minDistance; source.maxDistance = mine.maxDistance;
	    var destruct = go.AddComponent<SoundSelfDestruct>();
	    destruct.Audio = source;
	    source.Play();
	    destruct.AbleToDestruct = true;
	}
}
