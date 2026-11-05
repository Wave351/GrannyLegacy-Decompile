using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/HandlePuzzles.c (Granny Legacy, IL2CPP x64).
public partial class HandlePuzzles
{
	public HandlePuzzles()
	{
	    SpeedFill = .1f; SpeedWheelMain = 25; ValueHinkCurrentY = -30.87071f; HinkGetSpeed = 4; ValueWinchZ = -1.707599E-06f;
	    ValueWireY = 4.432013f; MaxBarbScale = 1.089923f; MaxBarbPos = 4.389988f; SpeedBarbWire = 10; ScaleBarbWire = 6.593704f;
	    SpeedBarbWireScaleSpeed = 5; BarbSpedUp = 1; Tilling1 = 1; SpeedRevTilling = .1f; SpeedWinchHandle = 50; MaxBarbwire = 36.28f;
	    SpeedRotateFan = 3; FanStopSpeed = 10; RotateFan = true; GasFill = .03f;
	}
	private void Start()
	{
	    var red = new Color(1, 0, 0, 1);
	    L1.material.color = red; L2.material.color = red; LightPC.material.color = red; LightRemote.material.color = red;
	}
	private void Update()
	{
	    Barbwire.transform.localPosition = new Vector3(138.145f, ValueWireY, -105.0274f);
	    Hink.transform.localPosition = new Vector3(138.145f, ValueHinkCurrentY, -105.0274f);
	    WinchHandleThere.transform.localEulerAngles = new Vector3(0, 50, ValueWinchZ);
	    WheelHold.transform.localEulerAngles = new Vector3(0, 50, WheelCurrentValZ);
	    Barbwire.transform.localScale = new Vector3(.633594f, .633594f, ScaleBarbWire);
	    FuelMeterUI.fillAmount = FuelMeter;
	    Barb.material.mainTextureScale = new Vector2(Tilling1, 1);
	    Barb.material.mainTexture = PlayerPrefs.GetInt("Textures") == 1 ? RopeNM : RopeN;
	    // All four pictures put back: the extra sixth day.
	    if (Pic1.activeSelf && Pic2.activeSelf && Pic3.activeSelf && Pic4.activeSelf && !Days.ExtraDay)
	    {
	        Days.ExtraDay = true;
	        Ray.StopSetOfTexts();
	        RewardedT.SetActive(true);
	        RewardedT.GetComponent<TextManager>().Add();
	    }
	    if (!TankFuelled)
	    {
	        if (!FillingTank) FillSound.SetActive(false);
	        else
	        {
	            FillSound.SetActive(true);
	            FuelMeter += Time.deltaTime * SpeedFill;
	            var audio = FillSound.GetComponent<AudioSource>();
	            audio.pitch += GasFill * Time.deltaTime;
	            if (1 <= FuelMeter) TankFuelled = true;
	        }
	    }
	    else
	    {
	        FillSound.SetActive(false);
	        if (OpenedTank) { OpenedTank = false; FuelTank.Play("tanklockClose"); }
	    }
	    if (Cogwheel1.activeSelf && Cogwheel2.activeSelf && !PlacedCogwheels)
	    {
	        LekhusHolder.GetComponent<Animation>().Play("openToyLock");
	        CollLekhus.SetActive(false);
	        PlacedCogwheels = true;
	        Noise_Playhouse.SetActive(true);
	    }
	    if (P1.activeSelf && P2.activeSelf && P3.activeSelf && !GotShotgun)
	    {
	        DoneShotgun.SetActive(true);
	        P1.SetActive(false); P2.SetActive(false); P3.SetActive(false);
	        GotShotgun = true;
	    }
	    // The winch pulls the barbed-wire cage up.
	    if (!IsUpFully)
	    {
	        if (!RotatingWinch) SoundEffectWinch.SetActive(false);
	        else
	        {
	            SoundEffectWinch.SetActive(true);
	            ValueHinkCurrentY += Time.deltaTime * HinkGetSpeed;
	            ValueWinchZ += Time.deltaTime * SpeedWinchHandle;
	            WheelCurrentValZ += Time.deltaTime * SpeedWheelMain;
	            ScaleBarbWire -= Time.deltaTime * SpeedBarbWireScaleSpeed;
	            Tilling1 -= Time.deltaTime * SpeedRevTilling;
	            if (ScaleBarbWire <= MaxBarbScale) IsUpFully = true;
	            if (ScaleBarbWire < 1.5f) SpeedRevTilling = .18f;
	        }
	    }
	    else SoundEffectWinch.SetActive(false);
	    Fan.transform.eulerAngles = new Vector3(0, 90, FanRotation);
	    FanRotation += Time.deltaTime * SpeedRotateFan;
	    if (CutAtticWire)
	    {
	        if (SpeedRotateFan < 0) { SpeedRotateFan = 0; RotateFan = false; }
	        else if (RotateFan) SpeedRotateFan -= Time.deltaTime * FanStopSpeed;
	        FanCollider.SetActive(false);
	        FanAudio.volume -= Time.deltaTime;
	    }
	    // Front door requirements by game version and difficulty (the 2 planks are always needed).
	    bool basics = PlankHit == 2 && UsedPadlockKey && CutWire1 && CutWire2;
	    int version = PlayerPrefs.GetInt("CURVERSION");
	    int diff = PlayerPrefs.GetInt("Diff");
	    if (PlayerPrefs.GetInt("EL") == 1 && version != 0 && version != 1 && version != 2)
	    {
	        if (basics && UsedCode && UsedBattery && OpenedExLock) IsDoorFree = true;
	    }
	    else if (version == 0 || version == 1 || version == 2) { if (basics && UsedCode) IsDoorFree = true; }
	    else if (diff == 0) { if (basics) IsDoorFree = true; }
	    else if (diff == 1) { if (basics && UsedCode) IsDoorFree = true; }
	    else if (diff == 2) { if (basics && UsedCode && UsedBattery) IsDoorFree = true; }
	    else if ((diff == 3 || diff == 4 || diff == 5) && basics && UsedCode && UsedBattery && OpenedExLock) IsDoorFree = true;
	    if (ScrewsPLSpiderCellar == 4 && !ScrewedPlattaSpiderCellar) { ScrewedPlattaSpiderCellar = true; StartCoroutine(ScrewPlattaGoingDownSpiderCellar()); }
	    if (ScrewsSwitchEX == 4 && !ScrewedSwitchEX) { ScrewedSwitchEX = true; StartCoroutine(ScrewPanelSwitchDown()); }
	    if (ScrewsOldHouse == 2 && !ScrewedOldHouse) { ScrewedOldHouse = true; StartCoroutine(MetalRustdown()); }
	}
	private IEnumerator ScrewPlattaGoingDownSpiderCellar()
	{
	    yield return new WaitForSeconds(1.15f);
	    ScrewPlattaSpiderCellar.Play("skruvLucka");
	    yield return new WaitForSeconds(7);
	    if (ScrewPlattaSpiderCellar) ScrewPlattaSpiderCellar.gameObject.SetActive(false);
	}
	private IEnumerator ScrewPanelSwitchDown()
	{
	    yield return new WaitForSeconds(1);
	    var body = PanelSwitch.GetComponent<Rigidbody>();
	    if (!body) yield break;
	    body.isKinematic = false;
	    PanelSwitch.layer = LayerMask.NameToLayer("Item");
	    yield return new WaitForSeconds(7);
	    PanelSwitch.SetActive(false);
	}
	private IEnumerator MetalRustdown()
	{
	    yield return new WaitForSeconds(1);
	    var body = MetalRust.GetComponent<Rigidbody>();
	    if (!body) yield break;
	    body.isKinematic = false;
	    yield return new WaitForSeconds(7);
	    MetalRust.SetActive(false);
	}
	private static void SetVolume(GameObject[] objects, float volume)
	{
	    foreach (var o in objects) { var a = o ? o.GetComponent<AudioSource>() : null; if (a) a.volume = volume; }
	}
	private static void StopParticles(GameObject[] objects)
	{
	    foreach (var o in objects) { var p = o ? o.GetComponent<ParticleSystem>() : null; if (p) p.Stop(); }
	}
	// Recharging the robot: the sparks run along the four sites, then fade out, and the screen says "YES".
	private IEnumerator RechargeProcess()
	{
	    var sparking = new[] { Sparking1, Sparking2, Sparking3, Sparking4 };
	    var charge = new[] { ChargeSpark1, ChargeSpark2, ChargeSpark3, ChargeSpark4 };
	    yield return new WaitForSeconds(1);
	    Sparking1.SetActive(true);
	    yield return new WaitForSeconds(1);
	    ChargeSpark1.SetActive(true); Sparking2.SetActive(true);
	    yield return new WaitForSeconds(1);
	    ChargeSpark2.SetActive(true); Sparking3.SetActive(true);
	    yield return new WaitForSeconds(1);
	    ChargeSpark3.SetActive(true); Sparking4.SetActive(true);
	    yield return new WaitForSeconds(1);
	    ChargeSpark4.SetActive(true);
	    yield return new WaitForSeconds(6.5f);
	    StopParticles(charge); SetVolume(charge, .7f);
	    yield return new WaitForSeconds(.3f);
	    SetVolume(charge, .2f);
	    yield return new WaitForSeconds(.3f);
	    SetVolume(charge, 0);
	    yield return new WaitForSeconds(.7f);
	    StopParticles(sparking); SetVolume(sparking, .7f);
	    yield return new WaitForSeconds(.2f);
	    SetVolume(sparking, 0);
	    yield return new WaitForSeconds(.5f);
	    RechargedRobot = true;
	    if (YesStatus) YesStatus.text = "YES";
	}
	public void UsePadlockKey2()
	{
	    usedPadlockForPort = true;
	    var body = HangLockPort.GetComponent<Rigidbody>();
	    if (body) body.isKinematic = false;
	}
	public void OpenPort() { openedPort = true; GaragePort.Play("garageportOpen"); AudioGP.Play(); }
	public void PlaceFusePanel() { FusePlaced.SetActive(true); fuseInPlace = true; }
	public void PlaceP1() { Pic1.SetActive(true); }
	public void PlaceP2() { Pic2.SetActive(true); }
	public void PlaceP3() { Pic3.SetActive(true); }
	public void PlaceP4() { Pic4.SetActive(true); }
	public void StartRecharging() { IsRecharging = true; StartCoroutine(RechargeProcess()); }
	public void UseElecKey()
	{
	    usedElecKey = true;
	    var anim = DoorPanel.GetComponent<Animation>(); if (!anim) return;
	    anim.Play("OpenCoilDoor");
	    var audio = DoorPanel.GetComponent<AudioSource>(); if (audio) audio.Play();
	}
	public void OpenRemote()
	{
	    usedRemote = true;
	    HandleIn.SetActive(false); HandleOut.SetActive(true);
	    SoundEffectRemote.SetActive(true);
	    SmallDoor.name = "SmallDoor";
	    LightRemote.material.color = new Color(0, 1, 0, 1);
	}
	public void UseSparkplug() { PlacedSparkplug.SetActive(true); usedSparkPlug = true; Noise_Spark.SetActive(true); }
	public void EnginePlace() { EnginePartPlace.SetActive(true); PlacedEnginePart = true; Noise_Engine.SetActive(true); }
	public void CarBatteryPlace() { CarBatteryInPlace.SetActive(true); PlacedCarBattery = true; Noise_CarBattery.SetActive(true); }
	public void PlaceUSB()
	{
	    PlacedUSB = true;
	    DataUSBPlaced.SetActive(true);
	    LightPC.material.color = new Color(0, 1, 0, 1);
	    NoDataT.SetActive(false); YesDataT.SetActive(true);
	}
	public void OpenTrunkCarKey() { UsedCarKey = true; CarTrunk.OpenAction(); }
	public void PlacePlank()
	{
	    usedPlank = true;
	    if (TBF != null) TBF.BreakPlanks();
	    TriggerPlanks.SetActive(false); HighlightPL.SetActive(false); PlankPL.SetActive(true);
	}
	public void UsePlayhouse()
	{
	    var body = LekhusPadlock.GetComponent<Rigidbody>();
	    if (!body) return;
	    body.isKinematic = false;
	    LekhusDoor.EnabledAnim = true;
	    LekhusDoor.name = "PlayHouseDoor";
	    usedPlayhouseKey = true;
	}
	public void UseSpecialKey()
	{
	    var door = SlidingDoorAttic.GetComponent<Animation>(); if (!door) return;
	    door.Play("vind2DoorOpen");
	    var lockAnim = AtticLockPlace.GetComponent<Animation>(); if (!lockAnim) return;
	    lockAnim.Play("vind2LockOpen");
	    UsedSpecialKey = true;
	    DropFixDis.SetActive(false);
	    Grandpa.Waypoints.Add(NewNavGranpa);
	}
	public void PlaceMelon() { PlacedMelon = true; MelonPlace.SetActive(true); }
	public void CutAtticWires() { AtticCable.SetActive(false); AtticCableB.SetActive(true); CutAtticWire = true; Noise_Fan.SetActive(true); }
	public void UseWPKey()
	{
	    UsedWPKey = true;
	    CrossBowDoor.Play("VapenDoorOpen");
	    var audio = CrossBowDoor.GetComponent<AudioSource>(); if (!audio) return;
	    audio.Play();
	    var col = WeaponCrossbow.GetComponent<Collider>(); if (!col) return;
	    col.enabled = true;
	    foreach (var dart in DartsWeapon) dart.GetComponent<Collider>().enabled = true;
	    Noise_WPLocker.SetActive(true);
	}
	public void PullBookshelf()
	{
	    Bookshelf.Play("BookShelfMove");
	    var audio = Bookshelf.GetComponent<AudioSource>(); if (!audio) return;
	    audio.Play();
	    SwitchBookshelf.Play("Tavelspak");
	}
	public void OpenSafe()
	{
	    var anim = SafeDoorAnim.GetComponent<Animation>(); if (!anim) return;
	    anim.Play("safeDoorOpen");
	    var audio = SafeDoorAnim.GetComponent<AudioSource>(); if (!audio) return;
	    audio.Play();
	    UsedSafeKey = true;
	    SafeDoorAnim.layer = LayerMask.NameToLayer("Item");
	}
	public void PutBook() { BookPlaced.SetActive(true); EnemyController.MomFreed(); Noise_Book.SetActive(true); }
	public void MasterKeyUsed() { UsedMaster = true; }
	public void ExtremeUnlock()
	{
	    EXLock.SetActive(false); EXUnlock.SetActive(true);
	    Instantiate(NoiseObj, EXLock.transform.position, EXLock.transform.rotation);
	    OpenedExLock = true;
	    Noise_Screwdriver.SetActive(true);
	}
	public void UseBattery()
	{
	    BatteryPlaced.SetActive(true);
	    BatteryAnim.Play("BatteryLockOpen");
	    Instantiate(NoiseObj, BatteryPlaced.transform.position, BatteryPlaced.transform.rotation);
	    BatterySpak.Stop();
	    BatterySpak.Play("BattSpakOK");
	    UsedBattery = true;
	}
	public void NoBattery() { BatterySpak.Play("BattSpakNotOK"); }
	public void CuttingWire1()
	{
	    Cable1.SetActive(false); Cable1B.SetActive(true); CutWire1 = true;
	    L1.material.color = new Color(0, 1, 0, 1);
	    Noise_PliersDoor.SetActive(true);
	}
	public void CuttingWire2()
	{
	    Cable2.SetActive(false); Cable2B.SetActive(true); CutWire2 = true;
	    L2.material.color = new Color(0, 1, 0, 1);
	}
	public void UseCode()
	{
	    UsedCode = true;
	    var body = CodePad.GetComponent<Rigidbody>(); if (!body) return;
	    body.isKinematic = false;
	    var anim = CodeLock.GetComponent<Animation>(); if (anim) anim.Play("DlockAnim");
	}
	public void UsePadlockKey()
	{
	    UsedPadlockKey = true;
	    var body = HanglockDoor.GetComponent<Rigidbody>(); if (!body) return;
	    body.isKinematic = false;
	    var bom = HangBom.GetComponent<Rigidbody>(); if (bom) bom.isKinematic = false;
	}
	// The front door plank takes two hits: the first loosens it, the second tears it off its hinge.
	public void HitPlankDoor()
	{
	    if (PlankHit == 0)
	    {
	        var body = PlankDoor ? PlankDoor.GetComponent<Rigidbody>() : null;
	        if (!body) return;
	        body.isKinematic = false;
	        AudioSource.PlayClipAtPoint(SmashPlank, PlankDoor.transform.position);
	        PlankHit = 1;
	    }
	    else if (PlankHit == 1)
	    {
	        Destroy(PlankDoor.GetComponent<HingeJoint>());
	        AudioSource.PlayClipAtPoint(SmashPlank, PlankDoor.transform.position);
	        PlankHit = 2;
	    }
	}
	public void OpenIronDoors() { TriggerLeave.SetActive(true); OpenedIronDoors = true; IronDoors.Play("openIrondoors"); }
	public void WoodenStickPlaced() { PlacedStick = true; IronDoors.Play("RotateSpinLock"); }
	public void CutChains() { UsedChainCutter = true; IronDoors.Play("cutChain"); }
	public void UseRustyPadlock() { UsedRustyPadlock = true; PadlockRusty.isKinematic = false; }
	public void OpenHatchSpiderCellar() { SpiderCellarDone = true; HatchSpiderCellar.Play("luckaVRopen"); }
	public void WheelCrankRotate() { AiMom.AddNewNav(); WheelCrankPlaced = true; WheelCrankAction.Play("SpiderGrindOpen"); }
}
