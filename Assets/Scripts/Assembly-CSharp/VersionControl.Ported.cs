using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/VersionControl.c (Granny Legacy, IL2CPP x64).
public partial class VersionControl
{
	private static void Off(GameObject[] objects) { foreach (var o in objects) o.SetActive(false); }
	private void OldFootsteps() { ST.S1 = W1; ST.S2 = W2; ST.S3 = W3; ST.S4 = W4; }
	private void NoBackyardWindow()
	{
	    Window.transform.localEulerAngles = new Vector3(-90, -87.73f, 88);
	    Window.name = "WindowNoBackyard";
	}
	private void NoBackdoor()
	{
	    BackdoorManag.enabled = false;
	    BackdoorManag.gameObject.name = "BYDoorUnable";
	}
	private void Unparent(GameObject grid) { grid.SetActive(true); grid.transform.parent = null; }
	private void OldItemSpots()
	{
	    CodeP2.transform.position = CodeSpawnP2.position;
	    CodeP2.transform.rotation = CodeSpawnP2.rotation;
	    BatteryP3.transform.position = BatterySpawnP3.position;
	    BatteryP3.transform.rotation = BatterySpawnP3.rotation;
	    GridBY.name = "GridUnableToUse";
	    GrandpaSpawnPos4.localPosition = NewGrandpaSpotPos4;
	    GrandpaSpawnNav2.localPosition = NewGrandpaNav2;
	}
	// Rebuilds the house as it was in an older game version (CURVERSION 0-8 = 1.0-1.8): objects, doors and enemy
	// routes that did not exist yet are switched off, renamed or moved.
	private void Start()
	{
	    int version = PlayerPrefs.GetInt("CURVERSION");
	    if (version == 0)
	    {
	        SpiderHatch.SetActive(false);
	        Door.name = "SmallDoor";
	        Pepper.SetActive(false); RemoteLock.SetActive(false); RemoteDel.SetActive(false); BloodHand.SetActive(false);
	        NoBackdoor();
	        NoBackyardWindow();
	        MomSwitch.SetActive(false);
	        EC.Pos2Granny = EC.Pos1Granny;
	        EC.Pos4Granny = EC.Pos3Granny;
	        EC.Pos1Grandpa = EC.Pos2Grandpa;
	        EC.Pos3Grandpa = EC.Pos2Grandpa;
	        EC.Pos4Grandpa = EC.Pos2Grandpa;
	        CardBoards.SetActive(false); Box1.SetActive(false); Box2.SetActive(false); Plank.SetActive(false); Pieces.SetActive(false);
	        AI.Waypoints.Clear();
	        AI2.Waypoints.Clear();
	        Screw.SetActive(false);
	        if (AI.Waypoints == null) AI.Waypoints = new List<Transform>();
	        if (AI2.Waypoints == null) AI2.Waypoints = new List<Transform>();
	        foreach (var nav in NavsGranny) AI.Waypoints.Add(nav);
	        foreach (var nav in NavsGrandpa) AI2.Waypoints.Add(nav);
	        Nav4Grandpa.position = GrandpaSP1New;
	        DoorAttic.SetActive(false);
	        CodesBatterys.SetActive(false);
	        WBox1.SetActive(false); WBox2.SetActive(false);
	    }
	    else if (version >= 1 && version <= 4)
	    {
	        OldFootsteps();
	        MomMoving.SetActive(false); Grid10.SetActive(false); SpiderHatch.SetActive(false);
	        MeatDoor.name = "MeatDoorUnavailable";
	        SmallDoorRemote.name = "SmallDoor";
	        PepperSprayDisable.SetActive(false);
	        Off(DisableOneSixObjects); Off(DisableOneFiveObjects); Off(DisableOneFourObjects);
	        if (version <= 3) Off(DisableOneThreeObjects);
	        if (version <= 2) Off(DisableOneTwoObjects);
	        CarControlsUI.SetActive(false); RemotePart1.SetActive(false); RemotePart2.SetActive(false);
	        EnginePartsCar.SetActive(false); SpecialLock.SetActive(false); LeverMom.SetActive(false);
	        OldItemSpots();
	        if (version == 4) { YouCantEscapeText.SetActive(true); }
	        else if (version == 3)
	        {
	            Unparent(HoleGrid_Garage);
	            RemoveNavGranny("Nav11"); RemoveNavGrandpa("Nav11");
	            GrandpaSpawnPos1.position = GrandpaSP1New;
	        }
	        else
	        {
	            NoBackdoor();
	            BloodHand.SetActive(false);
	            NoBackyardWindow();
	            Unparent(HoleGrid_Garage);
	            if (version == 1) Unparent(HoleGrid_Attic);
	            Unparent(HoleGrid_BY);
	            MasterKeyMelonOFF.SetActive(true); PadlockKeyMelonOFF.SetActive(true); PadlockKeyMelonOFF2.SetActive(true);
	            if (version == 1) { ButtonTeddyRoom.SetActive(false); DoorAttic.SetActive(false); }
	            RemoveNavGranny("Nav11"); RemoveNavGrandpa("Nav11");
	            RemoveNavGranny("Nav15"); RemoveNavGranny("Nav16");
	            RemoveNavGrandpa("Nav8");
	            if (version == 1) { RemoveNavGranny("Nav12"); RemoveNavGrandpa("Nav4"); }
	            GrandpaSpawnPos1.position = GrandpaSP1New;
	            GrandpaSpawnPos3.position = GrandpaSP3New;
	            if (version == 1) Nav4Grandpa.position = GrandpaSP1New;
	            WBox1.SetActive(false); WBox2.SetActive(false);
	            if (version == 1) Pieces.SetActive(false);
	        }
	    }
	    else if (version == 5 || version == 6)
	    {
	        OldFootsteps();
	        MomMoving.SetActive(false); Grid10.SetActive(false); SpiderHatch.SetActive(false);
	        MeatDoor.name = "MeatDoorUnavailable";
	        if (version == 5)
	        {
	            SmallDoorRemote.name = "SmallDoor";
	            PepperSprayDisable.SetActive(false);
	            Off(DisableOneSixObjects); Off(DisableOneFiveObjects);
	            RemotePart1.SetActive(false); RemotePart2.SetActive(false);
	        }
	        else Off(DisableOneSixObjects);
	        OldItemSpots();
	    }
	    else if (version == 7)
	    {
	        OldFootsteps();
	        MomMoving.SetActive(false); Grid10.SetActive(false); SpiderHatch.SetActive(false);
	    }
	    else if (version == 8) { Grid17.SetActive(false); Grid10.SetActive(false); }
	    if (PlayerPrefs.GetInt("Extras") == 0) AtticTree.SetActive(false);
	}
	public void RemoveNavGrandpa(string n) { AI2.Waypoints.RemoveAll(t => t != null && t.name == n); }
	public void RemoveNavGranny(string n) { AI.Waypoints.RemoveAll(t => t != null && t.name == n); }
}
