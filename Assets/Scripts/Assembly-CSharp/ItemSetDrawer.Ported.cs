using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemSetDrawer.c (Granny Legacy, IL2CPP x64).
public partial class ItemSetDrawer
{
	private void Start() { StartCoroutine(SetItOff()); }
	// The drawer only takes an item in the first 5 seconds, then closes.
	private IEnumerator SetItOff()
	{
	    yield return new WaitForSeconds(5);
	    PlacedItemIn = true;
	    Anim.Play(CloseDoor.name);
	}
	// Item numbers (ItemSpawn.CountItem) of the items Slendrina can hide in her drawer store.
	private static readonly Dictionary<string, float> StoreItems = new Dictionary<string, float> {
	    { "MasterKey", 16 }, { "CarKey", 9 }, { "RedCog", 17 }, { "OrangeCog", 18 }, { "RoboData", 25 }, { "PDKey", 15 },
	    { "SafeKey", 27 }, { "Pliers", 2 }, { "WPKey", 45 }, { "PlayKey", 23 }, { "Shotgun_Trigger", 31 }, { "SparkPlug", 34 },
	    { "Meat", 19 }, { "Wrench", 51 }, { "Remote", 24 }, { "RustyKey", 26 }, { "WheelCrank", 49 }, { "SPKey", 35 } };
	// An item dropped in: normally it stays in the drawer; with Slendrina in a seeded run it is moved to her item store instead
	// (any other item is destroyed).
	private void OnTriggerEnter(Collider other)
	{
	    if (PlacedItemIn || other.gameObject.layer != LayerMask.NameToLayer("Item")) return;
	    if (PlayerPrefs.GetInt("SlendrinaEnemy") == 1 && PlayerPrefs.GetInt("FlashL") == 1)
	    {
	        var data = other.gameObject.GetComponent<ItemSeedData>();
	        float count;
	        if (StoreItems.TryGetValue(data.itemName, out count))
	        {
	            var copy = Instantiate(Item, SlendrinaItemsStore.transform.position, SlendrinaItemsStore.transform.rotation);
	            var spawn = copy.GetComponent<ItemSpawn>();
	            spawn.CountItem = count;
	            spawn.DropForceItem = 0;
	            copy.transform.parent = SlendrinaItemsStore.transform;
	        }
	        PlacedItemIn = true;
	        Destroy(other.gameObject);
	    }
	    else
	    {
	        PlacedItemIn = true;
	        other.transform.SetParent(transform, true);
	    }
	    Anim.Play(CloseDoor.name);
	}
}
