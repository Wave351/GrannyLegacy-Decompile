using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

// Ported from Annotated-Pseudocode/TVHandler.c (Granny Legacy, IL2CPP x64).
public partial class TVHandler
{
	private void Start()
	{
		if (canEndProcess)
		{
			return;
		}
		videop.clip = videos[UnityEngine.Random.Range(0, videos.Length)];
		videop.enabled = true;
		videop.Play();
		GameObject other = GameObject.Find("TVWatch(Clone)");
		if (other != null && other != TV.gameObject)
		{
			UnityEngine.Object.Destroy(other);
		}
		canEndProcess = true;
	}

	private void Update()
	{
		// The original only polls the TV's AudioSource.isPlaying here and discards the result.
	}
}
