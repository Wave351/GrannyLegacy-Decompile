using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PrisondoorGranny.c (Granny Legacy, IL2CPP x64).
public partial class PrisondoorGranny
{
	public void GrannyAttempts()
	{
		Animation anim = PCT.PrisonDoor.GetComponent<Animation>();
		anim.Stop();
		anim.Play("prisondoorLocked");
		PCT.PrisonDoor.GetComponent<AudioSource>().PlayOneShot(PCT.LockedSound);
	}

	public void GrannySmackDoor()
	{
		Sprint1.DropSprint();
		Sprint2.DropSprint();
		Sprint1.name = "No";
		Sprint2.name = "No";
	}
}
