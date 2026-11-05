using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/WeightController.c (Granny Legacy, IL2CPP x64).
public partial class WeightController
{
	public WeightController() { WeightFade = 2.5f; WeightUnlock = 5; StillIntact = true; CurrentItems = new HashSet<ItemSeedData>(); }
	// The scale in the secret room opens its hatch at 5 kg of items; the display eases toward the real weight.
	private void Start()
	{
	    previousWeight = Weight;
	    UpdateWeightText();
	    ItemWeights = new Dictionary<string, float> {
	        { "Crossbow", 2 }, { "Pliers", .5f }, { "Battery", 1 }, { "Gas", 2 }, { "BirdSeed", .5f }, { "Book", .5f }, { "Winch", 1 },
	        { "CarBattery", 3 }, { "ChainCutter", 2.7f }, { "ElectricBaton", .5f }, { "Hammer", 1 }, { "Melon", 1.5f }, { "Screwdriver", 1 },
	        { "RegShotgun", 2 }, { "Shotgun_Trigger", 1 }, { "Shotgun_Buttstock", 1 }, { "Shotgun_Barrel", 1 }, { "Message", 1 },
	        { "Engine", 3.5f }, { "WheelCrank", 1 }, { "WoodenStick", 1 }, { "Wrench", 1 }, { "Vase", 1 }, { "Vase2", 1 },
	        { "FreezeTrap", 1.5f }, { "PoisonTrap", 1.5f }, { "Plank", 3.5f }, { "Meat", .8f }, { "GRVase", 1 } };
	}
	private void Update()
	{
	    if (!StillIntact) return;
	    if (Weight < MaxWeight) Weight += Time.deltaTime * WeightFade;
	    else if (MaxWeight < Weight) Weight -= Time.deltaTime * WeightFade;
	    if (.01f < Mathf.Abs(previousWeight - Weight)) { UpdateWeightText(); previousWeight = Weight; }
	    if (WeightUnlock <= Weight)
	    {
	        Lamp.material.color = Color.green;
	        if (IfOpen) return;
	        IfOpen = true;
	        OpenDoor.Play("OpenItemLucka");
	    }
	    else Lamp.material.color = Color.red;
	}
	private void UpdateWeightText() { if (TextKG) TextKG.text = string.Format("{0:F1} KG", Weight); }
	private void OnTriggerEnter(Collider other)
	{
	    ItemSeedData item;
	    float w;
	    if (!other.TryGetComponent(out item))
	    {
	        if (!other.gameObject.CompareTag("Shotgun2")) return;
	        MaxWeight += 2.2f;
	        ShotgunInPlace = true;
	    }
	    else
	    {
	        if (CurrentItems.Contains(item) || !ItemWeights.TryGetValue(item.itemName, out w)) return;
	        CurrentItems.Add(item);
	        MaxWeight += w;
	    }
	    if (StillIntact) AS.PlayOneShot(AddedWeight);
	}
	private void OnTriggerExit(Collider other)
	{
	    ItemSeedData item;
	    if (other.TryGetComponent(out item)) { RemoveItem(item); return; }
	    if (!other.gameObject.CompareTag("Shotgun2")) return;
	    RevSH2Always();
	}
	public void RemoveItem(ItemSeedData item)
	{
	    float w;
	    if (!CurrentItems.Contains(item) || !ItemWeights.TryGetValue(item.itemName, out w)) return;
	    CurrentItems.Remove(item);
	    MaxWeight -= w;
	    if (StillIntact) AS.PlayOneShot(RemovedWeight);
	}
	private void RevSH2Always()
	{
	    MaxWeight -= 2.2f;
	    ShotgunInPlace = false;
	    if (StillIntact) AS.PlayOneShot(RemovedWeight);
	}
	public void RevSH2() { if (ShotgunInPlace) RevSH2Always(); }
}
