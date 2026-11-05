using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SpecialEffectsManager.c (Granny Legacy, IL2CPP x64).
public partial class SpecialEffectsManager
{
	// "SPE" special effects, and the StreamingAssets switches: lavaMode.txt (lava atmosphere), enemySpawn1-4.txt (fixed
	// speedrun enemy spawn) and extraTraps.txt.
	private void Start()
	{
	    if (PlayerPrefs.GetInt("SPE") == 1) { Coffin1.SetActive(true); Coffin2.SetActive(true); MomEff.SetActive(true); ToiletEff.SetActive(true); }
	    StartCoroutine(TimerDestroyThis());
	    string assets = Application.streamingAssetsPath;
	    if (System.IO.File.Exists(System.IO.Path.Combine(assets, "lavaMode.txt"))) lavaAtmosphereOn = true;
	    for (int i = 1; i <= 4; i++)
	        if (System.IO.File.Exists(System.IO.Path.Combine(assets, "enemySpawn" + i + ".txt")))
	        { EC.spawnSelected = i - 1; EC.speedrunAdditionSpawnOn = true; }
	    if (System.IO.File.Exists(System.IO.Path.Combine(assets, "extraTraps.txt"))) extraTraps.SetActive(true);
	    if (!lavaAtmosphereOn) { PlayerPrefs.SetInt("LavaFrance", 0); return; }
	    lavaRise.SetActive(true);
	    AIGranny.VoiceLinesEnemy = grannyLinesLava;
	    GrandpaEyes.YappingClips = grandpaLinesLava;
	    toiletFlush.pitch = .4f;
	    PlayerPrefs.SetInt("LavaFrance", 1);
	}
	private static void SetAll(GameObject[] objects, bool on) { foreach (var o in objects) o.SetActive(on); }
	private void Props(bool brooms, bool canes, bool arrows)
	{
	    SetAll(Bats, false); SetAll(Arrows, arrows); SetAll(Canes, canes); SetAll(Brooms, brooms);
	    RendererBat.enabled = false;
	}
	private bool PlainTextures { get { return PlayerPrefs.GetInt("Textures") == 0 && !lavaAtmosphereOn; } }
	private void SkinGranny(Texture2D skin) { foreach (var m in GrannySkins) m.mainTexture = skin; }
	private void Music(AudioSource source, AudioClip clip) { if (source.clip != clip) { source.clip = clip; source.Play(); } }
	// Native quirk: the background-music checks compare the AudioSource object itself to the clip, which is never equal,
	// so BGMusic is reassigned and restarted every frame until this manager destroys itself after 9 seconds.
	private void BGRestart(AudioClip clip) { if ((UnityEngine.Object)BGMusic != clip) { BGMusic.clip = clip; BGMusic.Play(); } }
	// Holiday themes: 1 Halloween, 2 Christmas, 3 Valentine's, 4 March, 5 Easter, 6 pride, 7 summer.
	private void Update()
	{
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    if (holiday == 7)
	    {
	        RenderSettings.skybox = SkySummer;
	        SummerWater.SetActive(true);
	        Music(BackyardAtmosphere, BeachBY);
	    }
	    else if (holiday == 6)
	    {
	        foreach (var bat in Bats)
	        {
	            var renderer = bat.GetComponent<Renderer>();
	            var mats = renderer.materials;
	            for (int i = 0; i < mats.Length; i++) if (mats[i].name.Contains("ASHSEN_3")) mats[i] = BatQueer;
	            bat.GetComponent<Renderer>().sharedMaterials = mats;
	        }
	        GrannyPride.SetActive(true);
	        InMouthGrannyPride.SetActive(true);
	        SetAll(GrandpaPrideAcessories, true);
	        GrandpaEyes.OriginalGrandpaPitch = 1.35f;
	        if (PlainTextures) SkinGranny(GrannyLGBT);
	        rendererWaterSewer.sharedMaterial = gayWater;
	        rendererWaterSewer2.sharedMaterial = gayWater;
	    }
	    else if (holiday == 5) { if (PlainTextures) SkinGranny(GrannyBunny); }
	    else if (holiday == 4) { if (PlainTextures) SkinGranny(GrannyLepre); }
	    else if (holiday == 3) Props(false, false, true);
	    else if (holiday == 2)
	    {
	        Props(false, true, false);
	        if (PlainTextures) SkinGranny(GrannySanta);
	        if (PlainTextures)
	        {
	            BGRestart(ChristmasBG);
	            HuntMusic.clip = ChaseChristmas;
	            HuntMusic.pitch = 1.1f;
	            BGMusic.pitch = 1;
	            MomMain.mainTexture = MomChristmas;
	        }
	        // Krampus Grandpa.
	        TeddyHunt.GrandpaEyesNormal = GrandpaKrampusEyes;
	        TeddyHunt.GrandpaEyeM.material.color = GrandpaKrampusEyes;
	        TeddyHunt.GrandpaEyesSetDark = GrandpaKrampusEyes;
	        GrandpaEyes.OriginalGrandpaPitch = .9f;
	        GrandpaFeetSource.pitch = .7f;
	        TheEndT.sprite = MerryChristmasText;
	    }
	    else if (holiday == 1)
	    {
	        if (PlainTextures)
	        {
	            SkinGranny(GrannyWitch);
	            BGRestart(HalloBG);
	            GrannyFeetSource.pitch = 1.3f;
	            GrandpaEyes.OriginalGrandpaPitch = .8f;
	            Elevator.HalloweenThemed = true;
	            Elevator.HissSound.pitch = 1.25f;
	            HuntMusic.clip = HalloChase;
	            HuntMusic.pitch = 1.1f;
	            BGMusic.pitch = 1.2f;
	            Music(BackyardAtmosphere, HalloweenBY);
	        }
	        AIGranny.JumpscarePlayer = AttackHalloween;
	        Props(true, false, false);
	        RenderSettings.skybox = SkyHalloween;
	    }
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (diff == 3 || diff == 4 || diff == 5 || PlayerPrefs.GetInt("Dark") == 1 || PlayerPrefs.GetInt("FlashLightIsOn") == 1) RenderSettings.skybox = null;
	    if (!lavaAtmosphereOn) return;
	    SkinGranny(GrannyLava);
	    MomMain.mainTexture = MomLava;
	    BGRestart(LavaBG);
	    Elevator.HalloweenThemed = true;
	    Elevator.HissSound.pitch = 1.25f;
	    HuntMusic.clip = LavaChase;
	    HuntMusic.pitch = 1.1f;
	}
	private IEnumerator TimerDestroyThis() { yield return new WaitForSeconds(9); Destroy(gameObject); }
}
