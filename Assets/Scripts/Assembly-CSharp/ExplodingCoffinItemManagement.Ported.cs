using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ExplodingCoffinItemManagement.c (Granny Legacy, IL2CPP x64).
public partial class ExplodingCoffinItemManagement
{
	// The item placed in the exploding-coffins room goes into one of the two coffins at random; that coffin's number pad
	// appears and ObjectsManager.NumberChoosen remembers which coffin is safe (DoorRay blows up the other one).
	private void OnTriggerEnter(Collider other)
	{
	    if (PlacedItemInItsSpot) return;
	    var data = other.gameObject.GetComponent<ItemSeedData>();
	    if (!data) return;
	    Transform p1, p2;
	    switch (data.itemName)
	    {
	        case "Winch": p1 = WinchHandle_P1; p2 = WinchHandle_P2; break;
	        case "RoboData": p1 = RoboData_P1; p2 = RoboData_P2; break;
	        case "Pliers": p1 = Pliers_P1; p2 = Pliers_P2; break;
	        case "SPKey": p1 = SPKey_P1; p2 = SPKey_P2; break;
	        case "SafeKey": p1 = SafeKey_P1; p2 = SafeKey_P2; break;
	        case "Melon": p1 = Melon_P1; p2 = Melon_P2; break;
	        case "PDKey": p1 = PDKey_P1; p2 = PDKey_P2; break;
	        case "CarKey": p1 = CarKey_P1; p2 = CarKey_P2; break;
	        case "Meat": p1 = Meat_P1; p2 = Meat_P2; break;
	        case "Remote": p1 = Remote_P1; p2 = Remote_P2; break;
	        case "WheelCrank": p1 = WheelCrank_P1; p2 = WheelCrank_P2; break;
	        case "RustyKey": p1 = RustyKey_P1; p2 = RustyKey_P2; break;
	        case "MasterKey": p1 = MasterKey_P1; p2 = MasterKey_P2; break;
	        case "Hammer": p1 = Hammer_P1; p2 = Hammer_P2; break;
	        case "RedCog": p1 = CogRed_P1; p2 = CogRed_P2; break;
	        case "OrangeCog": p1 = CogOrange_P1; p2 = CogOrange_P2; break;
	        case "BirdSeed": p1 = BirdSeed_P1; p2 = BirdSeed_P2; break;
	        case "Wrench": p1 = Wrench_P1; p2 = Wrench_P2; break;
	        default: return;
	    }
	    int coffin = UnityEngine.Random.Range(1, 3);
	    Transform spot = coffin == 1 ? p1 : p2;
	    OM.NumberChoosen = coffin;
	    (coffin == 1 ? NumberPad1 : NumberPad2).SetActive(true);
	    other.gameObject.transform.position = spot.position;
	    other.gameObject.transform.rotation = spot.rotation;
	    PlacedItemInItsSpot = true;
	}
}
