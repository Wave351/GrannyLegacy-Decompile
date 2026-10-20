using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FloorPiece.c (Granny Legacy, IL2CPP x64).
public partial class FloorPiece
{
	private void Start()
	{
		foreach (Collider piece in OtherPieces)
		{
			Physics.IgnoreCollision(SelfPiece, piece, true);
		}
	}

	public void CollisionIsTrue()
	{
		foreach (Collider piece in OtherPieces)
		{
			Physics.IgnoreCollision(SelfPiece, piece, false);
		}
	}
}
