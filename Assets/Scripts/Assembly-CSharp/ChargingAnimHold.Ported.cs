using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ChargingAnimHold.c (Granny Legacy, IL2CPP x64).
public partial class ChargingAnimHold
{
	public void StartCharge()
	{
		IsLoading = true;
	}

	public void StopCharge()
	{
		IsLoading = false;
	}

	public void GivePoints()
	{
		Ray.ChargePoints = 2f;
	}

	public void RayPickBaton()
	{
		if (!Status.Killed)
		{
			Ray.PickBaton();
			return;
		}
		GameObject item = UnityEngine.Object.Instantiate(Item, transform.position, transform.rotation);
		ItemSpawn spawn = item != null ? item.GetComponent<ItemSpawn>() : null;
		if (spawn != null)
		{
			spawn.CountItem = 12f;
		}
	}

	public void PlayIt()
	{
		Animation anim = GetComponent<Animation>();
		if (anim != null)
		{
			anim.Play("RechargeBaton");
		}
	}
}
