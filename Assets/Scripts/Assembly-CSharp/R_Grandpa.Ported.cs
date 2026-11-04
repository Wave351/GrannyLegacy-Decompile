using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/R_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class R_Grandpa
{
	public R_Grandpa() { TimerFade = .2f; }
	private void Start()
	{
	    EC = GameObject.Find("EnemyController");
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if (g == 1) Cane.SetActive(true);
	    else if (g == 2 && EC.GetComponent<EnemyController>().ShotgunGrandpa.activeSelf) Shotgun.SetActive(true);
	    StartCoroutine(TimerGo());
	}
	// Grandpa's ragdoll turns green when poisoned, then fades out.
	private void Update()
	{
	    if (Fadin)
	    {
	        if (Skin != null)
	        {
	            Skin.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
	            Skin.material.color = SkinC;
	            SkinC.a -= Time.deltaTime * TimerFade;
	        }
	    }
	    else if (Poisoned) Skin.material.color = Color.Lerp(Skin.material.color, SkinPoisoned, Mathf.Clamp01(Time.deltaTime * .8f));
	    SkinC.a = Mathf.Clamp01(SkinC.a);
	}
	private IEnumerator TimerGo()
	{
	    yield return new WaitForSeconds(TimerGone);
	    TC.enabled = false;
	    Fadin = true;
	    Skin.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
	    Skin.material.mainTexture = null;
	    foreach (var o in ObjectsDisable)
	    {
	        if (o == null) break;
	        o.SetActive(false);
	        if (Shotgun != null) Destroy(Shotgun);
	    }
	    yield return new WaitForSeconds(3);
	    Destroy(gameObject);
	}
}
