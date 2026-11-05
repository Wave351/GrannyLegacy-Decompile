using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/MelonContainItem.c (Granny Legacy, IL2CPP x64).
public partial class MelonContainItem
{
	private void Start() { StartCoroutine(StopPlacedItemLogic()); }
	// Items only count as hidden in the melon when dropped in during the first 6 seconds after it was placed.
	private IEnumerator StopPlacedItemLogic() { yield return new WaitForSeconds(6); PlacedItemIn = true; }
	// An item dropped into the melon is held inside it at that item's spot.
	private void OnTriggerEnter(Collider other)
	{
	    if (PlacedItemIn || other.gameObject.layer != LayerMask.NameToLayer("Item")) return;
	    PlacedItemIn = true;
	    other.gameObject.transform.parent = MelonDropPoint;
	    other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
	    var data = other.gameObject.GetComponent<ItemSeedData>();
	    if (!data) return;
	    Transform spot;
	    switch (data.itemName)
	    {
	        case "CarKey": spot = CarKey; break;
	        case "CabinetKey": spot = ECKey; break;
	        case "PDKey": spot = PDKey; break;
	        case "MasterKey": spot = MasKey; break;
	        case "RedCog": case "OrangeCog": spot = Cog; break;
	        case "PlayKey": spot = PlayHouse; break;
	        case "Remote": spot = Remote; break;
	        case "RustyKey": spot = RustyKey; break;
	        case "SparkPlug": spot = SparkPlug; break;
	        case "WPKey": spot = WPKey; break;
	        default: return;
	    }
	    other.gameObject.transform.position = spot.position;
	    other.gameObject.transform.rotation = spot.rotation;
	}
}
