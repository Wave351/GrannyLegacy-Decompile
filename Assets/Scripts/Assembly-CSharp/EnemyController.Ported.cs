using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/EnemyController.c (Granny Legacy, IL2CPP x64).
public partial class EnemyController
{
	public EnemyController() { MaxTimerRecoverGranny = 60f; MaxTimerRecoverGrandpa = 60f; MaxBedSleep = 30f; }
	private void Start()
	{
		if (PlayerPrefs.GetInt("Granny", 1) == 0 && PlayerPrefs.GetInt("Diff", 1) < 3) Active(GrannyStuff, false);
		if (PlayerPrefs.GetInt("Grandpa", 0) == 0) Active(GrandpaStuff, false);
		Active(Grandpa, false); Active(Granny, false); Active(MomCrawl, false); Active(MomSpider, false);
		if (MomSpider != null && MomSpiderPosSend != null) MomSpider.transform.position = MomSpiderPosSend.position;
		if (MomBody != null && PositionSpawnMom != null) MomBody.transform.localPosition = PositionSpawnMom.localPosition;
		if (PlayerPrefs.GetInt("Grandpa", 0) == 1) Active(CanePBed, true);
	}
	private void Update()
	{
		if (GrannyKilled) { TimerGrannyRecover += Time.deltaTime; if (TimerGrannyRecover >= MaxTimerRecoverGranny) { TimerGrannyRecover = 0f; GrannyKilled = false; SpawnGranny(); } }
		else TimerGrannyRecover = 0f;
		if (GrandpaKilled) { TimerGrandpaRecover += Time.deltaTime; if (TimerGrandpaRecover >= MaxTimerRecoverGrandpa) { TimerGrandpaRecover = 0f; GrandpaKilled = false; SpawnGrandpa(); } }
		else TimerGrandpaRecover = 0f;
		if (GrandpaBed != null && GrandpaBed.activeSelf)
		{
			TimerInBed += Time.deltaTime;
			if (TimerInBed >= MaxBedSleep)
			{
				Active(GrandpaBed, false); var ai = ComponentOn<AI_Grandpa>(Grandpa);
				if (ai != null) ai.IsBlind = false;
				MurderNoise2(); Active(Grandpa, true); if (ai != null) ai.ChooseWaypoint();
			}
		}
		else TimerInBed = 0f;
		Active(ShotgunGrandpaBed, ShotgunGrandpa != null && ShotgunGrandpa.activeSelf);
		var granny = ComponentOn<AI_Granny>(Granny);
		if (granny != null)
		{
			if (CrossbowHand != null && CrossbowHand.activeSelf) Active(granny.Crossbow, false);
			if (PepperHand != null && PepperHand.activeSelf) Active(granny.Spray, false);
		}
	}
	public void GrannyFrozeDeath() { DefeatGranny(true, false, false); }
	public void GrannyDeath(bool Poisoned) { DefeatGranny(false, false, Poisoned); }
	public void GrannyDeathS() { DefeatGranny(false, true, false); }
	public void GrandpaFrozeDeath() { DefeatGrandpa(true, false, false, false); }
	public void GrandpaDeath(bool Poisoned) { DefeatGrandpa(false, false, Poisoned, false); }
	public void GrandpaDeathS() { DefeatGrandpa(false, true, false, false); }
	public void GrandpaDeathSBed() { DefeatGrandpa(false, true, false, true); }
	private void DefeatGranny(bool frozen, bool shotgun, bool poisoned)
	{
		NavOBSDisable(); SpawnAt(N1, RagdollPSGranny);
		var body = SpawnAt(frozen ? GrannyFreezeDoll : RagdollGranny, RagdollPSGranny);
		GrannyKilled = true;
		if (TH != null) TH.AlwaysAngry1 = false;
		if (shotgun && Granny != null) foreach (var dart in Granny.GetComponentsInChildren<Dart>(true)) dart.InitiateDrop();
		var ai = ComponentOn<AI_Granny>(Granny);
		if (ai != null) { ai.DeadDisable(); if (!frozen) { ai.AtDishes = false; Active(ai.DishesDone, false); } }
		Active(Granny, false);
		int difficulty = Difficulty();
		MaxTimerRecoverGranny = Recovery(difficulty, shotgun);
		float bodyDuration = BodyDuration(difficulty, shotgun, false);
		if (frozen)
		{
			var freeze = body != null ? body.GetComponentInChildren<Freeze_Granny>() : null;
			if (freeze != null) freeze.TimerGone = bodyDuration;
		}
		else
		{
			var ragdoll = ComponentOn<R_Granny>(body);
			if (ragdoll != null) { ragdoll.TimerGone = bodyDuration; if (poisoned) ragdoll.Poisoned = true; }
		}
		ShowRecovery(GrannyGoneText, "Granny", MaxTimerRecoverGranny);
		if (ES != null) ES.UsedWPOnce = true;
	}
	private void DefeatGrandpa(bool frozen, bool shotgun, bool poisoned, bool inBed)
	{
		NavOBSDisable(); SpawnAt(N2, RagdollPSGrandpa);
		var body = SpawnAt(frozen ? GrandpaFreezeDoll : RagdollGrandpa, inBed ? RagdollPSGrandpaBed : RagdollPSGrandpa);
		GrandpaKilled = true;
		if (TH != null) TH.AlwaysAngry2 = false;
		if (shotgun && !inBed && Grandpa != null) foreach (var dart in Grandpa.GetComponentsInChildren<Dart>(true)) dart.InitiateDrop();
		if (inBed) Active(GrandpaBed, false);
		var ai = ComponentOn<AI_Grandpa>(Grandpa); if (ai != null) ai.DeadDisable();
		Active(Grandpa, false);
		int difficulty = Difficulty();
		MaxTimerRecoverGrandpa = Recovery(difficulty, shotgun);
		float bodyDuration = BodyDuration(difficulty, shotgun, inBed);
		if (frozen)
		{
			var freeze = body != null ? body.GetComponentInChildren<Freeze_Grandpa>() : null;
			if (freeze != null) freeze.TimerGone = bodyDuration;
		}
		else
		{
			var ragdoll = ComponentOn<R_Grandpa>(body);
			if (ragdoll != null) { ragdoll.TimerGone = bodyDuration; if (poisoned) ragdoll.Poisoned = true; }
		}
		ShowRecovery(GrandpaGoneText, "Grandpa", MaxTimerRecoverGrandpa);
		if (ES != null) ES.UsedWPOnce = true;
	}
	private static int Difficulty() { return Mathf.Clamp(PlayerPrefs.GetInt("Diff", 1), 0, 5); }
	private static float Recovery(int difficulty, bool shotgun) { return shotgun ? new[] { 150f, 90f, 60f, 30f, 15f, 7.5f }[difficulty] : new[] { 120f, 60f, 30f, 15f, 7.5f, 3f }[difficulty]; }
	private static float BodyDuration(int difficulty, bool shotgun, bool bed)
	{
		return shotgun ? new[] { 60f, 30f, 15f, 15f, 7.5f, bed ? 3f : 3.5f }[difficulty] : new[] { 60f, 30f, 15f, 7.5f, 3.75f, 1f }[difficulty];
	}
	private static void ShowRecovery(TMP_Text label, string name, float seconds)
	{
		if (label == null) return;
		label.text = name + " is now gone for " + (seconds >= 60f ? (seconds / 60f).ToString() + (seconds == 60f ? " minute" : " minutes") : seconds.ToString() + " seconds ");
		var timer = label.GetComponent<TextManager>(); if (timer != null) timer.Add(); Active(label.gameObject, true);
	}
	public void MurderNoise1() { ClearNoise("noiseobject"); }
	public void MurderNoise2() { ClearNoise("noiseobiect"); }
	private void ClearNoise(string tag) { foreach (var noise in GameObject.FindGameObjectsWithTag(tag)) if (noise != gameObject) UnityEngine.Object.Destroy(noise); }
	public void EnemyRearrange()
	{
		foreach (var enemy in new[] { Granny, Grandpa })
		{
			var agent = ComponentOn<NavMeshAgent>(enemy); if (agent != null) { agent.enabled = false; agent.baseOffset = -0.15f; }
			// The original routine sends both actors to Pos1Granny before selecting their actual spawn.
			if (enemy != null && Pos1Granny != null) enemy.transform.position = Pos1Granny.position;
			Active(enemy, false);
		}
	}
	public void SpawnGranny()
	{
		var ai = ComponentOn<AI_Granny>(Granny); if (ai == null) return;
		ai.IsWalking = false; ai.IsIdle = false; ai.NeedsToBirthTrap = false; ai.IsSearching = false;
		ai.IsChasing = false; ai.UnSeenPlayer = false; ai.SaunaLocked = false; ai.IsInSauna = false;
		ai.IsFollowingSound = false; ai.IsDying = false; ai.TimerInDangerSauna = 0f; ai.TimerInSauna = 0f;
		ai.ResetCertainTimer(); ai.PlayerClose = false;
		var agent = ComponentOn<NavMeshAgent>(Granny); if (agent != null) agent.enabled = false;
		if (ai.Anim != null && ai.Idle != null) ai.Anim.Play(ai.Idle.name);
		Active(Granny, true); if (SDeath != null) SDeath.GrannyIn = false; MurderNoise1();
		var point = SpawnPoint(false); if (point != null) Granny.transform.position = point.position;
		GrannyKilled = false; TimerGrannyRecover = 0f; ai.IsBlind = false; ai.PepperedEnemy = false;
		if (EF1 != null) EF1.InWater = false;
		if (ai.EnemyVision != null) { ai.EnemyVision.IsChasing = false; ai.EnemyVision.enabled = true; }
		if (agent != null) agent.enabled = true;
		ai.CaughtPlayer = false; ai.ChooseWaypoint();
		var steps = ai.Anim != null ? ai.Anim.GetComponent<EnemyFootstep>() : null; if (steps != null) steps.CurrentGround = 1f;
		ai.AtDishes = false;
	}
	public void SpawnGrandpa()
	{
		var ai = ComponentOn<AI_Grandpa>(Grandpa); if (ai == null) return;
		ai.IsWalking = false; ai.IsIdle = false; ai.NeedsToBirthTrap = false; ai.IsSearching = false;
		ai.IsChasing = false; ai.UnSeenPlayer = false; ai.SaunaLocked = false; ai.IsFollowingSound = false;
		ai.IsInSauna = false; ai.IsDying = false; ai.TimerInDangerSauna = 0f; ai.TimerInSauna = 0f;
		ai.ResetCertainTimer(); ai.PlayerClose = false;
		var agent = ComponentOn<NavMeshAgent>(Grandpa); if (agent != null) agent.enabled = false;
		if (ai.Anim != null && ai.Idle != null) ai.Anim.Play(ai.Idle.name);
		var shoot = ai.Anim != null ? ai.Anim.GetComponent<ShootLogic>() : null; if (shoot != null) shoot.Shooting = false;
		Active(Grandpa, true); MurderNoise2(); if (SDeath != null) SDeath.GrandpaIn = false;
		var point = SpawnPoint(true); if (point != null) Grandpa.transform.position = point.position;
		GrandpaKilled = false; TimerGrandpaRecover = 0f; ai.IsBlind = false; ai.PepperedEnemy = false;
		if (ai.EnemyVision != null) { ai.EnemyVision.IsChasing = false; ai.EnemyVision.enabled = true; }
		if (agent != null) agent.enabled = true;
		ai.CaughtPlayer = false; if (EF2 != null) EF2.InWater = false; ai.ChooseWaypoint();
		var steps = ai.Anim != null ? ai.Anim.GetComponent<EnemyFootstep>() : null; if (steps != null) steps.CurrentGround = 1f;
		if (PlayerPrefs.GetInt("Grandpa", 0) == 2 && WC != null) WC.RevSH2();
	}
	private Transform SpawnPoint(bool grandpa)
	{
		Transform[] points = grandpa ? new[] { Pos1Grandpa, Pos2Grandpa, Pos3Grandpa, Pos4Grandpa } : new[] { Pos1Granny, Pos2Granny, Pos3Granny, Pos4Granny };
		if (speedrunAdditionSpawnOn)
		{
			speedrunAdditionSpawnOn = false; int selected = Mathf.RoundToInt(spawnSelected);
			return selected >= 0 && selected < points.Length ? points[selected] : null;
		}
		if (SpawnCheck == null) return null;
		int[] choices = SpawnCheck.Floor1 ? new[] { 1, 3 } : SpawnCheck.Floor2 ? new[] { 0, 2, 3 } :
			SpawnCheck.CellarFloor ? new[] { 0, 1, 3 } : SpawnCheck.OldHouseFloor ? (grandpa ? new[] { 0, 2, 1 } : new[] { 0, 1, 2 }) : null;
		return choices != null ? points[choices[UnityEngine.Random.Range(0, choices.Length)]] : null;
	}
	public void GrandpaySleep(int SpotChoosen)
	{
		Active(Grandpa, false);
		Transform[] beds = { Bedroom1Spot, Bedroom2Spot, Bedroom3Spot, CouchSpot };
		Transform[] weapons = { ShotgunBedSpot_1, ShotgunBedSpot_2, ShotgunBedSpot_3, ShotgunCouchSpot };
		int index = SpotChoosen - 1;
		if (index >= 0 && index < beds.Length) { Pose(GrandpaBed, beds[index]); Pose(ShotgunGrandpaBed, weapons[index]); }
		Active(GrandpaBed, true);
	}
	public void SlendrinaSpawn()
	{
		if (PlayerPrefs.GetInt("Teddied", 0) == 1 || PlayerPrefs.GetInt("SlendrinaEnemy", 0) != 1) return;
		Active(Slendrina, true);
		if (Slendrina != null && SlendrinaStartPos != null) Slendrina.transform.position = SlendrinaStartPos.position;
		var ai = ComponentOn<AI_Slendrina>(Slendrina); if (ai != null) ai.EnemySpawn();
	}
	public void CrawlFaster()
	{
		if (MomKilled) return;
		StartCrawlingMom(); var animation = ComponentOn<Animation>(MomCrawl);
		if (animation != null) { animation.Play("MomCrawl"); if (animation["MomCrawl"] != null) animation["MomCrawl"].speed = 20f; }
		StartCoroutine(DisableRoarAfterCrawlFaster());
	}
	public void StartCrawlingMom() { Active(MomBody, false); Active(MomCrawl, true); Active(ActivateTriggers, false); }
	public void MomShoot() { var animation = ComponentOn<Animation>(MomCrawl); if (animation != null) animation.Play("MomDie"); MomKilled = true; }
	public void MomFroze() { MomKilled = true; var animation = ComponentOn<Animation>(MomCrawl); if (animation != null) animation.enabled = false; StartCoroutine(TimerFreeze()); }
	private IEnumerator TimerFreeze()
	{
		yield return new WaitForSeconds(3f);
		Active(SpiderKey, true); if (SpiderKey != null) SpiderKey.transform.SetParent(null);
		Active(DollFreezeMom, true); if (DollFreezeMom != null) DollFreezeMom.transform.SetParent(null);
		Active(MomFullBody, false);
	}
	public void NavOBSDisable()
	{
		var door = SDeath != null && SDeath.Sauna != null ? SDeath.Sauna.Door : null;
		var obstacle = ComponentOn<NavMeshObstacle>(door); if (obstacle != null) obstacle.carving = false;
	}
	public void MomFreed()
	{
		PutDownMom = true; Active(MomPutDownSound, true); Active(ActivateTriggers, true);
		if (MomBody != null) foreach (var body in MomBody.GetComponentsInChildren<Rigidbody>()) body.isKinematic = false;
	}
	private IEnumerator DisableRoarAfterCrawlFaster() { yield return new WaitForSeconds(5f); if (MomRoarDeath != null) MomRoarDeath.enabled = false; }
	private static T ComponentOn<T>(GameObject item) where T : Component { return item != null ? item.GetComponent<T>() : null; }
	private static GameObject SpawnAt(GameObject prefab, Transform location) { return prefab != null && location != null ? UnityEngine.Object.Instantiate(prefab, location.position, location.rotation) : null; }
	private static void Active(GameObject item, bool active) { if (item != null && item.activeSelf != active) item.SetActive(active); }
	private static void Pose(GameObject item, Transform point) { if (item != null && point != null) item.transform.SetPositionAndRotation(point.position, point.rotation); }
}
