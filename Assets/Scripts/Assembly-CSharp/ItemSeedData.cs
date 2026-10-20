using System.Collections.Generic;
using UnityEngine;

public class ItemSeedData : MonoBehaviour
{
	public string itemName;

	[Tooltip("1=escape-only, 2=escape+puzzle, 3=puzzle-only, 3=free")]
	public int category;

	public List<string> containedItems;

	public void OnDestroy()
	{
	}
}
