using System;
using System.Collections.Generic;
using UnityEngine;

public partial class SeedManager : MonoBehaviour
{
	private class OriginalState
	{
		public Vector3 pos;

		public Quaternion rot;

		public bool active;

		public bool kinematic;
	}
	public List<GameObject> allItems;
	public List<PuzzleDef> puzzleDefs;
	public List<string> requiredEscapeItemNames;
	public SpawnArea[] spawnAreas;
	public int Seed;
	public bool RandomizeSeed;
	public bool activatePlacedItems;
	public bool fillAllFreeSpawns;
	public bool Rigid;
	public float EscapeItemPuzzleChance;
	public System.Random rnd;
	public Dictionary<string, GameObject> itemsByName;
	public HashSet<string> usedItemNames;
	public HashSet<Transform> usedPuzzleSpawns;
	public Dictionary<string, bool> puzzleHasItem;
	private const int MaxAttempts = 50;
	private Dictionary<GameObject, OriginalState> originalStates;
}
