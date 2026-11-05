using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/GameJoltMenu.c (Granny Legacy, IL2CPP x64).
public partial class GameJoltMenu
{
	public GameJoltMenu()
	{
	    AvailableToTrophy = true;
	    tierNames = new[] { "Rainbow", "Pink", "Purple", "Red", "Yellow", "Orange", "Green", "Blue", "Grey", "Brown" };
	    itemNames = new[] { "CarKey", "WPKey", "Screwdriver", "Pliers", "ChainCutter" };
	}
	private void Start() { StartCoroutine(AvailabilityGone()); }
	// Game Jolt: shows the signed-in user, syncs trophies and unlocked item skins with the cloud once per session, and
	// (in the first 6 seconds) unlocks the trophies for achievements already earned locally.
	private void Update()
	{
	    var api = GameJolt.API.GameJoltAPI.Instance;
	    // Restoration guard: the GameJolt API object is missing when the menu runs in the editor.
	    if (api == null || api.CurrentUser == null)
	    {
	        UsernameText.text = "Not signed in";
	        sessionSynced = false;
	    }
	    else
	    {
	        UsernameText.text = "Signed in as: " + api.CurrentUser.Name;
	        if (!sessionSynced)
	        {
	            sessionSynced = true;
	            GameJolt.API.Trophies.Get(trophies =>
	            {
	                if (trophies == null) return;
	                bool changed = false;
	                foreach (var trophy in trophies)
	                {
	                    if (trophy == null || !trophy.Unlocked) continue;
	                    string key = KeyGrabTrophyID(trophy.ID);
	                    if (!string.IsNullOrEmpty(key) && PlayerPrefs.GetInt(key) != 1) { PlayerPrefs.SetInt(key, 1); changed = true; }
	                }
	                if (changed) PlayerPrefs.Save();
	            });
	            GameJolt.API.DataStore.Get("unlocked_skins", false, SyncSkins);
	        }
	    }
	    if (!AvailableToTrophy) return;
	    var trophyPrefs = new (string pref, int id)[] {
	        ("NoDie", 0x4531a), ("EX_Robo", 0x45317), ("EX_Both", 0x45318), ("IM_Both", 0x45319), ("HOL_Esc", 0x4531c),
	        ("SlendrinaUnlock", 0x45316), ("IM_NoWP", 0x4531b), ("HOL_AllRoutesExtrasBoth", 0x4531d), ("EX_Door", 0x4542d),
	        ("EX_Car", 0x4542b), ("EX_Pipe", 0x4542c), ("Unseen", 0x4542e), ("NoWP", 0x4542f), ("HARD_Pipe", 0x45427),
	        ("HARD_Car", 0x45428), ("HARD_BothRobo", 0x45429), ("HARD_NoDyingBoth", 0x4542a), ("EX_BothRobo", 0x45430),
	        ("EX_AllRouteNo", 0x45431), ("EX_AllRouteYes", 0x45432), ("IM_Door", 0x45433), ("IM_Car", 0x45434),
	        ("IM_Pipe", 0x45435), ("IM_Robo", 0x45436), ("IM_NoSee", 0x45437), ("HOL_Robo", 0x45438), ("HOL_Both", 0x45439) };
	    foreach (var t in trophyPrefs) if (PlayerPrefs.GetInt(t.pref) == 1) GameJolt.API.Trophies.Unlock(t.id);
	}
	// Cloud skins: unlocks the skins saved in the cloud locally, then uploads the local ones ("<tier>_<item>") the cloud lacks.
	private void SyncSkins(string cloudJson)
	{
	    var data = new CloudSkinData { skinKeys = new List<string>() };
	    if (!string.IsNullOrEmpty(cloudJson))
	    {
	        data = JsonUtility.FromJson<CloudSkinData>(cloudJson);
	        if (data == null || data.skinKeys == null) return;
	        bool unlocked = false;
	        foreach (var key in data.skinKeys) if (PlayerPrefs.GetInt(key) != 1) { PlayerPrefs.SetInt(key, 1); unlocked = true; }
	        if (unlocked) PlayerPrefs.Save();
	    }
	    bool added = false;
	    foreach (var tier in tierNames)
	        foreach (var item in itemNames)
	        {
	            string key = tier + "_" + item;
	            if (PlayerPrefs.GetInt(key) != 1) continue;
	            if (data == null || data.skinKeys == null) return;
	            if (!data.skinKeys.Contains(key)) { data.skinKeys.Add(key); added = true; }
	        }
	    if (added) GameJolt.API.DataStore.Set("unlocked_skins", JsonUtility.ToJson(data), false, success => { });
	}
	public void SkinToCloud(string newSkinKey)
	{
	    GameJolt.API.DataStore.Get("unlocked_skins", false, cloudJson =>
	    {
	        var data = new CloudSkinData { skinKeys = new List<string>() };
	        if (!string.IsNullOrEmpty(cloudJson)) data = JsonUtility.FromJson<CloudSkinData>(cloudJson);
	        if (data == null || data.skinKeys == null || data.skinKeys.Contains(newSkinKey)) return;
	        data.skinKeys.Add(newSkinKey);
	        GameJolt.API.DataStore.Set("unlocked_skins", JsonUtility.ToJson(data), false, success =>
	        {
	            if (success) Debug.Log("Cloud saved new skin unlock: " + newSkinKey);
	        });
	    });
	}
	private string KeyGrabTrophyID(int id)
	{
	    switch (id)
	    {
	        case 0x45316: return "SlendrinaUnlock";
	        case 0x45317: return "EX_Robo";
	        case 0x45318: return "EX_Both";
	        case 0x45319: return "IM_Both";
	        case 0x4531a: return "NoDie";
	        case 0x4531b: return "IM_NoWP";
	        case 0x4531c: return "HOL_Esc";
	        case 0x4531d: return "HOL_AllRoutesExtrasBoth";
	        case 0x45427: return "HARD_Pipe";
	        case 0x45428: return "HARD_Car";
	        case 0x45429: return "HARD_BothRobo";
	        case 0x4542a: return "HARD_NoDyingBoth";
	        case 0x4542b: return "EX_Car";
	        case 0x4542c: return "EX_Pipe";
	        case 0x4542d: return "EX_Door";
	        case 0x4542e: return "Unseen";
	        case 0x4542f: return "NoWP";
	        case 0x45430: return "EX_BothRobo";
	        case 0x45431: return "EX_AllRouteNo";
	        case 0x45432: return "EX_AllRouteYes";
	        case 0x45433: return "IM_Door";
	        case 0x45434: return "IM_Car";
	        case 0x45435: return "IM_Pipe";
	        case 0x45436: return "IM_Robo";
	        case 0x45437: return "IM_NoSee";
	        case 0x45438: return "HOL_Robo";
	        case 0x45439: return "HOL_Both";
	        default: return null;
	    }
	}
	private IEnumerator AvailabilityGone() { yield return new WaitForSeconds(6); AvailableToTrophy = false; }
	public void SignInStart() { GameJolt.UI.GameJoltUI.Instance.ShowSignIn(); }
	public void UnlockTrophy(int t) { GameJolt.API.Trophies.Unlock(t); }
}
