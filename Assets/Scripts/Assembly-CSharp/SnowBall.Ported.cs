using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SnowBall.c (Granny Legacy, IL2CPP x64).
public partial class SnowBall
{
	private void Start()
	{
		SnowMan = GameObject.Find("SnowManAI");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			SnowManAI snowMan = SnowMan.GetComponent<SnowManAI>();
			snowMan.ScreenEffects.FreezeVolume += 0.6f;
			snowMan.HitSnowBall();
		}
		int index = UnityEngine.Random.Range(0, Clips.Length);
		AudioSource source = SnowBallDestructEffect.GetComponent<AudioSource>();
		if (source != null)
		{
			source.clip = Clips[index];
			SnowBallDestructEffect.SetActive(true);
			source.Play();
			SnowBallDestructEffect.transform.parent = null;
			UnityEngine.Object.Destroy(gameObject);
		}
	}
}
