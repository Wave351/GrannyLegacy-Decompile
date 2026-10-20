using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemLogicNoClip.c (Granny Legacy, IL2CPP x64).
public partial class ItemLogicNoClip
{
	private void Start()
	{
		Collider other = GameObject.Find(otherGameObjectNameIgnore).GetComponentInParent<Collider>();
		Physics.IgnoreCollision(other, GetComponent<MeshCollider>());
	}
}
