using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/TranquilizerDropAll.c (Granny Legacy, IL2CPP x64).
public partial class TranquilizerDropAll
{
	public TranquilizerDropAll() { tag = "arrow"; MaxCount = 3; inside = new List<GameObject>(); TimerMaxDart = 7; }
	private void Start() { if (PlayerPrefs.GetInt("Diff") == 5) MaxCount = 1; }
	// Darts stuck in this spot (MaxCount of them, 1 on Hollow) are sent back to the dart spot after a while.
	private void Update()
	{
	    if (inside.Count < MaxCount) { TimerDart = 0; return; }
	    TimerDart += Time.deltaTime;
	    if (TimerMaxDart > TimerDart) return;
	    TimerDart = 0;
	    foreach (var dart in inside) dart.transform.position = ReposDart.position;
	}
	public void OnTriggerEnter(Collider other)
	{
	    if (other.gameObject.CompareTag(tag) && !inside.Contains(other.gameObject)) inside.Add(other.gameObject);
	    var data = other.gameObject.GetComponent<ItemSeedData>();
	    if (!data) return;
	    if (data.itemName != "Gas" && data.itemName != "CarBattery" && data.itemName != "BirdSeed" && data.itemName != "Melon") return;
	    var body = other.GetComponent<Rigidbody>();
	    if (body) body.isKinematic = false;
	}
	public void OnTriggerExit(Collider other)
	{
	    if (other.gameObject.CompareTag(tag) && inside.Contains(other.gameObject)) inside.Remove(other.gameObject);
	}
}
