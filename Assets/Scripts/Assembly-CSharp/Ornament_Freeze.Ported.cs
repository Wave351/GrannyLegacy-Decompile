using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Ornament_Freeze.c (Granny Legacy, IL2CPP x64).
public partial class Ornament_Freeze
{
	private void OnCollisionEnter(Collision other)
	{
		if (!OrnamentHits.IsBreakSurface(other.gameObject))
		{
			return;
		}
		UnityEngine.Object.Instantiate(FreezePower, transform.position, FreezePower.transform.rotation);
		UnityEngine.Object.Instantiate(BrokenOrnament, transform.position, transform.rotation);
		UnityEngine.Object.Destroy(gameObject);
	}
}
