using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EnemyFootstep.c (Granny Legacy, IL2CPP x64).
public partial class EnemyFootstep
{
	private void Start() { InWater = false; }
	private void Update()
	{
	    RaycastHit hit;
	    if (!Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance)) return;
	    if (hit.collider.CompareTag("grus")) CurrentGround = 2;
	    else if (hit.collider.CompareTag("water")) CurrentGround = 3;
	    else if (hit.collider.CompareTag("golv") || hit.collider.CompareTag("Noice")) CurrentGround = InWater ? 3 : 1;
	}
	public void SoundEffectStep()
	{
	    AudioClip[] clips = CurrentGround == 1 ? House : CurrentGround == 2 ? Grass : CurrentGround == 3 ? Water : null;
	    if (FeetSource && clips != null && clips.Length > 0) FeetSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
	}
	public EnemyFootstep() { CurrentGround = 1; RayDistance = 1.5f; }
}
