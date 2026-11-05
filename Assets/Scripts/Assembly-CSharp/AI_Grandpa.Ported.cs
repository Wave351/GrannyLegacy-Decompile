using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.ootii.Actors.BoneControllers;

// Ported from Annotated-Pseudocode/AI_Grandpa.c (Granny Legacy, IL2CPP x64).
public partial class AI_Grandpa
{
	private bool AgentReady { get { return Agent && Agent.enabled && Agent.isOnNavMesh; } }
	private bool AllyCaught { get { return Ally && Ally.CaughtPlayer; } }
	private bool ProjectileBusy { get { return IsShooting; } }
	private void Start()
	{
	    int d = Mathf.Clamp(PlayerPrefs.GetInt("Diff"), 0, 5);
	    Run_Speed = new float[] { 2.5f, 3.8f, 5, 8.5f, 12, 14 }[d];
	    Run_Anim = new float[] { 1.25f, 1.9f, 2.5f, 4.25f, 5, 5.5f }[d];
	    TimerMaxShoot = new float[] { 2, 1.8f, 1.1f, .8f, .5f, .15f }[d];
	    if (d >= 4) { DistanceJumpscare = d == 4 ? 3.5f : 4; TimerSaunaMax = 5; }
	    bool flashlight = PlayerPrefs.GetInt("FlashLightIsOn") == 1;
	    OldShader = Shader.Find(flashlight ? "Standard" : "Legacy Shaders/Self-Illumin/Bumped Diffuse");
	    BlindShader = Shader.Find(flashlight ? "Standard" : "Particles/Standard Unlit");
	    if (flashlight) OldColor = BlindColor = new Color(.462264f, .462264f, .462264f, 1);
	    if (Anim && Shoot && Anim[Shoot.name] != null) Anim[Shoot.name].speed = new float[] { .8f, 1, 1.3f, 1.4f, 1.6f, 2 }[d];
	    int weapon = PlayerPrefs.GetInt("Grandpa");
	    if (weapon == 1 && Cane) Cane.SetActive(true); if (weapon == 2 && Shotgun) Shotgun.SetActive(true);
	}
	private void FixedUpdate()
	{
	    if (!Player || !PlayerStatus || !Agent) return;
	    if (RayCheckP)
	    {
	        RayCheckP.LookAt(PlayerHead ? PlayerHead.position : Player.position);
	        RaycastHit hit;
	        PlayerTouchedRay = Physics.Raycast(RayCheckP.position, RayCheckP.forward, out hit, DistanceRayPL, LayerExP)
	                           && hit.collider.CompareTag("Player");
	    }
	    if (PlayerClose && PlayerTouchedRay && !IsBlind && !IsChasing && !PlayerStatus.HidingFromHouseEnemy
	        && !(EnemyVision && EnemyVision.SC && EnemyVision.SC.Entered_SC)) ChaseAction();
	    if (!CaughtPlayer && !IsDying)
	    {
	        if (!AgentReady) return;
	        CheckDoors();
	        traps = GameObject.FindGameObjectsWithTag("BearTrapActivated");
	        SetNewBoneControllerStare(PlayerHead);
	        var noise = GameObject.FindGameObjectWithTag(NoiseObjectTag);
	        if (noise && !IsChasing && !IsAngry && !PepperedEnemy)
	        {
	            NoiseObj = noise; IsFollowingSound = true; IsWalking = IsIdle = IsSearching = false;
	            ResetCertainTimer();
	        }
	        if (NeedsToBirthTrap)
	        {
	            TimerAfterSearchTrap += Time.deltaTime;
	            if (TimerAfterSearchTrap >= TimerAfterSearchTrapMax)
	            { PlacingTrap = true; NeedsToBirthTrap = false; TimerAfterSearchTrap = 0; }
	        }
	        else TimerAfterSearchTrap = 0;
	        if (PlacingTrap)
	        {
	            TimerDropTrap += Time.deltaTime;
	            if (TimerDropTrap >= TimerMaxTrap)
	            {
	                var prefab = PlayerPrefs.GetInt("Textures") == 1 ? BearTrapNM : BearTrap;
	                if (prefab && PointTrap) Instantiate(prefab, PointTrap.position, PointTrap.rotation);
	                PlacingTrap = false; TimerDropTrap = 0;
	            }
	        }
	        else TimerDropTrap = 0;
	        if (IsAngry && !PepperedEnemy && !IsChasing) ChaseAction();
	        if (FootstepS) FootstepS.volume = PlacingTrap || IsOpeningDoor ? FootstepS.volume - 1.5f * Time.deltaTime : 1;
	        UpdateSauna();
	        if (IsDying || !gameObject.activeInHierarchy) return;
	        if (SensorSprayKill) SensorSprayKill.SetActive(IsBlind);
	        if (!IsBlind) BlindTimer = 0;
	        else
	        {
	            BlindTimer += Time.deltaTime;
	            if (BlindTimer >= 30) { IsBlind = false; BlindTimer = 0; }
	        }
	        if (Skin)
	        {
	            if (IsBlind && MeshBlind) Skin.sharedMesh = MeshBlind;
	            else if (!IsBlind && MeshGood) Skin.sharedMesh = MeshGood;
	            var material = Skin.material; var shader = IsBlind ? BlindShader : OldShader;
	            if (shader) material.shader = shader; material.color = IsBlind ? BlindColor : OldColor;
	        }
	        var falling = Player.GetComponent<FallingHolder>();
	        bool playerFalling = falling && (falling.isLanding || falling.isFalling);
	        if (Shotgun && Shotgun.activeSelf && !IsShooting && EnemyVision && EnemyVision.Seeing && IsChasing && !playerFalling && !PlayerStatus.HidingUnderThings)
	        {
	            TimerShoot += Time.deltaTime;
	            if (TimerShoot >= TimerMaxShoot)
	            { TimerShoot = 0; IsShooting = true; StartCoroutine(ShotgunFinishShot()); PlayClip(Shoot, Anim && Shoot && Anim[Shoot.name] != null ? Anim[Shoot.name].speed : 1); }
	        }
	        if (IsChasing)
	        {
	            if (ScreenEffects) ScreenEffects.NormalSee = true;
	            CurrentTarget = PlayerSpot ? PlayerSpot.position : Player.position;
	            if (EnemyVision && EnemyVision.IsChasing)
	            { if (MusicBG) MusicBG.GrannyHunting = true; if (BC) BC.enabled = true; }
	            IsWalking = IsIdle = IsFollowingSound = false;
	            Agent.SetDestination(PlayerStatus.HidingUnderThings ? PlayerPos : CurrentTarget);
	            bool stationary = Agent.velocity.sqrMagnitude < 1e-10f;
	            if (UnSeenPlayer && !IsAngry && EnemyVision && !EnemyVision.PlayerSpotted
	                && stationary && !IsOpeningDoor && Vector3.Distance(transform.position, Agent.destination) <= (Agent.isStopped ? 4.5f : 2.5f))
	                EnemyVision.TimerStopVision = 100;
	            if ((IsAngry || (EnemyVision && EnemyVision.PlayerSpotted)) && stationary && Vector3.Distance(transform.position, Player.position) <= 2.5f)
	                CheckHidingPlayer();
	            else TimerBedCaught = 0;
	            if (CaughtPlayer) { UpdateCaughtLook(); UpdateWeaponColliders(); return; }
	            Agent.speed = IsOpeningDoor || SaunaLocked || ProjectileBusy ? 0 : Run_Speed;
	            MurderNoiseObjs(); TimerNearNoise = 0; 
	        }
	        else if (IsIdle && !IsAngry)
	        {
	            TimerChoosing += Time.deltaTime; PlayClip(Idle);
	            if (TimerChoosing >= TimerMaxNewNav) { TimerChoosing = 0; ChooseWaypoint();  }
	        }
	        else if (IsSearching && !IsAngry)
	        {
	            Agent.speed = 0;
	            if (Agent.velocity.sqrMagnitude < 1e-10f)
	            {
	                SearchTimer += Time.deltaTime; TimerNearNoise = 0; PlayClip(Search);
	                if (SearchTimer >= MaxSearch)
	                {
	                    SearchTimer = 0; IsSearching = false;
	                    if (TrapPregnant && traps.Length < 5) NeedsToBirthTrap = true;
	                    if (Furni) Furni.CleanUp(); SpeakEnemy(); ChooseWaypoint(); 
	                }
	            }
	        }
	        else if (IsFollowingSound && !IsAngry && !PepperedEnemy)
	        {
	            IsWalking = false;
	            if (NoiseObj) CurrentTarget = NoiseObj.transform.position;
	            Agent.SetDestination(CurrentTarget); Agent.speed = IsOpeningDoor || SaunaLocked ? 0 : Run_Speed;
	            if (Vector3.Distance(transform.position, Agent.destination) <= DistanceFromNav)
	            {
	                TimerNearNoise += Time.deltaTime;
	                if (TimerNearNoise >= TimerMaxNoise) { IsFollowingSound = false; IsSearching = true; MurderNoiseObjs(); }
	            }
	            else TimerNearNoise = 0;

	        }
	        else if (IsWalking && !IsFollowingSound && !IsAngry)
	        {
	            Agent.speed = IsOpeningDoor || PlacingTrap ? 0 : Walk_Speed;
	            PlayClip(WalkAnim, Walk_Anim); Agent.SetDestination(CurrentTarget);
	            if (Vector3.Distance(transform.position, Agent.destination) <= DistanceFromNav)
	            {
	                string nav = Waypoints != null && CurrentWaypoint >= 0 && CurrentWaypoint < Waypoints.Count && Waypoints[CurrentWaypoint]
	                             ? Waypoints[CurrentWaypoint].name : "";
	                int bed = nav == "Nav7" ? 1 : nav == "Nav17" ? 2 : nav == "Nav18" ? 3 : nav == "Nav19" ? 4 : 0;
	                if (bed > 0 && EC) { EC.GrandpaySleep(bed); return; }
	                IsWalking = false; IsIdle = true;
	            }
	        }

	        if (!gameObject.activeInHierarchy) return;
	        if (!PepperedEnemy)
	        {
	            if (Agent.velocity.sqrMagnitude >= 1e-10f)
	            {
	                if (ProjectileBusy) LookAtThings(Player.gameObject, 15);
	                else PlayClip(WalkAnim, IsWalking && !PlacingTrap ? Walk_Anim : Run_Anim);
	            }
	            else if (!false)
	            {
	                if (IsSearching) PlayClip(Search);
	                else if (!ProjectileBusy) PlayClip(Idle);
	                if (Sdeath && Sdeath.GrandpaPresence && SaunaPoint) LookAtThings(SaunaPoint.gameObject, 15);
	                else if (IsChasing || ProjectileBusy) UpdateCaughtLook();
	            }
	        }
	        if (Vector3.Distance(transform.position, Player.position) <= DistanceJumpscare && IsChasing
	            && !AllyCaught && !ProjectileBusy && !(WJ && WJ.IsJumping) && !playerFalling
	            && !PlayerStatus.HidingUnderThings && EnemyVision && EnemyVision.Seeing) CatchPlayer();
	    }
	    if (CaughtPlayer) UpdateCaughtLook();

	    UpdateWeaponColliders();
	}
	private void PlayClip(AnimationClip clip, float speed = 1)
	{
	    if (!Anim || !clip || Anim[clip.name] == null) return;
	    Anim[clip.name].speed = speed; Anim.CrossFade(clip.name);
	}
	private void UpdateSauna()
	{
	    if (!IsInSauna || !Sdeath || !Sdeath.Sauna || !Sdeath.Sauna.PutHandle) { TimerInSauna = 0; return; }
	    TimerInSauna += Time.deltaTime;
	    if (Sdeath.GrandpaPresence)
	    {
	        if (!IsChasing) ChaseAction();
	        TimerInDangerSauna += Time.deltaTime;
	        if (TimerInDangerSauna >= 13.5f && EC) { EC.GrandpaDeath(false); return; }
	    }
	    if (TimerInSauna >= TimerSaunaMax && SaunaPoint2)
	    { CurrentTarget = SaunaPoint2.position; if (AgentReady) Agent.SetDestination(CurrentTarget); }
	}
	private void CheckDoors()
	{
	    RaycastHit hit;
	    // Restoration guard (EnemyDoorRay): also looks along the steering direction and from 0.75 m back.
	    if (!EnemyDoorRay.Cast(OpenDoorRay, Agent, DoorDistance, LayerEx, out hit))
	    { TimerDoorOpening = LockedDoorAttempts = 0; IsOpeningDoor = false; Agent.isStopped = false; return; }
	    string door = hit.collider.gameObject.name;
	    // Restoration guard: a door mid-swing is named "ProcessState" and is otherwise ignored, so the enemy kept walking
	    // into it for the whole close animation. Wait in place instead; the open timer starts once it is fully shut.
	    if (door == "ProcessState" && hit.collider.GetComponent<AnimatedStuff>()) { IsOpeningDoor = true; return; }
	    bool closet = door == "GarderobDoor" || door == "MetalLockerDoor";
	    bool open = door == "Innerdoor" || door == "SmallDoor" || door == "MeatDoor" || door == "OldHouseWoodenDoor" || door == "OuthouseDoor" || door == "StealDoor" || (closet && CanOpenClosets);
	    bool close = door == "Motorhuv2" || door == "ClosetDoorR2" || door == "ClosetDoorL2";
	    if (open || close)
	    {
	        IsOpeningDoor = true; TimerMaxDoor = close ? .1f : closet ? 1 : 2; LockedDoorAttempts = 0;
	        TimerDoorOpening += Time.deltaTime;
	        if (closet) Agent.isStopped = true;
	        if (TimerDoorOpening >= TimerMaxDoor)
	        {
	            var animated = hit.collider.GetComponent<AnimatedStuff>();
	            if (animated) { if (close) animated.CloseAction(); else animated.OpenAction(); }
	            TimerDoorOpening = 0;
	        }
	        return;
	    }
	    if (door == "BastuDoor")
	    {
	        var sauna = hit.collider.GetComponent<Sauna>();
	        if (sauna && sauna.Closed && !sauna.PutHandle)
	        {
	            IsOpeningDoor = true; TimerMaxDoor = 2; LockedDoorAttempts = 0; TimerDoorOpening += Time.deltaTime;
	            if (TimerDoorOpening >= 2) { sauna.OpenDoor(); TimerDoorOpening = 0; }
	            return;
	        }
	        if (sauna && sauna.PutHandle && IsInSauna && TimerInSauna >= TimerSaunaMax)
	        {
	            IsOpeningDoor = true; TimerMaxDoor = 2; TimerDoorOpening += Time.deltaTime;
	            if (DoorAttempt(.4f))
	            {
	                var anim = hit.collider.GetComponent<Animation>(); if (anim) anim.Play("InnerdoorLocked_0");
	                var audio = hit.collider.GetComponentInParent<AudioSource>(); if (audio) audio.PlayOneShot(sauna.DoorLocked);
	            }
	            else if (TimerDoorOpening > 2 && LockedDoorAttempts == 3) { sauna.RemoveHandle(); LockedDoorAttempts = 0; }
	            return;
	        }
	    }
	    if (door == "prisonDoor")
	    {
	        var prison = hit.collider.GetComponent<PrisondoorGranny>();
	        if (prison && prison.PCT && prison.PCT.Locked && !prison.PCT.StoppedStuff)
	        {
	            IsOpeningDoor = true; TimerMaxDoor = 2; TimerDoorOpening += Time.deltaTime;
	            if (DoorAttempt(.4f)) prison.GrannyAttempts();
	            else if (TimerDoorOpening > 2 && LockedDoorAttempts == 3) { prison.GrannySmackDoor(); LockedDoorAttempts = 0; }
	            Agent.isStopped = false; return;
	        }
	    }
	    if (door == "InnerdoorVind" || door == "Backdoor")
	    {
	        IsOpeningDoor = true; TimerMaxDoor = 2; TimerDoorOpening += Time.deltaTime;
	        var attic = hit.collider.GetComponent<AtticDoorManage>();
	        var back = hit.collider.GetComponent<BackdoorManage>();
	        if (DoorAttempt(.8f)) { if (attic) attic.LockedAttempt(); if (back) back.LockedAttempt(); }
	        else if (TimerDoorOpening > 4.3f && LockedDoorAttempts == 3)
	        { LockedDoorAttempts = 0; if (attic) attic.OpenDoor(); if (back) back.OpenDoor(); }
	        Agent.isStopped = true; return;
	    }
	    Agent.isStopped = closet && IsChasing; IsOpeningDoor = false; TimerDoorOpening = 0;
	}
	private bool DoorAttempt(float interval)
	{
	    if (LockedDoorAttempts < 3 && TimerDoorOpening > interval * (LockedDoorAttempts + 1))
	    { LockedDoorAttempts++; return true; }
	    return false;
	}
	private void CheckHidingPlayer()
	{
	    if (PlayerStatus.InCoffinAttic && PlayerStatus.CoffinHide1) PlayerStatus.CoffinHide1.GetOutM();
	    else if (PlayerStatus.InCoffinBackyard && PlayerStatus.CoffinHide2) PlayerStatus.CoffinHide2.GetOutM();
	    else if (PlayerStatus.InCar && PlayerStatus.CarHide && PlayerStatus.CarHide.CarShit && !PlayerStatus.CarHide.CarShit.CarMoving) PlayerStatus.CarHide.GetOutM();
	    else if (PlayerStatus.InBed1 || PlayerStatus.InBed2 || PlayerStatus.InBed3)
	    {
	        TimerBedCaught += Time.deltaTime;
	        if (TimerBedCaught >= MaxBedCaughtTimer && !AllyCaught)
	        {
	            IsChasing = false; TimerBedCaught = 0; Agent.baseOffset = 0; ClearLookAndChase();
	            PlayerStatus.GrandpaCaughtYouBed(); if (JumpscareBed) JumpscareBed.Play();
	            if (Anim && UnderBed) Anim.Play(UnderBed.name);
	            CaughtPlayer = true; if (Ally) Ally.StopAI();
	        }
	    }
	}
	private void UpdateCaughtLook()
	{
	    Transform target = PlayerStatus.InBed1 ? BedCam1 : PlayerStatus.InBed2 ? BedCam2 : PlayerStatus.InBed3 ? BedCam3 : Player;
	    if (target) LookAtThings(target.gameObject, 15);
	}
	private void ClearLookAndChase()
	{
	    if (BC) BC.enabled = false; if (MusicBG) MusicBG.GrannyHunting = false;
	    if (ScreenEffects) ScreenEffects.NormalSee = false;
	}
	private void HaltEnemy()
	{
	    MurderNoiseObjs(); TimerNearNoise = 0; ClearLookAndChase();
	    IsChasing = IsSearching = false; CurrentTarget = transform.position;
	    IsShooting = false; 
	}
	public void DeadDisable()
	{
	    HaltEnemy(); if (EnemyVision) EnemyVision.IsChasing = false;
	    if (Agent) Agent.enabled = false; PepperedEnemy = false; IsDying = true;
	}
	public void StopAI()
	{
	    HaltEnemy(); if (Agent) Agent.enabled = false; PlayClip(Idle);
	    if (EnemyVision) EnemyVision.enabled = false; CaughtPlayer = true; IsDying = false;
	}
	public void ResetAIDecision()
	{
	    MurderNoiseObjs(); if (Agent) Agent.enabled = true; TimerNearNoise = 0; PlayClip(Idle);
	    if (EnemyVision) EnemyVision.enabled = true; CaughtPlayer = IsDying = false;
	}
	public void ShootDart()
	{
	    HaltEnemy(); if (Agent) Agent.enabled = false; IsDying = true; PlayClip(Tranquilized); StartCoroutine(DartProcess());
	}
	public void FrozenEnemy()
	{
	    HaltEnemy(); if (Agent) Agent.enabled = false; IsDying = true; PlayClip(Frozen); StartCoroutine(FreezeProcess());
	}
	public void PepperHit()
	{
	    IsBlind = PepperedEnemy = true; HaltEnemy(); ResetCertainTimer();
	    if (EnemyVision) { EnemyVision.IsChasing = false; EnemyVision.TimerStopVision = 0; }
	    IsWalking = IsIdle = false; if (Agent) Agent.speed = 0;
	    BlindTimer = 0; IsDying = false; PlayClip(Pepper); StartCoroutine(PepperedTimer());
	}
	private IEnumerator PepperedTimer()
	{
	    yield return new WaitForSeconds(2.5f);
	    if (PlayerStatus && !PlayerStatus.Killed && !PlayerStatus.IsJumpscared && !IsDying)
	    { MurderNoiseObjs(); ChooseWaypoint(); BlindTimer = 0; PepperedEnemy = IsFollowingSound = IsSearching = false; }
	}
	private IEnumerator DartProcess() { yield return new WaitForSeconds(1.7f); if (EC) EC.GrandpaDeath(false); }
	private IEnumerator FreezeProcess() { yield return new WaitForSeconds(2.5f); if (EC) EC.GrandpaFrozeDeath(); }
	private IEnumerator ShotgunFinishShot()
	{
	    if (Anim && Anim["shootGun"] != null)
	        yield return new WaitForSeconds(Anim["shootGun"].length / Mathf.Max(.0001f, Anim["shootGun"].speed));
	    IsShooting = false;
	}
	public void SensorPlayer()
	{
	    if (CaughtPlayer || PepperedEnemy || AllyCaught || IsDying || !PlayerTouchedRay || !PlayerStatus || !Player || (WJ && WJ.IsJumping)) return;
	    var falling = Player.GetComponent<FallingHolder>();
	    if ((falling && (falling.isLanding || falling.isFalling)) || PlayerStatus.HidingUnderThings) return;
	    CatchPlayer();
	}
	private void CatchPlayer()
	{
	    if (CaughtPlayer) return;
	    if (Agent) { Agent.speed = 0; Agent.enabled = false; }
	    CaughtPlayer = true; PlayerStatus.GrandpaCaughtYou(); MurderNoiseObjs(); ClearLookAndChase();
	    if (PlayerPrefs.GetInt("Textures") == 1 && JumpscareHitNM) JumpscareHitNM.Play();
	    if (Ally && Ally.gameObject.activeSelf) Ally.StopAI();
	    PlayClip(JumpscarePlayer); StartCoroutine(SmackTimer()); 
	}
	public void ChaseAction() { IsFollowingSound = false; IsChasing = true; IsSearching = false; ResetCertainTimer(); }
	public void StopChase()
	{
	    MurderNoiseObjs(); if (EnemyVision) EnemyVision.IsChasing = false;
	    TimerNearNoise = 0; ClearLookAndChase(); IsFollowingSound = IsChasing = false; IsSearching = true; CurrentTarget = transform.position;
	}
	public void LookAtThings(GameObject ThatThing, float RotateSpeed)
	{
	    if (!ThatThing) return;
	    Vector3 delta = ThatThing.transform.position - transform.position; delta.y = 0;
	    if (delta.sqrMagnitude > .000001f) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(delta), RotateSpeed * Time.deltaTime);
	}
	public void SpeakEnemy()
	{
	    if (GrannyVoiceSource && VoiceLinesEnemy != null && VoiceLinesEnemy.Length > 0)
	        GrannyVoiceSource.PlayOneShot(VoiceLinesEnemy[UnityEngine.Random.Range(0, VoiceLinesEnemy.Length)]);
	}
	public void MurderNoiseObjs()
	{
	    if (string.IsNullOrEmpty(NoiseObjectTag)) return;
	    foreach (var noise in GameObject.FindGameObjectsWithTag(NoiseObjectTag)) if (noise != gameObject) Destroy(noise);
	}
	public void ResetCertainTimer() { TimerChoosing = TimerBedCaught = 0; }
	public void ChooseWaypoint()
	{
	    if (Waypoints == null || Waypoints.Count == 0) return;
	    do { WaypointNumb = UnityEngine.Random.Range(0, Waypoints.Count); }
	    while (Waypoints.Count > 1 && WaypointNumb == CurrentWaypoint);
	    CurrentWaypoint = WaypointNumb;
	    if (Waypoints[CurrentWaypoint])
	    { CurrentTarget = Waypoints[CurrentWaypoint].position; IsSearching = false; IsWalking = true; IsIdle = false; }
	}
	private IEnumerator SmackTimer()
	{
	    yield return new WaitForSeconds(.72f);
	    if (HitSound) HitSound.Play(); if (PlayerStatus) PlayerStatus.KnockDeath();
	    yield return new WaitForSeconds(.3f);
	}
	private void OnTriggerStay(Collider other)
	{
	    if (other.CompareTag("Player")) PlayerClose = true;
	    if (other.CompareTag("Shotgun2") && !IsDying) { if (Shotgun) Shotgun.SetActive(true); Destroy(other.gameObject); }
	}
	private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) PlayerClose = false; }
	private void UpdateWeaponColliders() { SetWeaponCollider(Shotgun); }
	private void SetWeaponCollider(GameObject weapon)
	{
	    if (!weapon) return;
	    bool dropped = PepperedEnemy || IsDying;
	    var collider = weapon.GetComponent<Collider>(); if (collider) collider.enabled = dropped;
	    weapon.layer = LayerMask.NameToLayer(dropped ? "Item" : "GrannyParent");
	}
	public void SetNewBoneControllerStare(Transform t)
	{
	    if (!BC) return;
	    var look = BC.GetMotor<com.ootii.Actors.BoneControllers.LookAtMotor>();
	    if (look != null) look.TargetTransform = t; BoneLookFallback.SetTarget(BC, t);
	}
	public AI_Grandpa()
	{
	    DistanceJumpscare = 1.5f; Walk_Speed = 3; Run_Speed = 5; Walk_Anim = 1.1f; Run_Anim = 1.2f;
	    TimerSaunaMax = 20; TimerMaxShoot = 3; TimerMaxNoise = .1f; TimerMaxNewNav = MaxSearch = 5;
	    DistanceFromNav = 2; DistanceFromPlayer = 1.5f; DoorDistance = 4; TimerMaxDoor = 2;
	    NoiseObjectTag = "noiseobject"; TrapPregnant = true; TimerMaxTrap = 2; TimerAfterSearchTrapMax = 6;
	    MaxTraps = 5; DistanceRayPL = 6; MaxBedCaughtTimer = 2.5f;
	}
}
