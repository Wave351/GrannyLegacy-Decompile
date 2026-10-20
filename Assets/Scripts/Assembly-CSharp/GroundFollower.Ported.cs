using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GroundFollower.c (Granny Legacy, IL2CPP x64).
public partial class GroundFollower
{
	public GroundFollower()
	{
		HeightOffset = 0.05f;
		MaxRayDistance = 10f;
	}

	private void Update()
	{
		Vector3 origin = transform.position + Vector3.up * 0.5f;
		if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, MaxRayDistance, GroundMask))
		{
			Vector3 position = transform.position;
			transform.position = new Vector3(position.x, hit.point.y + HeightOffset, position.z);
		}
	}
}
