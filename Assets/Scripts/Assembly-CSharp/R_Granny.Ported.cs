using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/R_Granny.c (Granny Legacy, IL2CPP x64).
public partial class R_Granny
{
	public R_Granny() { TimerFade = .2f; }
	private void Start()
	{
	    EC = GameObject.Find("EnemyController");
	    StartCoroutine(TimerGo());
	    var ai = EC.GetComponent<EnemyController>().Granny.GetComponent<AI_Granny>();
	    int weapon = PlayerPrefs.GetInt("GrannyWP");
	    if (weapon == 1 && ai.Crossbow.activeSelf) Crossbow.SetActive(true);
	    else if (weapon == 2 && ai.Spray.activeSelf) PepperSpray.SetActive(true);
	}
	// Granny's ragdoll: turns green when poisoned, fades out, and puts her bloody bat back in the house when the player dies.
	private void Update()
	{
	    if (Fadin)
	    {
	        Skin.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
	        Skin.material.color = SkinC;
	        F1.material.color = FC; F2.material.color = FC;
	        SkinC.a -= Time.deltaTime * TimerFade;
	        FC.a -= Time.deltaTime * TimerFade;
	    }
	    else if (Poisoned) Skin.material.color = Color.Lerp(Skin.material.color, SkinPoisoned, Mathf.Clamp01(Time.deltaTime * .8f));
	    SkinC.a = Mathf.Clamp01(SkinC.a);
	    FC.a = Mathf.Clamp01(FC.a);
	    if (BatBloodyLight == null) BatBloodyLight = GameObject.Find("BatBloodyRagdoll");
	    else if (!PositionBat && PlayerPrefs.GetInt("GrannyWP") == 0)
	    {
	        PositionBat = true;
	        BatBloodyLight.transform.parent = Hand;
	        BatBloodyLight.transform.position = PositionBatBloody.position;
	        BatBloodyLight.transform.rotation = PositionBatBloody.rotation;
	    }
	    if (GrannyStuff == null) GrannyStuff = GameObject.Find("Granny-Stuff");
	    if (PS == null) PS = GameObject.Find("PlayerManage");
	    if (RepositionBatPos == null) RepositionBatPos = GameObject.Find("BatBloodyReposition");
	    var status = PS ? PS.GetComponent<PlayerStatus>() : null;
	    if (!status || !status.Killed) return;
	    ReturnBat();
	}
	private void ReturnBat()
	{
	    BatBloodyLight.transform.parent = GrannyStuff.transform;
	    if (!RepositionBatPos) return;
	    BatBloodyLight.transform.position = RepositionBatPos.transform.position;
	    BatBloodyLight.transform.rotation = RepositionBatPos.transform.rotation;
	}
	private IEnumerator TimerGo()
	{
	    yield return new WaitForSeconds(TimerGone);
	    TC.enabled = false;
	    Fadin = true;
	    var shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
	    Skin.material.shader = shader; F1.material.shader = shader; F2.material.shader = shader;
	    Skin.material.mainTexture = null; F1.material.mainTexture = null; F2.material.mainTexture = null;
	    foreach (var o in ObjectsDisable) o.SetActive(false);
	    ReturnBat();
	    if (!RepositionBatPos) yield break;
	    yield return new WaitForSeconds(3);
	    Destroy(gameObject);
	}
}
