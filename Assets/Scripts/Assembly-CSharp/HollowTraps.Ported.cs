using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HollowTraps.c (Granny Legacy, IL2CPP x64).
public partial class HollowTraps
{
	public HollowTraps() { maxTimerSpawnTrap = 60; currentTraps = new List<GameObject>(); }
	private void Update()
	{
	    if (!FPS.AbleToMove) return;
	    timerSpawnTraps += Time.deltaTime;
	    if (maxTimerSpawnTrap <= timerSpawnTraps) { timerSpawnTraps = 0; TrapSpawn(); }
	}
	// Every minute on Hollow two traps move to two different random spots.
	public void TrapSpawn()
	{
	    foreach (var t in currentTraps) if (t != null) Destroy(t);
	    currentTraps.Clear();
	    int a = UnityEngine.Random.Range(0, spawnsTrap.Length);
	    int b = UnityEngine.Random.Range(0, spawnsTrap.Length);
	    while (b == a) b = UnityEngine.Random.Range(0, spawnsTrap.Length);
	    currentTraps.Add(Instantiate(hollowTrap, spawnsTrap[a].position, spawnsTrap[a].rotation));
	    currentTraps.Add(Instantiate(hollowTrap, spawnsTrap[b].position, spawnsTrap[b].rotation));
	}
}
