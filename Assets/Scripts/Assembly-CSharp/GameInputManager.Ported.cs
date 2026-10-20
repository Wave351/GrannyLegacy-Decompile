using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GameInputManager.c (Granny Legacy, IL2CPP x64).
public partial class GameInputManager
{
	// The shipped game checks steam_api64.dll and requires a logged-in Steam client (app 962400), quitting otherwise.
	// This Mono port has no Steamworks runtime (Facepunch.Steamworks is a stub), so it skips both checks.
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnApplicationQuit()
	{
	}
}
