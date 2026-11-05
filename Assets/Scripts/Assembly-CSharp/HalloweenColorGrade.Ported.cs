using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Ported from Annotated-Pseudocode/HalloweenColorGrade.c (Granny Legacy, IL2CPP x64).
public partial class HalloweenColorGrade
{
	private void Start()
	{
	    Profile.TryGetSettings(out CG);
	    Profile2.TryGetSettings(out CG2);
	    Profile3.TryGetSettings(out CG3);
	}
	private void Grade(float exposure, float hue, float saturation, float contrast, Color? filter)
	{
	    foreach (var cg in new[] { CG, CG2, CG3 })
	    {
	        cg.postExposure.value = exposure; cg.hueShift.value = hue; cg.saturation.value = saturation; cg.contrast.value = contrast;
	    }
	    if (filter.HasValue) foreach (var cg in new[] { CG, CG2, CG3 }) cg.colorFilter.value = filter.Value;
	}
	private void Skins(Texture2D slendrina, Texture2D teddy, Texture2D bat)
	{
	    TSlendrina.Default = slendrina;
	    TSlendrina.Material.mainTexture = slendrina;
	    SlendrinaM.mainTexture = slendrina;
	    TeddyM.mainTexture = teddy;
	    TeddyM2.mainTexture = teddy;
	    if (bat != null) BatBloody.materials[1].mainTexture = bat;
	}
	private void Props(bool ears, bool lepre, bool summer, bool elf, bool valentine)
	{
	    Teddy_Ears.SetActive(ears); Teddy_Lepre.SetActive(lepre); Teddy_Summer.SetActive(summer);
	    Teddy_Elf.SetActive(elf); Teddy_Valentine.SetActive(valentine);
	}
	// Holiday colour grading, Slendrina/teddy/bat skins, music and teddy accessories:
	// 1 Halloween, 2 Christmas, 3 Valentine's, 4 March, 5 Easter, 6 pride, 7 summer, other = normal.
	private void Update()
	{
	    int holiday = PlayerPrefs.GetInt("Holiday");
	    bool nm = PlayerPrefs.GetInt("Textures") == 1;
	    if (holiday == 2)
	    {
	        Grade(PostExposureEV_CH, HueShift_CH, Saturation_CH, Contrast_CH, colorNormal);
	        if (nm) Skins(NM, TeddyNM, null); else Skins(SlendrinaNormal, TeddyNormal, null);
	        MusicChristmas.SetActive(true); MusicNormal.SetActive(false); MusicHalloween.SetActive(false);
	        Props(false, false, false, false, false);
	        CandyCane.material.color = new Color(.5188679f, .5188679f, .5188679f, 1);
	        LilSanta.SetActive(true);
	        TCWalls.Default = JollyWalls;
	        JollyDecoUI.SetActive(true);
	        Teddy_Elf.SetActive(true);
	    }
	    else
	    {
	        if (holiday == 1)
	        {
	            Grade(PostExposureEV_HA, HueShift_HA, Saturation_HA, Contrast_HA, colorHA);
	            TSlendrina.Default = SlendrinaHalloween;
	            TSlendrina.Material.mainTexture = SlendrinaHalloween;
	            if (nm) Skins(NM, TeddyNM, BatNM); else Skins(SlendrinaHalloween, TeddyHalloween, BatNormal);
	        }
	        else
	        {
	            Grade(PostExposureEV_Normal, HueShift_Normal, Saturation_Normal, Contrast_Normal, colorNormal);
	            if (nm) Skins(NM, TeddyNM, holiday == 6 ? BatGay : BatNM);
	            else Skins(SlendrinaNormal, TeddyNormal, holiday == 6 ? BatGay : BatNormal);
	        }
	        MusicChristmas.SetActive(false);
	        MusicNormal.SetActive(holiday != 1);
	        MusicHalloween.SetActive(holiday == 1);
	        Props(holiday == 5, holiday == 4, holiday == 7, false, holiday == 3);
	        LilSanta.SetActive(false);
	        TCWalls.Default = NormalWalls;
	        JollyDecoUI.SetActive(false);
	    }
	    foreach (var accessory in grandpaPrideAccessories) accessory.SetActive(holiday == 6);
	}
}
