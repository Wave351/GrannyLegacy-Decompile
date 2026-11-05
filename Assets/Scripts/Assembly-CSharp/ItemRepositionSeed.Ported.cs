using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemRepositionSeed.c (Granny Legacy, IL2CPP x64).
public partial class ItemRepositionSeed
{
	// Seeded runs: an item dropped into this trigger sends the matching scene item to this spot's "SpotReposition_<item>" child.
	public void Awake()
	{
	    itemMap = new Dictionary<string, (Transform, string)> {
	        { "Pliers", (Pliers, "SpotReposition_Pliers") }, { "MasterKey", (MasterKey, "SpotReposition_MasterKey") },
	        { "Hammer", (Hammer, "SpotReposition_Hammer") }, { "PDKey", (PDKey, "SpotReposition_PDKey") },
	        { "Code", (Code, "SpotReposition_Code") }, { "SafeKey", (SafeKey, "SpotReposition_SafeKey") },
	        { "WPKey", (WPKey, "SpotReposition_WPKey") }, { "Battery", (Battery, "SpotReposition_Battery") },
	        { "Winch", (Winch, "SpotReposition_Winch") }, { "Melon", (Melon, "SpotReposition_Melon") },
	        { "PlayKey", (PlayHouseKey, "SpotReposition_PlayHouseKey") }, { "RedCog", (RedCog, "SpotReposition_Cogs") },
	        { "OrangeCog", (OrangeCog, "SpotReposition_Cogs") }, { "Shotgun_Barrel", (Barrel, "SpotReposition_Barrel") },
	        { "Shotgun_Buttstock", (Buttstock, "SpotReposition_Buttstock") }, { "Shotgun_Trigger", (Trigger, "SpotReposition_Trigger") },
	        { "CarKey", (CarKey, "SpotReposition_CarKey") }, { "SparkPlug", (SparkPlug, "SpotReposition_SparkPlug") },
	        { "Gas", (Gas, "SpotReposition_Gas") }, { "Engine", (Engine, "SpotReposition_Engine") },
	        { "CarBattery", (CarBattery, "SpotReposition_CarBattery") }, { "Wrench", (Wrench, "SpotReposition_Wrench") },
	        { "Book", (Book, "SpotReposition_Book") }, { "Meat", (Meat, "SpotReposition_Meat") },
	        { "SPKey", (SPKey, "SpotReposition_SPKey") }, { "Remote", (Remote, "SpotReposition_Remote") },
	        { "BirdSeed", (BirdSeed, "SpotReposition_Seed") }, { "WheelCrank", (WheelCrank, "SpotReposition_WheelCrank") },
	        { "ChainCutter", (ChainCutter, "SpotReposition_ChainCutter") }, { "WoodenStick", (WoodenStick, "SpotReposition_Stick") },
	        { "RustyKey", (RustyKey, "SpotReposition_RustyKey") }, { "RoboData", (RoboData, "SpotReposition_Data") },
	        { "ElectricBaton", (Baton, "SpotReposition_Baton") }, { "CabinetKey", (ECKey, "SpotReposition_ECKey") },
	        { "Fuse", (Fuse, "SpotReposition_Fuse") } };
	}
	public void OnTriggerEnter(Collider other)
	{
	    if (other.gameObject.layer != LayerMask.NameToLayer("Item") || RepositionedItem) return;
	    var data = other.GetComponent<ItemSeedData>();
	    if (data != null)
	    {
	        (Transform, string) entry;
	        if (itemMap.TryGetValue(data.itemName, out entry))
	        {
	            var spot = FindChildByName(transform, entry.Item2);
	            if (spot != null && entry.Item1 != null)
	            {
	                entry.Item1.position = spot.position;
	                entry.Item1.rotation = spot.rotation;
	                RepositionedItem = true;
	            }
	        }
	    }
	    if (Rigid) other.GetComponent<Rigidbody>().isKinematic = false;
	}
	public Transform FindChildByName(Transform parent, string name)
	{
	    foreach (Transform child in parent) if (child.name == name) return child;
	    return null;
	}
}
