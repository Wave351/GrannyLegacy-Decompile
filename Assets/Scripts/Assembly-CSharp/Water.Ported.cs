using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Water.c (Granny Legacy, IL2CPP x64).
public partial class Water
{
	public Water() { XSpeed = .5f; YSpeed = .5f; OffsetX = 1; OffsetY = 1; }
	// Scrolling water texture; items, arrows and the cellar hatch splash when they fall in.
	private void Update()
	{
	    OffsetY += Time.deltaTime * YSpeed;
	    OffsetX += Time.deltaTime * XSpeed;
	    WaterM.material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
	}
	private void OnTriggerEnter(Collider other)
	{
	    if (other.gameObject.layer == LayerMask.NameToLayer("Item") || other.gameObject.CompareTag("arrow"))
	        AudioSource.PlayClipAtPoint(WaterSplash, other.transform.position);
	    if (other.gameObject.name == "HatchSpiderCellar") AudioSource.PlayClipAtPoint(WaterSplash, other.transform.position);
	}
}
