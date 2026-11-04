using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Inventory.c (Granny Legacy, IL2CPP x64).
public partial class Inventory
{
	public Inventory() { ItemDefs = new List<ItemDefs>(); }
	public void PickupItem(string itemName)
	{
	    var def = ItemDefs.Find(d => d.itemName == itemName);
	    DropLogic();
	    if (def == null) return;
	    if (def.handObject) def.handObject.SetActive(true);
	    if (Audio && def.pickAudioClip != null) Audio.PlayOneShot(def.pickAudioClip);
	}
	public ItemDefs GetItemDefByName(string name)
	{
	    for (int i = 0; i < ItemDefs.Count; i++)
	        if (ItemDefs[i].itemName == name) return ItemDefs[i];
	    return null;
	}
	// Drops whatever is held: hides its hand model and spawns ItemsPrefab (an ItemSpawn) at the item's drop point.
	public void DropLogic()
	{
	    foreach (var def in ItemDefs)
	    {
	        if (!def.handObject || !def.handObject.activeSelf) continue;
	        def.handObject.SetActive(false);
	        if (!ItemsPrefab || !def.dropPoint) continue;
	        var go = Instantiate(ItemsPrefab, def.dropPoint.position, def.dropPoint.rotation);
	        go.GetComponent<ItemSpawn>().CountItem = def.itemInt;
	    }
	}
	public ItemDefs GetItem(string itemName) { return ItemDefs.Find(d => d.itemName == itemName); }
}
