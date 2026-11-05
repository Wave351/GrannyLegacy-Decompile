using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Days.c (Granny Legacy, IL2CPP x64).
public partial class Days
{
	private void Start()
	{
		ConfigureRun();
		StartCoroutine(StartDays());
	}
	private void ConfigureRun()
	{
		int difficulty = PlayerPrefs.GetInt("Diff", 1);
		int version = PlayerPrefs.GetInt("CURVERSION", 8);
		bool extraLocks = PlayerPrefs.GetInt("EL", 0) == 1 && version >= 3;
		if (PlayerPrefs.GetInt("Arachno", 0) == 1)
		{
			SetActive(SpiderWebs, false); SetActive(SpiderWall, false); SetActive(Webs, false);
			if (SpiderAtticSMR != null) SpiderAtticSMR.enabled = false;
			if (SpiderMomSMR != null) SpiderMomSMR.enabled = false;
			if (SpidersHallo != null) foreach (var spider in SpidersHallo) SetActive(spider, false);
			SetActive(SpiderMomSpray, true); SetActive(SpiderAtticSpray, true);
		}
		if (extraLocks)
		{
			SetActive(ExtremeLock, true); SetActive(HardLock, true); SetActive(NormalLock, true);
			SetActive(Codes, true); SetActive(Batteries, true);
		}
		else if (version <= 2)
		{
			SetActive(Codes, true); SetActive(ExtremeLock, false); SetActive(HardLock, false);
			if (difficulty >= 3) SetActive(GridExtreme, false);
			if (difficulty >= 4 && GridImpossible != null) foreach (var grid in GridImpossible) SetActive(grid, false);
		}
		else
		{
			if (difficulty == 0) { SetActive(NormalLock, false); SetActive(ExtremeLock, false); SetActive(HardLock, false); }
			if (difficulty == 1) { SetActive(Codes, true); SetActive(ExtremeLock, false); SetActive(HardLock, false); }
			if (difficulty == 2) { SetActive(HardLock, true); SetActive(Codes, true); SetActive(Batteries, true); SetActive(ExtremeLock, false); }
			if (difficulty >= 3)
			{
				SetActive(ExtremeLock, true); SetActive(HardLock, true); SetActive(Codes, true); SetActive(Batteries, true);
				if (difficulty < 5) SetActive(NoiseTRN, false);
			}
		}
		if (difficulty == 0) SetActive(NormalFeature, false);
		if (difficulty == 2) SetActive(EasyFeature, false);             // Normal+ (incl. its creaking floors) stays on Hard, with or without extra locks
		SetActive(ExtremeFeature, difficulty >= 3); SetActive(ImpossibleFeature, difficulty >= 4);
		if (difficulty >= 3) { if (extraLocks || version > 2) SetActive(EasyFeature, false); SetActive(NormalFeature, false); }
		if (difficulty == 5)
		{
			SetActive(HollowFeature, true);
			foreach (var item in new[] { PepperSpray, ClosetDoor1, ClosetDoor2, BackyardBoxDoor, CoffinBackyard, Bed1HideTrigger, Bed2HideTrigger, Bed3HideTrigger, AmmoSetSh2, Dart1, Dart2 }) SetActive(item, false);
			if (BathroomFallHatch != null)
			{
				BathroomFallHatch.enabled = false;
				var noise = BathroomFallHatch.GetComponentInChildren<SoundTrigger>();
				if (noise != null) noise.enabled = false;
			}
		}
		if (!extraLocks && version != 8) SetActive(NumberPads, false);
		bool dark = difficulty >= 3 || PlayerPrefs.GetInt("Dark", 0) == 1;
		RenderSettings.fog = dark;
		if (dark) RenderSettings.fogEndDistance = difficulty == 3 ? 6f : difficulty >= 4 ? 7.4f : 13.5f;
		bool nightmare = PlayerPrefs.GetInt("Textures", 0) == 1;
		int chase = PlayerPrefs.GetInt("Chase", 0);
		if (HuntSource != null)
		{
			HuntSource.clip = nightmare ? (chase == 2 ? HuntG2NM : chase == 3 ? HuntG3NM : HuntDefaultNM) :
				(chase == 1 ? HuntClassic : chase == 2 ? HuntG2 : chase == 3 ? HuntG3 : HuntDefault);
			if (!nightmare && chase == 1) HuntSource.pitch = 1.1f;
		}
		RenderSettings.skybox = dark ? null : nightmare ? NM : Normal;
		if (nightmare)
		{
			if (IGSource != null) IGSource.clip = BGNM;
			if (PepperEffect != null) { var main = PepperEffect.main; main.startColor = Color.red; }
			if (PepperEffectGranny != null) { var main = PepperEffectGranny.main; main.startColor = Color.red; }
			if (SeeEnemy != null) SeeEnemy.clip = SeeNM;
			if (OutSource != null) { OutSource.clip = OutNM; SetActive(OutSource.gameObject, false); }
			SetActive(NMStuff, true); SetActive(OldWater, false);
		}
		if (PlayerPrefs.GetInt("LightsHouse", 0) == 1) { SetActive(LightBulbs, true); AddBulbSpotLights(); }
	}
	private void Update()
	{
		ValueFade = Mathf.Clamp01(ValueFade + (BeganDay ? 2f : -2f) * Time.deltaTime);
		foreach (var text in DayImages()) if (text != null) text.color = new Color(1f, 1f, 1f, ValueFade);
		if (PlayerBedAnim != null && PlayerBedAnim.transform.parent != null && PlayerBedAnim.transform.parent.gameObject.activeSelf && !Paused.IsPaused && Input.GetKeyDown(KeyCode.B)) Time.timeScale = 15f;
		if (NormalLock != null && !NormalLock.activeSelf) SetActive(CodeMain, false);
		if (HardLock != null && !HardLock.activeSelf) SetActive(BatteryMain, false);
	}
	public void StartNew()
	{
		if (CurrentDay == (ExtraDay ? 6f : 5f)) { if (GOvers != null) GOvers.GameOverCall(); }
		else { CurrentDay += 1f; StartCoroutine(StartDays()); }
		var falling = Player != null ? Player.GetComponent<FallingHolder>() : null;
		if (falling != null) { falling.FallDurationHold = 1f; falling.DeathFall = false; }
		if (EC != null && EC.PutDownMom) EC.CrawlFaster();
	}
	public IEnumerator StartDays()
	{
		yield return new WaitForSeconds(2f);
		Cursor.visible = false;
		if (CurrentDay == 0f) CurrentDay = 1f;
		int day = Mathf.RoundToInt(CurrentDay);
		if (day < 1 || day > 6) yield break;
		if (day > 1) ResetForNewDay(day);
		yield return new WaitForSeconds(3f);
		Image dayText = day == 5 ? (ExtraDay ? Text_Day5 : Text_Day5Final) : DayImages()[day == 6 ? 6 : day - 1];
		if (AudioHold != null && DayTextPopup != null) AudioHold.PlayOneShot(DayTextPopup);
		if (dayText != null) SetActive(dayText.gameObject, true);
		BeganDay = true;
		yield return new WaitForSeconds(2f);
		BeganDay = false;
		if (day == 1) SetActive(NMStuff2, false);
		SetActive(BedNoise, true);
		if (IGSource != null) SetActive(IGSource.gameObject, true);
		if (PlayerBedAnim != null) PlayerBedAnim.CrossFade(day == 1 || day == 6 ? "PlayerInBed" : day <= 3 ? "PlayerInBed2" : "PlayerInBed3");
		if (BlackScreen != null) BlackScreen.CrossFadeAlpha(0f, 2.5f, false);
		yield return new WaitForSeconds(10.5f);
		if (OutSource != null) SetActive(OutSource.gameObject, true);
		if (dayText != null) SetActive(dayText.gameObject, false);
		if (PlayerBedAnim != null && PlayerBedAnim.transform.parent != null) SetActive(PlayerBedAnim.transform.parent.gameObject, false);
		SetActive(BedNoise, false); SetActive(Player, true); SetActive(UIElements, true); SetActive(PauseManager, true);
		if (day > 1) SetActive(Texts, true);
		if (day == 2 && RayPick != null && RayPick.T_NeedShit != null)
		{
			var hint = RayPick.T_NeedShit; SetActive(hint, true);
			var timer = hint.GetComponent<TextManager>(); if (timer != null) timer.Add();
			var label = hint.GetComponent<TMP_Text>(); if (label != null) label.text = "I have to leave this house";
		}
		DisableCaught();
		SpawnEnemies();
		Time.timeScale = 1f;
		if (EC != null) EC.SlendrinaSpawn();
	}
	private Image[] DayImages() { return new[] { Text_Day1, Text_Day2, Text_Day3, Text_Day4, Text_Day5, Text_Day5Final, Text_Day6 }; }
	private void ResetForNewDay(int day)
	{
		if (ObstacleSauna != null) ObstacleSauna.carving = false;
		SetActive(BCam1, false); SetActive(BCam2, false); SetActive(BCam3, false); SetActive(UIElements, false);
		if (day >= 4) SetActive(Day4Blood, true);
		if (day == 4) SetActive(BedBlood, true);
		if (EC != null)
		{
			if (EC.SpawnCheck != null) { EC.SpawnCheck.Floor1 = false; EC.SpawnCheck.Floor2 = true; EC.SpawnCheck.CellarFloor = false; EC.SpawnCheck.OldHouseFloor = false; }
			EC.GrannyKilled = false;
			if (EC.SDeath != null) { EC.SDeath.GrannyPresence = false; EC.SDeath.GrandpaPresence = false; }
			if (EC.GrannyGoneText != null) SetActive(EC.GrannyGoneText.gameObject, false);
			if (EC.GrandpaGoneText != null) SetActive(EC.GrandpaGoneText.gameObject, false);
		}
		KillRagdolls(); DoorsClose();
		if (Furni != null) Furni.CleanUp();
		NoiseObjectsKilled();
		if (RemoveRay != null) RemoveRay.Trapped = false;
		CamFOVReset();
		if (ScreenEffects != null) { ScreenEffects.Hurt = false; ScreenEffects.Blood = 0f; if (ScreenEffects.BloodScreenHit != null) SetActive(ScreenEffects.BloodScreenHit.gameObject, false); }
		if (EC != null) EC.EnemyRearrange();
		if (Exit_SC != null) Exit_SC.StopNewDay();
		if (BGManager != null)
		{
			BGManager.InHouse = true; BGManager.InOutside = false; BGManager.InAttic = false; BGManager.InCellar = false;
			if (BGManager.HouseMusic != null) SetActive(BGManager.HouseMusic.gameObject, true);
		}
		if (GroundC != null) GroundC.transform.localPosition = new Vector3(0f, -0.424f, 0f);
		if (ShakeCam != null) ShakeCam.transform.localPosition = new Vector3(0f, 1.794f, 0f);
		if (Player != null)
		{
			var controller = Player.GetComponent<CharacterController>();
			bool controllerEnabled = controller != null && controller.enabled;
			if (controller != null) controller.enabled = false;
			if (PositionPlayerBack != null) Player.transform.SetPositionAndRotation(PositionPlayerBack.position, PositionPlayerBack.rotation);
			if (controller != null) { controller.center = new Vector3(0f, 0.63f, 0f); controller.height = 2.45f; controller.enabled = controllerEnabled; }
			var crouch = Player.GetComponent<CrouchHolder>();
			if (crouch != null) { crouch.IsCrouched = false; crouch.isCrouching = false; crouch.Starter = true; crouch.Disabled = false; }
			var fps = Player.GetComponent<MobileFPS>();
			if (fps != null) { fps.InWeb = false; fps.InElev = false; fps.ResetRotation(); fps.isAllowedToMove = true; fps.IsCrouched = false; fps.AbleToMove = true; fps.CamK = true; }
		}
		if (PlayerBedAnim != null && PlayerBedAnim.transform.parent != null) SetActive(PlayerBedAnim.transform.parent.gameObject, true);
	}
	private void SpawnEnemies()
	{
		if (EC == null) return;
		bool granny = PlayerPrefs.GetInt("Granny", 1) == 1 || PlayerPrefs.GetInt("Diff", 1) >= 3;
		if (granny)
		{
			PlayerPrefs.SetInt("Granny", 1); EC.SpawnGranny();
			var ai = EC.Granny != null ? EC.Granny.GetComponent<AI_Granny>() : null;
			if (ai != null) { if (PlayerPrefs.GetInt("GrannyWP", 0) == 1) SetActive(ai.Crossbow, true); if (PlayerPrefs.GetInt("GrannyWP", 0) == 2) SetActive(ai.Spray, true); }
		}
		int grandpa = PlayerPrefs.GetInt("Grandpa", 0);
		if (grandpa == 1 || grandpa == 2) { if (grandpa == 2) SetActive(EC.ShotgunGrandpa, true); EC.SpawnGrandpa(); }
	}
	public void DoorsClose()
	{
		foreach (var door in new[] { Drawer1, Drawer2, Drawer3, Drawer4, Drawer5, Drawer6, Drawer7, Drawer8, Closet1, Closet2, Closet3, Door1, Door2, Door3, Door4, Door5, Door6, Door7, Door8, Door9, Door10, Door11, Door12, Fridge, Cabinet1, Cabinet2, Cabinet3, Cabinet4, Cabinet5, Cabinet6, Dulap1, Dulap2, Dulap3, Dulap4, Locker, Playhouse, OldCabinet }) if (door != null) door.AttemptClose();
	}
	public void NoiseObjectsKilled()
	{
		foreach (string tag in new[] { "noiseobject", "noiseobiect", "BearTrapActivated" })
			foreach (var item in GameObject.FindGameObjectsWithTag(tag)) UnityEngine.Object.Destroy(item);
	}
	public void KillRagdolls()
	{
		if (GameObject.Find("GrannyFreezeDoll(Clone)") != null)
		{
			var holder = GameObject.Find("GrannyFreeze"); var freeze = holder != null ? holder.GetComponent<Freeze_Granny>() : null;
			if (freeze != null) freeze.DestructionRagdolls();
		}
		if (GameObject.Find("GrandpaFreezeDoll(Clone)") != null)
		{
			var holder = GameObject.Find("TriggerBreakDoll"); var freeze = holder != null ? holder.GetComponent<Freeze_Grandpa>() : null;
			if (freeze != null) freeze.DestructionRagdolls();
		}
		foreach (var item in GameObject.FindGameObjectsWithTag("Shotgun2")) UnityEngine.Object.Destroy(item);
		foreach (string name in new[] { "GrannyRagdoll(Clone)", "GranpaRagdoll(Clone)" }) { var item = GameObject.Find(name); if (item != null) UnityEngine.Object.Destroy(item); }
		if (PlayerPrefs.GetInt("Granny", 1) == 1 || PlayerPrefs.GetInt("Diff", 1) >= 3)
		{
			int weapon = PlayerPrefs.GetInt("GrannyWP", 0);
			var item = weapon == 1 ? GameObject.Find("Crossbow") : weapon == 2 ? GameObject.Find("PepperSpray") : null;
			if (item != null) UnityEngine.Object.Destroy(item);
		}
	}
	public void DisableCaught()
	{
		if (EC == null) return;
		var granny = EC.Granny != null ? EC.Granny.GetComponent<AI_Granny>() : null;
		var grandpa = EC.Grandpa != null ? EC.Grandpa.GetComponent<AI_Grandpa>() : null;
		if (granny != null) granny.CaughtPlayer = false;
		if (grandpa != null) grandpa.CaughtPlayer = false;
	}
	private void CamFOVReset()
	{
		var camera = CamN != null ? CamN.GetComponent<Camera>() : null;
		if (camera != null) camera.fieldOfView = 60f + 5f * Mathf.Clamp(PlayerPrefs.GetInt("FOV", 1), 0, 7);
	}
	private static void SetActive(GameObject item, bool active) { if (item != null && item.activeSelf != active) item.SetActive(active); }

	// Added for the decompile: the optional light bulbs are only meshes, so the "Lights" option lit nothing. Give each
	// bulb a downward spot light (per-pixel, no shadows; only objects a bulb reaches pay for it).
	private void AddBulbSpotLights()
	{
		if (LightBulbs == null) return;
		foreach (var bulb in LightBulbs.GetComponentsInChildren<Renderer>(true))
		{
			if (bulb.transform.Find("BulbSpotLight") != null) continue;
			var go = new GameObject("BulbSpotLight");
			go.transform.SetParent(bulb.transform, false);
			go.transform.position = bulb.bounds.center + Vector3.down * 0.05f;
			go.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
			var light = go.AddComponent<Light>();
			light.type = LightType.Spot;
			light.spotAngle = 120f;
			light.innerSpotAngle = 60f;
			light.range = 7f;
			light.intensity = 1.3f;
			light.color = new Color(1f, 0.92f, 0.78f);
			light.shadows = LightShadows.None;
			light.renderMode = LightRenderMode.ForcePixel;
		}
	}
}
