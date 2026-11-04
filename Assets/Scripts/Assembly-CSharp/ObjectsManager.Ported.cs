using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ObjectsManager.c (Granny Legacy, IL2CPP x64).
public partial class ObjectsManager
{
	private static void SetAll(GameObject[] objects, bool on) { foreach (var o in objects) o.SetActive(on); }
	private void ToHouse(Transform t) { t.position = SetVaseToHousePos.position; }
	private static void PlaceAt(GameObject go, Transform at) { go.transform.position = at.position; go.transform.rotation = at.rotation; }
	private void Start()
	{
	    if (PlayerPrefs.GetInt("Extras") == 0)
	    {
	        OmGiStuff.SetActive(false);
	        int version = PlayerPrefs.GetInt("CURVERSION");
	        bool flash = PlayerPrefs.GetInt("FlashL") == 1;
	        if (version == 8)
	        {
	            AllItemsMelon.SetActive(false); BatterysCodes.SetActive(false); MainItems.SetActive(true);
	            if (flash) { SeedStuff.SetActive(true); Seed_Main.SetActive(true); }
	        }
	        else if (version >= 0 && version <= 7)
	        {
	            // Older game versions (CURVERSION 0-7 = 1.0-1.7). "FlashL" runs use the seeded item layout of
	            // that version and switch off every object added after it; otherwise the old/first layout is shown.
	            if (flash)
	            {
	                SeedStuff.SetActive(true);
	                new[] { Seed_10, Seed_11, Seed_12, Seed_13, Seed_14, Seed_15, Seed_16, Seed_17 }[version].SetActive(true);
	                BatterysCodes.SetActive(false); AllItemsMelon.SetActive(false); MainItems.SetActive(true);
	                var newer = new[] { DisableOneZeroObjects, DisableOneOneObjects, DisableOneTwoObjects, DisableOneThreeObjects,
	                    DisableOneFourObjects, DisableOneFiveObjects, DisableOneSixObjects, DisableOneSevenObjects };
	                for (int v = 7; v >= version; v--) SetAll(newer[v], false);
	            }
	            else if (version == 0) FirstVer.SetActive(true);
	            else OldVer.SetActive(true);
	            // Item spots that did not exist yet in that version are moved into the house (vases) or to their old places.
	            if (version <= 4) PlaceAt(PaintingBedroom, PaintingBedroomRepos);
	            if (version <= 2) ObjResetPoint.position = ObjectResetPointOld.position;
	            if (version == 0) ToHouse(CellarVasePos);
	            if (version <= 1) PicturePos_3.transform.position = SetPic3ToSpotPos.position;
	            if (version <= 2) { PicturePos_4_1.transform.position = SetPic4ToSpotPos.position; PicturePos_4_2.transform.position = SetPic4ToSpotPos.position; }
	            if (version <= 4) { ToHouse(AtticVasePos1); ToHouse(AtticVasePos2); }
	            if (version <= 6) { ToHouse(SewerVasePos1); ToHouse(SewerVasePos2); ToHouse(SewerVasePos3); }
	            ToHouse(SpiderCellarVasePos);
	            if (version <= 2) ToHouse(BackyardVasePos);
	        }
	        ElectricCoil.SetActive(false);
	    }
	    else
	    {
	        SetAll(ExtrasItems, true);
	        MainItems.SetActive(true); AllItemsMelon.SetActive(false); BatterysCodes.SetActive(false);
	        AreaCover.SetActive(false); AreaCoverSubBasement.SetActive(false);
	        if (PlayerPrefs.GetInt("FlashL") == 1) { SeedStuff.SetActive(true); Seed_Extra.SetActive(true); }
	    }
	    AmmoThere.SetActive(true);
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (diff == 0 || diff == 1)
	    {
	        // Teddy bear pieces (easy difficulties only).
	        Pieces.SetActive(true);
	        int a = UnityEngine.Random.Range(0, TB1_POSES.Length), b = UnityEngine.Random.Range(0, TB2_POSES.Length);
	        int c = UnityEngine.Random.Range(0, TB3_POSES.Length), d = UnityEngine.Random.Range(0, TB4_POSES.Length);
	        PlaceAt(TB1, TB1_POSES[a]); PlaceAt(TB2, TB2_POSES[b]); PlaceAt(TB3, TB3_POSES[c]); PlaceAt(TB4, TB4_POSES[d]);
	    }
	    int grandpa = PlayerPrefs.GetInt("Grandpa");
	    if (grandpa == 1 || grandpa == 2)
	    {
	        GrandpaVase.SetActive(true);
	        GrandpaVase.transform.position = VasePositionsGrandpa[UnityEngine.Random.Range(0, VasePositionsGrandpa.Length)].position;
	    }
	    // PresetChoosin: 0 = cycle through presets (saved "Preset" is the next one), 1 = random, 2-6 = preset 1-5.
	    int choosing = PlayerPrefs.GetInt("PresetChoosin");
	    int preset = -1;
	    GameObject fifthFirst = Preset1First;
	    if (choosing == 0) preset = PlayerPrefs.GetInt("Preset");
	    else if (choosing == 1) { preset = UnityEngine.Random.Range(0, 5); fifthFirst = Preset4First; }
	    else if (choosing >= 2 && choosing <= 6) preset = choosing - 2;
	    switch (preset)
	    {
	        case 0: ApplyPreset(BatteryPL1, CodePL1, GuillotinePL1, 1, Preset1First, Preset1O, 0); break;
	        case 1: ApplyPreset(BatteryPL2, CodePL2, GuillotinePL2, 2, Preset2First, Preset2O, 1); break;
	        case 2: ApplyPreset(BatteryPL3, CodePL3, GuillotinePL3, 3, Preset3First, Preset3O, 2); break;
	        case 3: ApplyPreset(BatteryPL4, CodePL4, GuillotinePL4, 4, Preset4First, Preset4O, 3); break;
	        case 4: ApplyPreset(BatteryPL5, CodePL5, GuillotinePL5, 0, fifthFirst, Preset5O, 4); break;
	    }
	    int pad = UnityEngine.Random.Range(0, PositionsNumberPad.Length);
	    PlaceAt(NumberPad1, PositionsNumberPad[pad]);
	    PlaceAt(NumberPad2, PositionsNumberPad[pad]);
	    int rat = UnityEngine.Random.Range(0, 2);
	    if (rat == 0) RemoteRatHang1.TheOne = true; else if (rat == 1) RemoteRatHang2.TheOne = true;
	    if (PlayerPrefs.GetInt("GrannyWP") == 1 && PlayerPrefs.GetInt("Granny") == 1) CrossbowOnly.SetActive(false);
	    if (PlayerPrefs.GetInt("GrannyWP") == 2 && PlayerPrefs.GetInt("Granny") == 1) Spray.SetActive(false);
	}
	private void ApplyPreset(GameObject battery, GameObject code, GameObject guillotine, int next, GameObject first, GameObject layout, float current)
	{
	    battery.SetActive(true); code.SetActive(true); guillotine.SetActive(true);
	    PlayerPrefs.SetInt("Preset", next);
	    PlayerPrefs.Save();
	    first.SetActive(true); layout.SetActive(true);
	    PresetChoosenCurrent = current;
	}
	// Versions 1.3/1.4 without a seed: the cutting pliers only exist in preset 2, and preset 4 uses the 1.4 weapon key.
	private void Update()
	{
	    int version = PlayerPrefs.GetInt("CURVERSION");
	    if ((version != 4 && version != 3) || PlayerPrefs.GetInt("FlashL") != 0) return;
	    if (CuttingPliersP2Old != null) CuttingPliersP2Old.SetActive(PresetChoosenCurrent == 1);
	    if (PresetChoosenCurrent == 3 && WeaponKeyP4OneFour != null) SwitchOneFourVersion();
	}
	public void SwitchOneFourVersion()
	{
	    GuillotinePL4.SetActive(false);
	    WeaponKeyP4OneFour.SetActive(true);
	    WPKeyFourCompartment.SetActive(false);
	}
}
