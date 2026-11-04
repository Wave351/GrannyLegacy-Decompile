using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MomAlign.c (Granny Legacy, IL2CPP x64).
public partial class MomAlign
{
	public MomAlign() { RotMax = 3; }
	private void Update() { if (agent.isOnNavMesh) AlignToGround(); }
	// Tilts the spider mom to the ground normal under her, keeping her heading.
	public void AlignToGround()
	{
	    RaycastHit hit;
	    if (!Physics.Raycast(Target.position + Vector3.up * .5f, Vector3.down, out hit, 2, Layer)) return;
	    Quaternion tilt = Quaternion.FromToRotation(Target.up, hit.normal) * Target.rotation;
	    Vector3 e = tilt.eulerAngles;
	    Quaternion goal = Quaternion.Euler(e.x, Target.eulerAngles.y, e.z);
	    Target.rotation = Quaternion.Lerp(Target.rotation, goal, Time.deltaTime * RotMax);
	}
}
