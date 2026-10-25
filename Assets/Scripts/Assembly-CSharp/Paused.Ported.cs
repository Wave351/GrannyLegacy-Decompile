using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Paused.c (Granny Legacy, IL2CPP x64).
public partial class Paused
{
	private void Start()
	{
		IsPaused = false;
		if (string.IsNullOrEmpty(PlayerPrefsTest)) PlayerPrefsTest = "Volume";
		float volume = PlayerPrefs.GetFloat(PlayerPrefsTest, 1f);
		if (Volume != null) { Volume.SetValueWithoutNotify(volume); Volume.onValueChanged.AddListener(SetVolume); }
		SetVolume(volume); SetPanel(pauseMenu, false);
		if (SensSlider != null) SensSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("sensitivity", 2f));
	}
	private void SetVolume(float volume) { AudioListener.volume = volume; PlayerPrefs.SetFloat(PlayerPrefsTest, volume); }
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (IsPaused) { if (!Visible(KeyBindMenu) && !Visible(EscMenu) && !Visible(ResMenu)) ResumeGame(); }
			else if (!Visible(EscMenu) && !Visible(ResMenu)) PauseGame();
		}
		if (AudiosAffected != null) foreach (var audio in AudiosAffected) if (audio != null) { if (IsPaused) audio.Pause(); else audio.UnPause(); }
		if (Input.GetKeyDown(KeyCode.V) && IsPaused && !Visible(KeyBindMenu) && !Visible(EscMenu) && !Visible(ResMenu)) SetPanel(MenuData, !Visible(MenuData));
		if (Visible(MenuData)) RefreshRunData();
	}
	private void RefreshRunData()
	{
		SetLabel(Data_Diff, "Difficulty : " + new[] { "Easy", "Normal", "Hard", "Extreme", "Impossible", "Hollow" }[Mathf.Clamp(PlayerPrefs.GetInt("Diff", 1), 0, 5)]);
		SetLabel(Data_Granny, PlayerPrefs.GetInt("Granny", 1) == 0 ? "Granny : Off" : "Granny : " + new[] { "On", "Crossbow", "Spray" }[Mathf.Clamp(PlayerPrefs.GetInt("GrannyWP", 0), 0, 2)]);
		SetLabel(Data_Grandpa, new[] { "Grandpa : Off", "Grandpa : On (Cane)", "Grandpa : On (Shotgun)" }[Mathf.Clamp(PlayerPrefs.GetInt("Grandpa", 0), 0, 2)]);
		SetLabel(Data_Extras, "Extras : " + PrefOn("Extras")); SetLabel(Data_ExtraTraps, "Extra Traps : " + PrefOn("MoreTraps"));
		SetLabel(Data_S, "Slendrina : " + PrefOn("SlendrinaEnemy")); SetLabel(Data_G, "Glitches : " + PrefOn("GlitchesAndShit"));
		if (PlayerPrefs.GetInt("FlashL", 0) == 1)
		{
			GameObject[] versionSeeds = { SeedSys10, SeedSys11, SeedSys12, SeedSys13, SeedSys14, SeedSys15, SeedSys16, SeedSys17, SeedSysMain };
			int version = Mathf.Clamp(PlayerPrefs.GetInt("CURVERSION", 8), 0, 8);
			GameObject system = version == 8 && PlayerPrefs.GetInt("Extras", 0) == 1 ? SeedSysExtra : versionSeeds[version];
			var seed = system != null ? system.GetComponent<SeedManager>() : null;
			if (seed != null) SetLabel(Data_Preset, "Seed : " + seed.Seed);
		}
		else if (OM != null)
			SetLabel(Data_Preset, "Preset : " + new[] { "One", "Two", "Three", "Four", "Five" }[Mathf.Clamp(Mathf.RoundToInt(OM.PresetChoosenCurrent), 0, 4)]);
	}
	public void PauseGame()
	{
		SetPanel(pauseMenu, true); Cursor.visible = true; Cursor.lockState = CursorLockMode.Confined;
		Time.timeScale = 0f; IsPaused = true; if (ClickSound != null) ClickSound.volume = 1f;
	}
	public void ResumeGame()
	{
		SetPanel(pauseMenu, false); Cursor.visible = false; Time.timeScale = 1f; IsPaused = false;
	}
	public void GoToMainMenu()
	{
		Time.timeScale = 1f; Cursor.visible = true; Cursor.lockState = CursorLockMode.Confined; IsPaused = false;
		PlayerPrefs.Save(); SceneManager.LoadScene("Menu");
	}
	public void RestartP() { Time.timeScale = 1f; Cursor.visible = false; IsPaused = false; PlayerPrefs.Save(); SceneManager.LoadScene("Scene"); }
	public void SensitivitySlider() { if (SensSlider != null) { PlayerPrefs.SetFloat("sensitivity", SensSlider.value); PlayerPrefs.Save(); } }
	private static bool Visible(GameObject item) { return item != null && item.activeSelf; }
	private static void SetPanel(GameObject item, bool active) { if (item != null && item.activeSelf != active) item.SetActive(active); }
	private static string PrefOn(string key) { return PlayerPrefs.GetInt(key, 0) == 0 ? "Off" : "On"; }
	private static void SetLabel(TMP_Text label, string value) { if (label != null && label.text != value) label.text = value; }
}
