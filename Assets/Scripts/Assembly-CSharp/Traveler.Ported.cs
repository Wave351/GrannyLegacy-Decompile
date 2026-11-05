using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Traveler.c (Granny Legacy, IL2CPP x64).
public partial class Traveler
{
	// Forest/traveler cutscene: fades in from black, Slendrina fades into view, Granny attacks, then back to the menu.
	private void Start()
	{
	    Cursor.visible = false;
	    StartCoroutine(TimerBlackFade());
	}
	private void Update()
	{
	    Slendrina.material.color = new Color(0.6226f, 0.6226f, 0.6226f, Alpha_Slendrina);
	    SlendrinaHair.material.color = new Color(0, 0, 0, Alpha_Hair);
	    if (Input.GetKeyDown(KeyCode.Escape)) UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	    if (SlendrinaFade)
	    {
	        Alpha_Slendrina += Time.deltaTime * 1.2f;
	        if (Alpha_Hair < 0.4f) Alpha_Hair += Time.deltaTime * 0.4f;
	    }
	}
	private IEnumerator TimerBlackFade()
	{
	    yield return new WaitForSeconds(1f);
	    Black.CrossFadeAlpha(0, 2.5f, false);
	}
	public void SlendrinaExpose() { GrandpaAnim.Play("GrandpaEntersHouse"); SlendrinaFade = true; }
	public void GrannyActivates() { GrannyBat.gameObject.transform.parent.gameObject.SetActive(true); }
	public void GrannyAttacks() { Scream.SetActive(true); GrannyBat.CrossFade("Granny_Attack"); }
	public void BloodScreenHappens() { BloodScreen.SetActive(true); }
	public void FadeAway() { Black.CrossFadeAlpha(1, 0.5f, false); }
	public void SceneMoveOut() { UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"); }
}
