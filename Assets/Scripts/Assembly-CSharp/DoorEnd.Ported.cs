using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Ported from Annotated-Pseudocode/DoorEnd.c (Granny Legacy, IL2CPP x64).
public partial class DoorEnd
{
	// Escape through the front door: Granny (and Grandpa) at home, or the good ending with the teddy ("Teddied").
	private void Start()
	{
	    P.TryGetSettings(out CA); P.TryGetSettings(out DOF); P.TryGetSettings(out V); P.TryGetSettings(out G);
	    CA.intensity.value = 0; DOF.focalLength.value = 1; V.intensity.value = 0; G.intensity.value = 0;
	    MHair.color = new Color(0, 0, 0, .388235f);
	    MSlendrina.color = new Color(.622642f, .622642f, .622642f, 1);
	    foreach (var r in EyesTransition) { r.material.shader = Shader.Find("Particles/Standard Unlit"); r.material.color = Color.white; }
	    Black.CrossFadeAlpha(0, 1, false);
	    Grandpa.transform.position = GrandpaSpawnEnding.position;
	    Grandpa.transform.rotation = GrandpaSpawnEnding.rotation;
	    int grandpa = PlayerPrefs.GetInt("Grandpa");
	    bool withGrandpa = grandpa == 1 || grandpa == 2;
	    if (grandpa == 1) { Cane.SetActive(true); Grandpa.SetActive(true); }
	    else if (grandpa == 2) { Shotgun.SetActive(true); Grandpa.SetActive(true); }
	    bool teddied = PlayerPrefs.GetInt("Teddied") == 1;
	    if (PlayerPrefs.GetInt("Granny") == 1)
	    {
	        if (!teddied)
	        {
	            Granny.SetActive(true);
	            GrannyAnim.Play("Granny_Suspicious");
	            Granny.transform.position = GrannyHomeSpawn.position;
	            Granny.transform.rotation = GrannyHomeSpawn.rotation;
	        }
	        else
	        {
	            Granny.SetActive(true);
	            Granny.transform.position = GrannyHomeGoodEndingSpawn.position;
	            Granny.transform.rotation = GrannyHomeGoodEndingSpawn.rotation;
	            TeddyHeld.SetActive(true);
	            Cam2.SetActive(true); Cam1.SetActive(false);
	            Shotgun.SetActive(false); Cane.SetActive(false);
	            Slendrina.SetActive(true);
	            if (withGrandpa)
	            {
	                GrandpaAnim.Play("dancing");
	                TeddyHeld.SetActive(false); TeddyGrandpa.SetActive(true);
	                GrannyAnim.Play("GrannyGoodEnding_NoTeddy");
	                Shotgun.SetActive(false); Cane.SetActive(false);
	            }
	            else GrannyAnim.Play("GrannyGoodEnding_Teddy");
	        }
	    }
	    else
	    {
	        if (teddied)
	        {
	            TeddyHeld.SetActive(true);
	            TeddyHeld.transform.position = TeddyGroundSpawn.position;
	            TeddyHeld.transform.rotation = TeddyGroundSpawn.rotation;
	            Slendrina.SetActive(true);
	            if (withGrandpa)
	            {
	                TeddyHeld.SetActive(false); Shotgun.SetActive(false); Cane.SetActive(false);
	                TeddyGrandpa.SetActive(true);
	                GrandpaAnim.Play("dancing");
	            }
	        }
	        Granny.SetActive(true);
	        GrannyAnim.Play("Granny_Suspicious");
	        Granny.transform.position = GrannyHomeCameBackSpawn.position;
	        Granny.transform.rotation = GrannyHomeCameBackSpawn.rotation;
	    }
	    StartCoroutine(TimerCam1());
	    int weapon = PlayerPrefs.GetInt("GrannyWP");
	    if (weapon == 1) { Bats.SetActive(false); Crossbow.SetActive(true); }
	    else if (weapon == 2) { Bats.SetActive(false); Spray.SetActive(true); }
	}
	private void Update()
	{
	    if (HatGrandpa.activeSelf && GrandpaHorns.enabled) GrandpaEyes.material.color = KrampusEyesGrandpa;
	}
	private IEnumerator TimerCam1()
	{
	    yield return new WaitForSeconds(10);
	    if (PlayerPrefs.GetInt("Teddied") == 0 && PlayerPrefs.GetInt("Granny") == 1)
	    {
	        Move.Play("GrannyMoveCamera");
	        GrannyAnim.Play("idle_3");
	    }
	    yield return new WaitForSeconds(2);
	    if (!Black) yield break;
	    Black.CrossFadeAlpha(1, .4f, false);
	    yield return new WaitForSeconds(3);
	    SceneManager.LoadScene("Menu");
	}
}
