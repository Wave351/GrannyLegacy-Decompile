using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/BGManager.c (Granny Legacy, IL2CPP x64).
public partial class BGManager
{
	public BGManager()
	{
		MaxHouse = 0.7f; MaxAttic = 0.7f; MaxOutside = 0.7f; MaxCellar = 0.5f;
		MaxHouseMusic = 0.3f; MaxCellarMusic = 0.4f; MaxMom = 0.7f; MaxGranny = 0.8f; SpeedChaseFade = 2f;
	}
	private void Start()
	{
		bool music = PlayerPrefs.GetInt("Music", 1) == 1;
		if (Music != null) Music.SetIsOnWithoutNotify(music);
		SetObject(MusicG, music);
	}
	private void Update()
	{
		FadeChase(ChaseNormal, GrannyHunting, MaxGranny);
		FadeChase(ChaseMom, MomHunting, MaxMom);
		if (FanNoise != null) FanNoise.mute = !InAttic;
		if (CanFade)
		{
			if (!InCellar)
			{
				FadeAmbient(HouseMusic, !GrannyHunting, MaxHouseMusic, FadeBackBG);
				FadeAmbient(CellarMusic, false, MaxCellarMusic, FadeBackBG);
			}
			else
			{
				FadeAmbient(HouseMusic, false, MaxHouseMusic, MomHunting ? Speed : FadeBackBG);
				FadeAmbient(CellarMusic, !MomHunting, MaxCellarMusic, MomHunting ? Speed : FadeBackBG);
			}
			int region = InAttic ? 0 : InHouse ? 1 : InOutside ? 2 : InCellar ? 3 : -1;
			if (region >= 0)
			{
				if (WJ != null) WJ.WentOutside = region == 2;
				FadeAmbient(Attic, region == 0, MaxAttic, Speed);
				FadeAmbient(House, region == 1, MaxHouse, Speed);
				FadeAmbient(Outside, region == 2, MaxOutside, Speed);
				FadeAmbient(Cellar, region == 3, MaxCellar, Speed);
			}
		}
		if (Music != null)
		{
			int value = Music.isOn ? 1 : 0;
			if (PlayerPrefs.GetInt("Music", 0) != value) { PlayerPrefs.SetInt("Music", value); PlayerPrefs.Save(); }
			SetObject(MusicG, Music.isOn);
		}
	}
	private void FadeChase(AudioSource source, bool active, float maximum)
	{
		if (source == null) return;
		source.volume = active ? Mathf.Min(maximum, source.volume + SpeedChaseFade * Time.deltaTime) : Mathf.Max(0f, source.volume - SpeedChaseFade * Time.deltaTime);
		if (active && !source.isPlaying) source.Play();
		else if (!active && source.isPlaying && source.volume == 0f) source.Stop();
	}
	private static void FadeAmbient(AudioSource source, bool active, float maximum, float speed)
	{
		if (source == null) return;
		source.volume = active ? Mathf.Min(maximum, source.volume + maximum * speed * Time.deltaTime) : Mathf.Max(0f, source.volume - speed * 0.4f * Time.deltaTime);
	}
	private static void SetObject(GameObject item, bool active) { if (item != null && item.activeSelf != active) item.SetActive(active); }
}
