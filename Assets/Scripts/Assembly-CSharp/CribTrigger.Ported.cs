using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CribTrigger.c (Granny Legacy, IL2CPP x64).
public partial class CribTrigger
{
	public CribTrigger() { SpeedFadeSlendrina = .5f; SpeedFadeSlendrinaH = .2f; FadeHatSpeed = 2; }
	private void Start()
	{
	    PlayerPrefs.SetInt("Teddied", 0);
	    TeddyM.shader = Shader.Find("Particles/Standard Unlit");
	    TeddyM.color = new Color(.5943396f, .5943396f, .5943396f, 1);
	    StartTeddy = TeddyM.color;
	    mats = new Material[RS.Length];
	    for (int i = 0; i < RS.Length; i++)
	    {
	        if (RS[i] == null) continue;
	        mats[i] = RS[i].material;
	        SetMaterialToFadeMode(mats[i]);
	    }
	}
	private void Update()
	{
	    TeddyM.color = StartTeddy;
	    SlendrinaBodyM.color = StartSlendrinaBody;
	    SlendrinaHairM.color = StartSlendrinaHair;
	    if (!SlendrinaFades)
	    {
	        StartSlendrinaBody.a -= Time.deltaTime * SpeedFadeSlendrina;
	        StartSlendrinaHair.a -= Time.deltaTime * SpeedFadeSlendrinaH;
	    }
	    else
	    {
	        StartSlendrinaBody.a += Time.deltaTime * SpeedFadeSlendrina;
	        if (StartSlendrinaHair.a < .388235f) StartSlendrinaHair.a += Time.deltaTime * SpeedFadeSlendrinaH;
	    }
	    if (TeddyFade) StartTeddy.a -= Time.deltaTime * .5f;
	    StartTeddy.a = Mathf.Clamp01(StartTeddy.a);
	    StartSlendrinaBody.a = Mathf.Clamp01(StartSlendrinaBody.a);
	    StartSlendrinaHair.a = Mathf.Clamp01(StartSlendrinaHair.a);
	    if (Slendrina.activeSelf)
	    {
	        Vector3 p = PlayerP.position;
	        Slendrina.transform.LookAt(new Vector3(p.x, Slendrina.transform.position.y, p.z));
	    }
	    var white = WhiteScreen.color;
	    WhiteScreen.color = new Color(white.r, white.g, white.b, AlphaWhite);
	    if (!FadeWhiteScreenIn) { if (0 < AlphaWhite) AlphaWhite -= Time.deltaTime * .2f; }
	    else if (AlphaWhite <= 1.1f) AlphaWhite += Time.deltaTime * 1.5f;
	    if (!HatAvailable) return;
	    alpha = Mathf.MoveTowards(alpha, FadeTheHats ? 1 : 0, Time.deltaTime * FadeHatSpeed);
	    ApplyAlpha();
	}
	// Putting the teddy in the crib: Slendrina appears for a while, the house enemies are sent away (or, with Slendrina as
	// an enemy, she is calmed), and "Teddied" unlocks the good ending.
	private void OnTriggerEnter(Collider other)
	{
	    var data = other.gameObject.GetComponent<ItemSeedData>();
	    if (!data || data.itemName != "Teddy") return;
	    Destroy(other.gameObject.GetComponent<ItemSeedData>());
	    GlowEffect.SetActive(true);
	    if (Granny.activeSelf)
	    {
	        var ai = Granny.GetComponent<AI_Granny>();
	        ai.Agent.enabled = false;
	        Granny.transform.position = Pos1.position;
	        ai.Agent.enabled = true;
	        if (ai.IsChasing) ai.EnemyVision.TimerStopVision = 100;
	    }
	    if (Grandpa.activeSelf)
	    {
	        var ai = Grandpa.GetComponent<AI_Grandpa>();
	        ai.Agent.enabled = false;
	        Grandpa.transform.position = Pos2.position;
	        ai.Agent.enabled = true;
	        if (ai.IsChasing) ai.EnemyVision.TimerStopVision = 100;
	    }
	    TeddyM.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
	    TeddyFade = true;
	    StartCoroutine(SlendrinaTimer());
	    TeddyObj = other.gameObject;
	    if (TeddyObj.GetComponent<Outline>() != null) Destroy(TeddyObj.GetComponent<Outline>());
	    if (PlayerPrefs.GetInt("SlendrinaEnemy") == 1)
	    {
	        EnemyHouse.SetActive(false);
	        if (ShakeAnim.IsPlaying("LookAtSlendrina")) ShakeAnim.Play("IdleShake");
	        StartCoroutine(SlendrinaBegoneEnemy());
	    }
	    if (PlayerPrefs.GetInt("FlashLightIsOn") != 1) return;
	    var flash = other.gameObject.GetComponent<FlashlightLogic>();
	    if (!flash) return;
	    flash.enabled = false;
	    var renderer = other.gameObject.GetComponent<Renderer>();
	    if (renderer) renderer.sharedMaterial = TeddyM;
	}
	private IEnumerator SlendrinaTimer()
	{
	    yield return new WaitForSeconds(2);
	    Destroy(TeddyObj);
	    Slendrina.SetActive(true);
	    SlendrinaFades = true; HatAvailable = true; FadeTheHats = true;
	    PlayerPrefs.SetInt("SlendrinaUnlock", 1);
	    yield return new WaitForSeconds(10);
	    Smoke.Stop();
	    SlendrinaFades = false; FadeTheHats = false;
	    yield return new WaitForSeconds(2);
	    var col = Slendrina.GetComponent<Collider>();
	    if (!col) yield break;
	    col.enabled = false;
	    if (PlayerPrefs.GetInt("SlendrinaEnemy") == 1 && PlayerPrefs.GetInt("FlashL") == 1) ItemDropPoint.SetActive(true);
	    yield return new WaitForSeconds(4);
	    Slendrina.SetActive(false);
	    PlayerPrefs.SetInt("Teddied", 1);
	}
	private IEnumerator SlendrinaBegoneEnemy()
	{
	    yield return new WaitForSeconds(7);
	    yield return new WaitForSeconds(1.5f);
	    SlendrinaFades = false; FadeTheHats = false;
	    yield return new WaitForSeconds(1);
	    var col = Slendrina ? Slendrina.GetComponent<Collider>() : null;
	    if (col) col.enabled = false;
	}
	public void ApplyAlpha()
	{
	    foreach (var m in mats)
	    {
	        if (m == null) continue;
	        var c = m.color; c.a = alpha; m.color = c;
	    }
	}
	public void SetMaterialToFadeMode(Material m)
	{
	    m.SetFloat("_Mode", 2);
	    m.SetInt("_SrcBlend", 5);
	    m.SetInt("_DstBlend", 10);
	    m.SetInt("_ZWrite", 0);
	    m.DisableKeyword("_ALPHATEST_ON");
	    m.EnableKeyword("_ALPHABLEND_ON");
	    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
	    m.renderQueue = 3000;
	}
}
