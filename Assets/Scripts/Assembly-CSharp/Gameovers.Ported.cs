using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Gameovers.c (Granny Legacy, IL2CPP x64).
public partial class Gameovers
{
	public Gameovers() { Speed = 2; Speed2 = 1.5f; }
	private void Update()
	{
	    if (!AbleBlack) return;
	    Black.color = BlackColor;
	    float a = Fadin ? BlackColor.a - Time.deltaTime * Speed : BlackColor.a + Time.deltaTime * Speed2;
	    BlackColor.a = Mathf.Clamp01(a);
	}
	// Day 5 (6 with the extra day) has ended: clear enemies/ragdolls and play one of the game-over scenes.
	public void GameOverCall()
	{
	    foreach (var name in new[] { "GrannyFreezeDoll(Clone)", "GrandpaFreezeDoll(Clone)", "Shotgun(Clone)", "GrannyRagdoll(Clone)", "GranpaRagdoll(Clone)" })
	    { var go = GameObject.Find(name); if (go != null) Destroy(go); }
	    GrannyThere.SetActive(false);
	    Granpa.SetActive(false);
	    OldB.SetActive(false);
	    Black.gameObject.SetActive(true);
	    BG.SetActive(false);
	    AbleBlack = true;
	    MomSpiderDeactivate.SetActive(false);
	    StartCoroutine(GameOverStart());
	    RenderSettings.fog = false;
	    MainUIElem.SetActive(false);
	}
	public void EndProcess() { StartCoroutine(EndOver()); }
	private IEnumerator EndOver()
	{
	    yield return new WaitForSeconds(2);
	    TextEnd.SetActive(true);
	    yield return new WaitForSeconds(4);
	    var image = TextEnd.GetComponent<Image>();
	    if (!image) yield break;
	    image.CrossFadeAlpha(0, 1.5f, false);
	    yield return new WaitForSeconds(3.5f);
	    SceneManager.LoadScene("Menu");
	}
	private static void Place(GameObject go, Transform at)
	{ go.transform.position = at.position; go.transform.rotation = at.rotation; }
	// Shows Granny frozen at a spot: vision, agent and AI off (optionally playing a clip first).
	private AI_Granny ShowGranny(Transform at, string clip)
	{
	    GrannyThere.SetActive(true);
	    var ai = GrannyThere.GetComponent<AI_Granny>();
	    ai.EnemyVision.enabled = false; ai.Agent.enabled = false;
	    if (clip != null) ai.Anim.Play(clip);
	    ai.enabled = false;
	    Place(GrannyThere, at);
	    return ai;
	}
	// Shows Grandpa staring at a head target, holding his cane (Grandpa=1) or shotgun (Grandpa=2).
	private void ShowGrandpa(Transform head, Transform at)
	{
	    Granpa.SetActive(true);
	    var ai = Granpa.GetComponent<AI_Grandpa>();
	    ai.EnemyVision.enabled = false; ai.Agent.enabled = false;
	    ai.Anim.Play(ai.Idle.name);
	    ai.SetNewBoneControllerStare(head);
	    ai.enabled = false;
	    ai.BC.enabled = true;
	    int g = PlayerPrefs.GetInt("Grandpa");
	    if (g == 1) ai.Cane.SetActive(true); else if (g == 2) ai.Shotgun.SetActive(true);
	    Place(Granpa, at);
	}
	private static bool GrandpaOn() { int g = PlayerPrefs.GetInt("Grandpa"); return g == 1 || g == 2; }
	private void SetRegion(bool attic, bool house)
	{
	    var bg = BGN.GetComponent<BGManager>();
	    bg.InAttic = attic; bg.InCellar = false; bg.InHouse = house; bg.InOutside = false;
	    bg.CellarMusic.gameObject.SetActive(false);
	}
	private void StairsScene()
	{
	    ShowGranny(PosGrannyStairs, GrannyThere.GetComponent<AI_Granny>().Idle.name);
	    Oldteeth.SetActive(false); Creepyteeth.SetActive(true);
	    SetRegion(false, false);
	    Door["SmallDoorOpen_0"].speed = 10;
	    Door.Play("SmallDoorOpen_0");
	    Scene1.SetActive(true);
	    PlayerPrefs.SetInt("GameOverWhich", 1);
	    if (GrandpaOn()) ShowGrandpa(HeadBasementGO, PosGrandpaEnd_Basement);
	}
	// The five scenes rotate through GameOverWhich 0-4 (the robot one only with Extras), one per lost run.
	private IEnumerator GameOverStart()
	{
	    yield return new WaitForSeconds(0);
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (PlayerPrefs.GetInt("Granny") != 1 && diff != 3 && diff != 4 && diff != 5)
	    {
	        SceneManager.LoadScene("Menu");
	    }
	    else
	    {
	        int which = PlayerPrefs.GetInt("GameOverWhich");
	        if (which == 0) StairsScene();
	        else if (which == 1)
	        {
	            Scene2.SetActive(true);
	            ShowGranny(PosGrannyEnd_Backyard, null);
	            Oldteeth.SetActive(false); Creepyteeth.SetActive(true);
	            DisableGrannyWPs();
	            SetRegion(false, false);
	            PlayerPrefs.SetInt("GameOverWhich", 2);
	            if (GrandpaOn()) ShowGrandpa(HeadBackyardGO, PosGrandpaEnd_Backyard);
	        }
	        else if (which == 2)
	        {
	            Sensors.SetActive(false);
	            Car.position = CarRepos.position;
	            Scene3.SetActive(true);
	            SetRegion(false, true);
	            PlayerPrefs.SetInt("GameOverWhich", 3);
	            if (GrandpaOn()) ShowGrandpa(HeadGarageGO, PosGrandpaEnd_Garage);
	        }
	        else if (which == 3)
	        {
	            if (GrandpaOn()) ShowGrandpa(HeadAtticGO, PosGrandpaStairs);
	            Scene4.SetActive(true);
	            ShowGranny(PosGrannyEnd_Attic, "GameOver_AtticPush");
	            Oldteeth.SetActive(false); Creepyteeth.SetActive(true);
	            DisableGrannyWPs();
	            SetRegion(false, false);
	            PlayerPrefs.SetInt("GameOverWhich", 4);
	        }
	        else if (which == 4)
	        {
	            if (PlayerPrefs.GetInt("Extras") == 1)
	            {
	                RoboAnim.Play("GameOverRobot");
	                SetRegion(true, false);
	                PlayerPrefs.SetInt("GameOverWhich", 0);
	                if (GrandpaOn()) ShowGrandpa(HeadRobotGO, PosGrandpaEnd_Robot);
	            }
	            else StairsScene();
	        }
	    }
	    var drop = GameObject.Find("P_R_D(Clone)");
	    if (drop != null) Destroy(drop);
	    if (SE && SE.V && SE.V.intensity != null)
	    {
	        SE.V.intensity.value = 0;
	        if (SE.G && SE.G.intensity != null) { SE.G.intensity.value = 0; SE.Blinded = false; }
	    }
	}
	public void DisableGrannyWPs() { foreach (var r in GrannyWPs) r.enabled = false; }
	public void BasementSwitchGrandpaPosition()
	{
	    var ai = Granpa.GetComponent<AI_Grandpa>();
	    if (!ai) return;
	    ai.enabled = true;
	    ai.SetNewBoneControllerStare(HeadBasementGO_2);
	    ai.enabled = false;
	    if (!ai.BC) return;
	    ai.BC.enabled = true;
	    Place(Granpa, PosGrandpaEnd_Basement_2);
	}
}
