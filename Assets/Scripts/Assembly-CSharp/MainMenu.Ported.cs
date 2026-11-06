using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// Ported from Annotated-Pseudocode/MainMenu.c (Granny Legacy, IL2CPP x64).
public partial class MainMenu
{
	// Restored from MainMenu.c and <Timerloading>d__193, including the original preference keys.
	private struct MenuToggle
	{
		public Toggle Control;
		public string Key;
		public MenuToggle(Toggle control, string key) { Control = control; Key = key; }
	}
	private struct MenuSlider
	{
		public Slider Control;
		public string Key;
		public MenuSlider(Slider control, string key) { Control = control; Key = key; }
	}
	private MenuToggle[] savedToggles;
	private MenuSlider[] savedSliders;
	private bool preferencesDirty;
	private bool loading;
	private int appliedQuality = -1;

	private void Start()
	{
		if (BlackFade != null) BlackFade.CrossFadeAlpha(0f, 0.4f, false);
		if (MusicMenu != null) MusicMenu.volume = 0.5f;
		if (BGSound != null) BGSound.volume = 0.7f;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		SetObject(Cam, true);
		if (Profile != null)
		{
			Profile.TryGetSettings(out AO);
			Profile.TryGetSettings(out MB);
			Profile.TryGetSettings(out CA);
			Profile.TryGetSettings(out V);
			Profile.TryGetSettings(out DOF);
			Profile.TryGetSettings(out G);
			Profile.TryGetSettings(out B);
		}
		if (V != null) V.intensity.value = 0f;
		if (G != null) G.intensity.value = 0f;
		if (CA != null) CA.intensity.value = 0f;
		if (DOF != null) { DOF.enabled.value = true; DOF.focalLength.value = 1f; }
		if (Bloom != null) Bloom.enabled = false;
		if (string.IsNullOrEmpty(PlayerPrefsTest)) PlayerPrefsTest = "Volume";
		float volume = PlayerPrefs.GetFloat(PlayerPrefsTest, 1f);
		if (Volume != null) { Volume.SetValueWithoutNotify(volume); Volume.onValueChanged.AddListener(SetVolume); }
		SetVolume(volume);
		if (GrannyP != null && PosGrannySpawn != null) GrannyP.transform.position = PosGrannySpawn.position;
		if (PlayerPrefs.GetInt("NewPlayerJoin", 0) == 0)
		{
			PlayerPrefs.SetInt("NewPlayerJoin", 1);
			PlayerPrefs.SetInt("Diff", 1);
			PlayerPrefs.SetInt("FlashL", 0);
			PlayerPrefs.SetInt("GrannyWP", 0);
			PlayerPrefs.SetInt("Granny", 1);
			PlayerPrefs.SetInt("Music", 1);
			PlayerPrefs.SetInt("FOV", 1);
			PlayerPrefs.SetInt("MoreTraps", 0);
			PlayerPrefs.SetInt("Limping", 1);
			PlayerPrefs.SetInt("CURVERSION", 8);
			PlayerPrefs.SetInt("Quality", 3);
			PlayerPrefs.SetInt("OcclusionCullingOP", 1);
			PlayerPrefs.SetInt("RandomSeed", 1);
			PlayerPrefs.SetInt("SlendrinaEnemy", 0);
			PlayerPrefs.SetString("PrimaryInt_KEY", "E");
			PlayerPrefs.SetString("SecondaryInt_KEY", "F");
			PlayerPrefs.SetString("Dropping_KEY", "Space");
			PlayerPrefs.SetString("Crouch_KEY", "C");
			PlayerPrefs.SetString("WP_KEY", "Mouse0");
		}
		PlayerPrefs.SetInt("RoboDesigns", 0);
		PlayerPrefs.SetInt("SPE", 0);
		savedToggles = new[]
		{
			new MenuToggle(ExtraLocks, "EL"), new MenuToggle(Vsync, "V"),
			new MenuToggle(NM, "Textures"), new MenuToggle(Music, "Music"),
			new MenuToggle(Dark, "Dark"), new MenuToggle(Granny, "Granny"),
			new MenuToggle(Spider, "Arachno"), new MenuToggle(Extras, "Extras"),
			new MenuToggle(Trap, "PoisonTrap"), new MenuToggle(ExTraps, "MoreTraps"),
			new MenuToggle(Flash, "FlashL"), new MenuToggle(Smooth, "SmoothM"),
			new MenuToggle(Lights, "LightsHouse"), new MenuToggle(Slendrina, "SlendrinaEnemy"),
			new MenuToggle(Limping, "Limping"), new MenuToggle(FlashLight, "FlashLightIsOn"),
			new MenuToggle(Glitches, "GlitchesAndShit"), new MenuToggle(MotionBlur, "EFFECT_MB"),
			new MenuToggle(ChaseEffect, "EFFECT_CC"), new MenuToggle(AmbientOcclusion, "EFFECT_AMB"),
			new MenuToggle(NightmareFogToggle = CreateNightmareFogToggle(), "NightmareFog"),
		};
		RefreshPurchases();
		if (SP != null) SP.SetIsOnWithoutNotify(false);
		if (Robot != null) Robot.SetIsOnWithoutNotify(false);
		savedSliders = new[]
		{
			new MenuSlider(Difficulty, "Diff"), new MenuSlider(PresetS, "PresetChoosin"),
			new MenuSlider(FOV, "FOV"), new MenuSlider(VersionControl, "CURVERSION"),
			new MenuSlider(Outlines, "Outlines"), new MenuSlider(CustomChase, "Chase"),
			new MenuSlider(Quality, "Quality"), new MenuSlider(Holiday, "Holiday"),
			new MenuSlider(Grandpa, "Grandpa"), new MenuSlider(GrannyWP, "GrannyWP"),
		};
		foreach (var binding in savedToggles)
			if (binding.Control != null) binding.Control.SetIsOnWithoutNotify(PlayerPrefs.GetInt(binding.Key, 0) == 1);
		foreach (var binding in savedSliders)
			if (binding.Control != null) binding.Control.SetValueWithoutNotify(PlayerPrefs.GetInt(binding.Key, 0));
		RestoreAchievementColors();
		ApplyOptions();
		PlayerPrefs.Save();
		preferencesDirty = false;
	}

	private void Update()
	{
		if (savedToggles == null) return;
		RefreshPurchases();
		ApplyOptions();
		if (CoinAmount != null) CoinAmount.text = PlayerPrefs.GetFloat("Coins", 0f).ToString();
		if (Input.GetKeyDown(KeyCode.Escape) && SettingsSet && SecondP != null && SecondP.activeSelf && (MS == null || !MS.IsTypingSeed))
		{
			OnClickSound();
			SetObject(SecondP, false); SetObject(TipMenu, false); SetObject(FirstP, true);
			SetObject(CamHallway, true); SetObject(CamBackyard, false); SetObject(CamBasement, false);
			SetObject(ArachnoTDis, false); SettingsSet = false;
		}
		if (AbleToTest && Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.J) && Input.GetKey((KeyCode)0x122) && Input.GetKey((KeyCode)0x12f))
		{
			PlayerPrefs.SetFloat("Coins", PlayerPrefs.GetFloat("Coins", 0f) + 15f);
			preferencesDirty = true;
		}
		if (preferencesDirty) { PlayerPrefs.Save(); preferencesDirty = false; }
	}

	// Added for the decompile: a "Nightmare Fog" toggle on the Visuals page, copied from the House Lights toggle and
	// placed above the row of toggles. Saved as "NightmareFog"; Days turns on the game's unused red fog.
	private Toggle NightmareFogToggle;
	private Toggle CreateNightmareFogToggle()
	{
		if (Lights == null) return null;
		var copy = UnityEngine.Object.Instantiate(Lights.gameObject, Lights.transform.parent);
		copy.name = "Option_NightmareFog";
		var source = (RectTransform)Lights.transform;
		var rect = (RectTransform)copy.transform;
		// Raise the FOV and Texture quality sliders to the Outlines row, and put Nightmare Fog in the space they
		// leave, centred above the row of toggles.
		float sliderRow = Outlines != null ? ((RectTransform)Outlines.transform).anchoredPosition.y : 242f;
		foreach (var slider in new[] { FOV, Quality })
		{
			if (slider == null) continue;
			var sliderRect = (RectTransform)slider.transform;
			sliderRect.anchoredPosition = new Vector2(sliderRect.anchoredPosition.x, sliderRow);
		}
		rect.anchoredPosition = new Vector2(0f, source.anchoredPosition.y + 142f);
		var label = copy.transform.Find("TextOption");
		if (label != null && label.TryGetComponent(out TMP_Text labelText)) labelText.text = "Nightmare Fog";
		var desc = copy.transform.Find("Desc");
		if (desc != null)
		{
			if (desc.TryGetComponent(out TMP_Text descText)) descText.text = "Fills the whole house with red fog. Needs Nightmare textures.";
			var hover = copy.AddComponent<HoverTextActivato>();
			hover.T = desc.gameObject; hover.SecondP = SecondP;
		}
		var toggle = copy.GetComponent<Toggle>();
		toggle.onValueChanged = new Toggle.ToggleEvent();
		toggle.onValueChanged.AddListener(_ => OnClickSound());
		return toggle;
	}

	private void ApplyOptions()
	{
		int difficulty = SliderInt(Difficulty, 0, 5);
		bool forced = difficulty >= 3;
		foreach (Toggle option in new[] { Granny, ExtraLocks, Dark })
		{
			if (option == null) continue;
			option.interactable = !forced;
			if (forced) option.SetIsOnWithoutNotify(true);
		}
		int version = SliderInt(VersionControl, 0, 8);
		if (Extras != null) { Extras.interactable = version == 8; if (version != 8) Extras.SetIsOnWithoutNotify(false); }
		// Nightmare Fog is greyed out (and off) unless Nightmare textures are on.
		if (NightmareFogToggle != null) { NightmareFogToggle.interactable = IsOn(NM); if (!IsOn(NM)) NightmareFogToggle.SetIsOnWithoutNotify(false); }
		foreach (var binding in savedToggles)
			if (binding.Control != null) SaveInt(binding.Key, binding.Control.isOn ? 1 : 0);
		foreach (var binding in savedSliders)
			if (binding.Control != null) SaveInt(binding.Key, Mathf.RoundToInt(binding.Control.value));
		SetText(Diff_Text, "Difficulty - " + new[] { "Easy", "Normal", "Hard", "Extreme", "Impossible", "Hollow" }[difficulty]);
		GameObject[] boxes = { Box_Diff1, Box_Diff2, Box_Diff3, Box_Diff4, Box_Diff5, Box_Diff6 };
		for (int i = 0; i < boxes.Length; i++) SetObject(boxes[i], i == difficulty);
		SetObject(GrannyP, IsOn(Granny)); SetObject(ExtremeLock, IsOn(ExtraLocks));
		RenderSettings.fog = IsOn(Dark);
		int grandpa = SliderInt(Grandpa, 0, 2);
		SetObject(GrandpaP, grandpa != 0); SetObject(Came, grandpa == 1); SetObject(Shooty, grandpa == 2);
		SetText(Grandpa_Text, new[] { "Grandpa (off)", "Grandpa (on with cane)", "Grandpa (on with shotgun)" }[grandpa]);
		int weapon = SliderInt(GrannyWP, 0, 2);
		SetObject(Bats, weapon == 0); SetObject(Crossbow, weapon == 1); SetObject(Spray, weapon == 2);
		SetText(GrannyWP_Text, "Granny's weapon (" + new[] { "Bat", "Crossbow", "Pepper spray" }[weapon] + ")");
		int preset = SliderInt(PresetS, 0, 6);
		SetText(Preset_Text, "Item Preset Chooser (" + (preset == 0 ? "in order" : preset == 1 ? "random" : (preset - 1).ToString()) + ")");
		int fov = SliderInt(FOV, 0, 7);
		SetText(FOV_Text, fov == 1 ? "Field of view (65f - Default)" : "Field of view (" + (60 + 5 * fov) + "f)");
		SetText(Ver_Text, "Game Version (1." + version + ")");
		int outline = SliderInt(Outlines, 0, 10);
		SetText(Outine_Text, "Outlines (" + new[] { "off", "yellow", "orange", "red", "magenta", "purple", "blue", "cyan", "green", "lime", "black" }[outline] + ")");
		if (OutlineImageUI != null && OutlineExampleCount != null && outline < OutlineExampleCount.Length) OutlineImageUI.sprite = OutlineExampleCount[outline];
		SetText(Ghase_Text, "Chase Music (" + new[] { "default", "1.7.9.3", "Granny 2", "Granny 3" }[SliderInt(CustomChase, 0, 3)] + ")");
		int quality = SliderInt(Quality, 0, 3);
		SetText(Quality_Text, "Texture quality (" + new[] { "very low", "low", "medium", "high" }[quality] + ")");
		if (appliedQuality != quality) { QualitySettings.SetQualityLevel(quality); appliedQuality = quality; }
		QualitySettings.vSyncCount = IsOn(Vsync) ? 1 : 0;
		int holiday = SliderInt(Holiday, 0, 7);
		SetText(Holiday_Text, "H.D : " + new[] { "Off", "Halloween", "Christmas", "Valentines", "St. Patrick", "Easter", "Pride", "Summer" }[holiday]);
		SetObject(HatGranny, holiday != 0); SetObject(HatGranpa, holiday != 0); SetObject(HatSlendrina, holiday != 0);
		SetObject(SeedButton, IsOn(Flash)); SetObject(SliderPreset, !IsOn(Flash));
		SetObject(SlendrinaMenu, IsOn(Slendrina)); SetObject(FlashLightObject, IsOn(FlashLight));
		SetObject(LightBulbs, IsOn(Lights)); SetObject(MusicG, IsOn(Music));
		SetObject(B1, !IsOn(NM)); SetObject(B2, IsOn(NM));
		RenderSettings.skybox = IsOn(NM) ? NMSky : NormalSky;
		if (BackyardBG != null)
		{
			var clip = IsOn(NM) ? BNM : BNormal;
			if (BackyardBG.clip != clip) BackyardBG.clip = clip;
		}
		if (CA != null) CA.enabled.value = IsOn(ChaseEffect);
		if (AO != null) AO.enabled.value = IsOn(AmbientOcclusion);
		if (MB != null) MB.enabled.value = IsOn(MotionBlur);
	}

	private void RefreshPurchases()
	{
		bool impossible = PlayerPrefs.GetInt("IM_BOUGHT", 0) == 1;
		bool hollow = impossible && PlayerPrefs.GetInt("HO_BOUGHT", 0) == 1;
		if (Difficulty != null) Difficulty.maxValue = hollow ? 5 : impossible ? 4 : 3;
		SetObject(PURCHASEDIM, impossible); SetObject(PURCHASEDHOL, hollow); SetObject(HolCoverButton, !impossible);
		bool shotgun = PlayerPrefs.GetInt("SH_BOUGHT", 0) == 1;
		if (Grandpa != null) Grandpa.maxValue = shotgun ? 2 : 1;
		SetObject(PURCHASEDSH, shotgun);
		ShowPurchase("NM_BOUGHT", NMBought, PURCHASEDNM);
		ShowPurchase("GRANDPA_BOUGHT", GrandpaBought, PURCHASEDGR);
		ShowPurchase("EXTRAS_BOUGHT", ExtrasBought, PURCHASEDEX);
		ShowPurchase("CC_BOUGHT", CustomCBought, PURCHASEDCC);
		ShowPurchase("CH_BOUGHT", CHBought, PURCHASEDCH);
		ShowPurchase("TRAP_BOUGHT", TrapBought, PURCHASEDTRAP);
		ShowPurchase("EXTRAPS_BOUGHT", ExtrapsBought, PURCHASEDEXTRAPS);
		ShowPurchase("FLASH_BOUGHT", FlashBought, PURCHASEDFLASH);
		ShowPurchase("VERC_BOUGHT", VerCBought, PURCHASEDVER);
		SetObject(ShootyCoverButton, PlayerPrefs.GetInt("GRANDPA_BOUGHT", 0) != 1);
		SetObject(textNoticeVERControl, PlayerPrefs.GetInt("VERC_BOUGHT", 0) == 1);
		if (Slendrina != null && PlayerPrefs.GetInt("SlendrinaUnlock", 0) == 1) SetObject(Slendrina.gameObject, true);
	}
	private static void ShowPurchase(string key, GameObject option, GameObject purchased)
	{
		bool bought = PlayerPrefs.GetInt(key, 0) == 1;
		SetObject(option, bought); SetObject(purchased, bought);
	}
	private void RestoreAchievementColors()
	{
		Achievement(EX_Door, "EX_Door"); Achievement(EX_Car, "EX_Car"); Achievement(EX_Pipe, "EX_Pipe");
		Achievement(EX_Robo, "EX_Robo"); Achievement(EX_Both, "EX_Both"); Achievement(IM_Both, "IM_Both");
		Achievement(NoSee, "Unseen"); Achievement(NoWP, "NoWP"); Achievement(NoDie, "NoDie");
		Achievement(HARD_Both, "HARD_Both"); Achievement(HOL_Esc, "HOL_Esc");
		Achievement(EASY_Escape, "EASY_Escape"); Achievement(EASY_Car, "EASY_Car");
		Achievement(NOR_Escape, "NOR_Escape"); Achievement(NOR_Grandpa, "NOR_Grandpa"); Achievement(NOR_Both, "NOR_Both");
		Achievement(TeddyCrib, "SlendrinaUnlock"); Achievement(HARD_Pipe, "HARD_Pipe"); Achievement(HARD_Car, "HARD_Car");
		Achievement(NOR_Robo, "NOR_Robo"); Achievement(HARD_BothRobo, "HARD_BothRobo"); Achievement(HARD_NoDyingBoth, "HARD_NoDyingBoth");
		Achievement(EX_RoboBoth, "EX_BothRobo"); Achievement(EX_AllRouteNo, "EX_AllRouteNo"); Achievement(EX_AllRouteYes, "EX_AllRouteYes");
		Achievement(IM_Door, "IM_Door"); Achievement(IM_Car, "IM_Car"); Achievement(IM_Pipe, "IM_Pipe");
		Achievement(IM_Robo, "IM_Robo"); Achievement(IM_NoSee, "IM_NoSee"); Achievement(IM_NoWP, "IM_NoWP");
		Achievement(HOL_Robo, "HOL_Robo"); Achievement(HOL_Both, "HOL_Both"); Achievement(HOL_AllRoutesExtrasBoth, "HOL_AllRoutesExtrasBoth");
		if (PlayerPrefs.GetInt("HOL_AllRoutesExtrasBoth", 0) == 1) SetObject(Trophy, true);
	}
	private static void Achievement(Image image, string key)
	{
		if (image != null && PlayerPrefs.GetInt(key, 0) == 1) image.color = Color.green;
	}
	private void SaveInt(string key, int value)
	{
		if (PlayerPrefs.GetInt(key, 0) == value) return;
		PlayerPrefs.SetInt(key, value); preferencesDirty = true;
	}
	private static bool IsOn(Toggle toggle) { return toggle != null && toggle.isOn; }
	private static int SliderInt(Slider slider, int min, int max) { return slider == null ? min : Mathf.Clamp(Mathf.RoundToInt(slider.value), min, max); }
	private static void SetObject(GameObject item, bool active) { if (item != null && item.activeSelf != active) item.SetActive(active); }
	private static void SetText(TMP_Text text, string value) { if (text != null && text.text != value) text.text = value; }
	private void SetVolume(float volume) { AudioListener.volume = volume; PlayerPrefs.SetFloat(PlayerPrefsTest, volume); }
	private void Buy(string key, float price)
	{
		float coins = PlayerPrefs.GetFloat("Coins", 0f);
		if (coins >= price)
		{
			PlayerPrefs.SetFloat("Coins", coins - price); PlayerPrefs.SetInt(key, 1);
			if (BuySound != null) BuySound.Play(); RefreshPurchases(); PlayerPrefs.Save();
		}
		else if (TextNotEnough != null)
		{
			var text = TextNotEnough.GetComponent<TextManager>();
			if (text != null) text.Add(); SetObject(TextNotEnough, true);
		}
	}
	public void AttemptBuy_NM() { Buy("NM_BOUGHT", 3f); }
	public void AttemptBuy_GR() { Buy("GRANDPA_BOUGHT", 5f); }
	public void AttemptBuy_SH() { Buy("SH_BOUGHT", 5f); }
	public void AttemptBuy_IM() { Buy("IM_BOUGHT", 10f); }
	public void AttemptBuy_EX() { Buy("EXTRAS_BOUGHT", 5f); }
	public void AttemptBuy_CC() { Buy("CC_BOUGHT", 4f); }
	public void AttemptBuy_CH() { Buy("CH_BOUGHT", 3f); }
	public void AttemptBuy_TRAP() { Buy("TRAP_BOUGHT", 3f); }
	public void AttemptBuy_EXTRAPS() { Buy("EXTRAPS_BOUGHT", 7f); }
	public void AttemptBuy_ROBO() { Buy("ROBO_BOUGHT", 6f); }
	public void AttemptBuy_FLASH() { Buy("FLASH_BOUGHT", 15f); }
	public void AttemptBuy_VERC() { Buy("VERC_BOUGHT", 8f); }
	public void AttemptBuy_HO() { Buy("HO_BOUGHT", 20f); }
	public void OnClickSound() { if (Sounds != null && SwitchToggle != null) Sounds.PlayOneShot(SwitchToggle); }
	public void OnClickPlay() { SettingsSet = true; }
	public void PlayLoad()
	{
		if (loading) return;
		loading = true; SettingsSet = false; Cursor.visible = false;
		ApplyOptions(); PlayerPrefs.Save(); StartCoroutine(Timerloading());
	}
	private IEnumerator Timerloading() { yield return new WaitForSeconds(3.5f); SceneManager.LoadScene("Scene"); }
	// Original: the Quit button calls this, and it calls Application.Quit (the port had dropped the quit).
	public void OnApplicationQuit()
	{
		PlayerPrefs.Save();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
	public void LoadGame()
	{
		if (GINPUT != null && (!GINPUT.gameObject.activeSelf || !GINPUT.enabled)) Application.Quit();
	}
	public void UnlockTrophy(int t) { GameJolt.API.Trophies.Unlock(t); }
}
