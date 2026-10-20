using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<ItemDefs> ItemDefs;

	public AudioSource Audio;

	public GameObject ItemsPrefab;

	public void PickupItem(string itemName)
	{
	}

	public ItemDefs GetItemDefByName(string name)
	{
		return null;
	}

	public void DropLogic()
	{
	}

	public ItemDefs GetItem(string itemName)
	{
		return null;
	}
}
