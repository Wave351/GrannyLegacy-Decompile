using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemSpawn.c (Granny Legacy, IL2CPP x64).
public partial class ItemSpawn
{
	public ItemSpawn() { DropForceItem = 5; }
	// Spawned by Inventory.DropLogic: activates the item matching CountItem (the item's itemInt) and pushes it forward.
	private void Update()
	{
	    if (Spawned) return;
	    var items = new[] { crossbow, plier, battery, gas, seed, book, winch, carbattery, carkey, cutter, code, baton, eckey,
	        hammer, padlock, mas, kugg1, kugg2, meat, melon, spray, plank, playh, remote, data, rusty, safe, screw, shotgun,
	        shotgun2, sp1, sp2, sp3, spark, special, spider, syringe, t1, t2, t3, t4, teddy, text, topp, wp, vas, vas2, vas3,
	        wheel, stick, wrench, rat, ornamentfreeze, ornamentbomb };
	    int n = (int)CountItem;
	    GameObject item = n >= 1 && n <= items.Length && n == CountItem ? items[n - 1] : fuse;
	    item.SetActive(true);
	    var body = item.GetComponent<Rigidbody>();
	    if (!body) return;
	    body.AddForce(item.transform.forward * DropForceItem, ForceMode.Impulse);
	    item.transform.parent = null;
	    Spawned = true;
	    Destroy(gameObject);
	}
}
