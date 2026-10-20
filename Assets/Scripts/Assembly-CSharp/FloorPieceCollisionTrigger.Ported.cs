using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FloorPieceCollisionTrigger.c (Granny Legacy, IL2CPP x64).
public partial class FloorPieceCollisionTrigger
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "golv" || bumped)
		{
			return;
		}
		bumped = true;
		foreach (Collider piece in FP.OtherPieces)
		{
			Physics.IgnoreCollision(FP.SelfPiece, piece);
		}
	}
}
