using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Sauna.c (Granny Legacy, IL2CPP x64).
public partial class Sauna
{
	public Sauna() { Closed = true; SpeedSteamSound = 1.5f; }
	private void Update()
	{
	    if (SaunaSteamSound) SaunaSteamSound.volume = CurrentValSauna;
	    CurrentValSauna = Mathf.MoveTowards(CurrentValSauna, SwitchOn ? .4f : 0, SpeedSteamSound * Time.deltaTime);
	    if (!Door) return;
	    var animation = Door.GetComponent<Animation>();
	    bool moving = animation && (animation.IsPlaying("InnerdoorOpen_0") || animation.IsPlaying("InnerdoorClose_0"));
	    foreach (var collider in new Collider[] { Door.GetComponent<MeshCollider>(), Door.GetComponent<BoxCollider>() })
	    {
	        if (!collider) continue;
	        if (Player) Physics.IgnoreCollision(collider, Player, moving);
	        if (PlayerH) Physics.IgnoreCollision(collider, PlayerH, moving);
	    }
	}
	private void PlayOn(GameObject target, string clip)
	{ if (target) { var animation = target.GetComponent<Animation>(); if (animation) animation.Play(clip); } }
	private void DoorSound(AudioClip clip)
	{ if (Door && clip) { var sound = Door.GetComponent<AudioSource>(); if (sound) sound.PlayOneShot(clip); } }
	private void SetCarving(bool carving)
	{ if (Door) { var obstacle = Door.GetComponent<NavMeshObstacle>(); if (obstacle) obstacle.carving = carving; } }
	public void HandleModify()
	{
	    if (!PutHandle)
	    {
	        PlayOn(Handle, "BastuBomNer"); DoorSound(Handle1); PutHandle = true;
	        if (Sdeath && (Sdeath.GrannyIn || Sdeath.GrandpaIn)) SetCarving(true);
	    }
	    else { PlayOn(Handle, "BastuBomUpp"); PutHandle = false; SetCarving(false); }
	}
	public void ModifySwitch()
	{
	    SwitchOn = !SwitchOn; PlayOn(Switch, SwitchOn ? "BastuSpakOn" : "BastuSpakOff");
	    if (Switch) { var sound = Switch.GetComponent<AudioSource>(); if (sound) sound.Play(); }
	    if (SwitchOn) { CurrentValSauna = 0; if (SteamSauna) SteamSauna.Play(); }
	    else if (SteamSauna) SteamSauna.Stop();
	}
	public void RemoveHandle()
	{
	    PutHandle = false; SetCarving(false);
	    if (Handle)
	    {
	        var animation = Handle.GetComponent<Animation>(); if (animation) animation.enabled = false;
	        var body = Handle.GetComponent<Rigidbody>();
	        if (body) { body.isKinematic = false; body.AddForce(Vector3.forward * 4, ForceMode.Force); }
	        Handle.name = "HandleRemoved";
	        if (SmashSound) AudioSource.PlayClipAtPoint(SmashSound, Handle.transform.position);
	    }
	    PlayOn(Door, "InnerdoorOpen_0"); Closed = false; DoorSound(DoorOpen);
	}
	public void OpenDoor()
	{
	    if (!Closed) { PlayOn(Door, "InnerdoorClose_0"); Closed = true; DoorSound(DoorClose); }
	    else if (!PutHandle) { PlayOn(Door, "InnerdoorOpen_0"); Closed = false; DoorSound(DoorOpen); }
	    else { PlayOn(Door, "InnerdoorLocked_0"); DoorSound(DoorLocked); }
	}
}
