using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ItemSeedData.c (Granny Legacy, IL2CPP x64).
public partial class ItemSeedData
{
	public void OnDestroy()
	{
		WeightController controller = UnityEngine.Object.FindObjectOfType<WeightController>();
		if (controller != null)
		{
			controller.RemoveItem(this);
		}
	}
}
