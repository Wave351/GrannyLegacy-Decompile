using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PushSecretDoorTrigger.c (Granny Legacy, IL2CPP x64).
public partial class PushSecretDoorTrigger
{
	private void Update()
	{
		if (IsClose && CanPush)
		{
			PushDoor.Play("SecretDoorOpen");
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public void ClickButton()
	{
		StartCoroutine(OpenLittle());
	}

	public IEnumerator OpenLittle()
	{
		PushDoor.Play("SecretDoorOpenLittle");
		yield return new WaitForSeconds(0.2f);
		CanPush = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			IsClose = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			IsClose = false;
		}
	}
}
