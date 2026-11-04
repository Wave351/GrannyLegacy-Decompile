using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EndCam3.c (Granny Legacy, IL2CPP x64).
public partial class EndCam3
{
	public void SpawnHer() { GrannyInCar.SetActive(true); }
	public void ReverseE() { Reverse.Play(); Car.Play(Back.name); }
	public void ForwardE()
	{
	    Forward.Play();
	    Car["CarForward"].speed = .7f;
	    Car.Play(Front.name);
	}
	public void StartCar()
	{
	    Start.SetActive(true); Wave.SetActive(true);
	    SparkPut.SetActive(false); EnginePut.SetActive(false); BatteryPut.SetActive(false);
	    if (CarHood != null) CarHood.SetActive(false);
	    SlendrinaSpiritCar.SetActive(true);
	}
	public void Send() { GO.EndProcess(); }
	public void Fade() { GO.Fadin = true; }
	public void FadeOut() { GO.Fadin = false; }
}
