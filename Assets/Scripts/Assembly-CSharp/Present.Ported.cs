using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Present.c (Granny Legacy, IL2CPP x64).
public partial class Present
{
	public Present()
	{
		SpeedFadeDown = 0.05f;
		AlphaColorValue = 1f;
	}

	private void Update()
	{
		Color color = RendererPresent.material.color;
		color.a = AlphaColorValue;
		RendererPresent.material.color = color;
		if (Fading)
		{
			AlphaColorValue -= Time.deltaTime * SpeedFadeDown;
		}
		if ((double)AlphaColorValue < 0.1)
		{
			GetComponent<Collider>().enabled = false;
		}
		if (AlphaColorValue < -1f)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public void UnwrapGift()
	{
		gameObject.name = "GiftUnwrapping";
		Fading = true;
		UnwrapEffects.Play();
		OpenPackageSound.Play();
	}
}
