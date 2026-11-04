using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/DoorRay.c (Granny Legacy, IL2CPP x64).
public partial class DoorRay
{
	public DoorRay() { RaycastDis = 5; }
	// Plain open/close pairs: "<Name>" opens (AnimatedStuff.OpenAction), "<Name>2" closes it again.
	private static readonly HashSet<string> OpenNames = new HashSet<string> {
	    "Innerdoor", "Drawer", "GarderobDoor", "ClosetDoorR", "ClosetDoorL", "KitchenDoor", "FridgeDoor", "SmallDoor",
	    "SmallDrawer", "Motorhuv", "MetalLockerDoor", "MeatDoor", "CabinetOldHouseClosed", "OldHouseWoodenDoor",
	    "SpiderAtticDoor", "OuthouseDoor", "PlayHouseDoor", "Baklucka", "StealDoor" };
	private RaycastHit hit;
	private GameObject HitGO { get { return hit.collider.gameObject; } }
	private T Hit<T>() where T : Component { return hit.collider.GetComponent<T>(); }
	// Interaction is available: highlight the ring. Returns true when the interact key/button was pressed this frame.
	private bool Ready()
	{
	    PickRay.T_HighlightItem.SetActive(false);
	    Ring.SetActive(true);
	    if (!buttonClicked) return false;
	    buttonClicked = false;
	    return true;
	}
	private void Noise(GameObject prefab, Transform at) { Instantiate(prefab, at.position, at.rotation); }
	private void OpenWithAnim(string clip, bool sound)
	{
	    Hit<Animation>().Play(clip);
	    HitGO.name = "OpenedObject";
	    if (sound) Hit<AudioSource>().Play();
	}
	private void Update()
	{
	    if (Paused.IsPaused) return;
	    var falling = Player.GetComponent<FallingHolder>();
	    if (Input.GetKeyDown(InteractKey) && Ring.activeSelf && !Paused.IsPaused && !falling.isFalling
	        && !WJ.IsJumping && !PlayerStatus.IsJumpscared)
	        buttonClicked = true;
	    Vector3 dir = transform.TransformDirection(Vector3.forward);
	    Debug.DrawRay(transform.position, dir * RaycastDis, Color.red);
	    if (Physics.Raycast(transform.position, dir, out hit, RaycastDis) && !falling.isFalling && !falling.isLanding
	        && !WJ.IsJumping && !PlayerStatus.IsJumpscared)
	    {
	        if (Interact()) return;
	    }
	    Ring.SetActive(false);
	    buttonClicked = false;
	}
	// Returns true when the hit object is interactable (the ring stays up or the action already handled it).
	private bool Interact()
	{
	    string name = HitGO.name;
	    if (OpenNames.Contains(name)) { if (Ready()) { Hit<AnimatedStuff>().OpenAction(); Ring.SetActive(false); } return true; }
	    if (name.EndsWith("2") && OpenNames.Contains(name.Substring(0, name.Length - 1)))
	    { if (Ready()) { Hit<AnimatedStuff>().CloseAction(); Ring.SetActive(false); } return true; }
	    switch (name)
	    {
	        case "GuillotineHandle":
	            if (GA.anim.isPlaying) break;
	            if (Ready()) { GA.Start1(); Ring.SetActive(false); }
	            return true;
	        case "PlayHouseDoorLocked":
	            if (!Hit<PlayHouseDoor>().AttemptOpen) break;
	            if (Ready()) { Hit<PlayHouseDoor>().AttemptLock(); Ring.SetActive(false); }
	            return true;
	        case "Backdoor":
	            if (Hit<Animation>().isPlaying) break;
	            if (Ready()) { Hit<BackdoorManage>().LockedAttempt(); Ring.SetActive(false); }
	            return true;
	        case "InnerdoorVind":
	            if (Hit<Animation>().isPlaying) break;
	            if (Ready()) { Hit<AtticDoorManage>().LockedAttempt(); Ring.SetActive(false); }
	            return true;
	        case "SmallDoorRemote":
	            if (Hit<Animation>().isPlaying) break;
	            if (Ready()) { Hit<Animation>().Play("SmallDoorLocked"); Hit<AudioSource>().PlayOneShot(ClosedDoor); Ring.SetActive(false); }
	            return true;
	        case "StealDoorLocked":
	            if (Hit<Animation>().isPlaying) break;
	            if (Ready()) { Hit<Animation>().Play("InnerdoorLocked_3"); Hit<AudioSource>().PlayOneShot(SteelClosed); Ring.SetActive(false); }
	            return true;
	        case "CarDoorInteract":
	            if (Ready())
	            {
	                Hit<Rigidbody>().isKinematic = false;
	                Hit<AudioSource>().Play();
	                Hit<MeshCollider>().convex = true;
	                HitGO.name = "Interacted";
	                HitGO.layer = LayerMask.NameToLayer("Door");
	                Ring.SetActive(false);
	            }
	            return true;
	        case "LockerDoorSpiderCellar":
	            if (Ready()) { OpenWithAnim("spiderCellarLuckaOpen", true); Ring.SetActive(false); }
	            return true;
	        case "GloveCompartment":
	            if (Ready()) { OpenWithAnim("handskfackOpen", true); Ring.SetActive(false); }
	            return true;
	        case "ButtonSecretDoor":
	            PickRay.T_HighlightItem.SetActive(false);
	            Ring.SetActive(true);
	            if (!buttonClicked) return true;
	            GameObject.Find("TriggerPushDoor").GetComponent<PushSecretDoorTrigger>().ClickButton();
	            buttonClicked = false;
	            OpenWithAnim("secretDoorButton", false);
	            Ring.SetActive(false);
	            return true;
	        case "TunnelLock":
	            if (Ready()) { OpenWithAnim("TunnelLock", true); Ring.SetActive(false); }
	            return true;
	        case "GridBackyardMove":
	            if (Ready()) { OpenWithAnim("GallerHole", true); Ring.SetActive(false); }
	            return true;
	        case "NestDoor":
	            if (Ready()) { OpenWithAnim("BurDoorOpen", true); Ring.SetActive(false); }
	            return true;
	        case "MicroDoor":
	            if (Ready())
	            {
	                Hit<Animation>().Play("MicroDoorOpen");
	                MicroSparks.SetActive(true);
	                Noise(NoiseGranny, NoiseSPWMicro);
	                HitGO.name = "OpenedObject";
	                Ring.SetActive(false);
	            }
	            return true;
	        case "FanCollider":
	            if (!Hit<FanCollider>().Interactable) break;
	            if (Ready()) { Hit<FanCollider>().Hurt(); Ring.SetActive(false); }
	            return true;
	        case "ColliderBirdNest":
	            if (ScreenEffects.Hurt) break;
	            if (Ready()) { hit.collider.GetComponentInParent<CrowManage>().AttemptSteal(); Ring.SetActive(false); }
	            return true;
	        case "Window":
	        {
	            var crouch = Player.GetComponent<CrouchHolder>();
	            if (crouch.Anim.isPlaying || crouch.IsCrouched) break;
	            if (Ready()) { if (WJ.WentOutside) WJ.JumpInside(); else WJ.JumpOutside(); Ring.SetActive(false); }
	            return true;
	        }
	        case "ElevatorButtonCall":
	            if (Elevator.ElevatorAnim.isPlaying || Elevator.ElevatorDoor.isPlaying) break;
	            if (Ready()) { Hit<Animation>().Play("HissKnappPress"); Elevator.AttemptUseElevator(); }
	            return true;
	        case "ElevatorButtonCallDown":
	            if (Elevator.ElevatorAnim.isPlaying || Elevator.ElevatorDoor.isPlaying || !Elevator.IsUp) break;
	            if (Ready()) { Hit<Animation>().Play("HissKnappPress"); Elevator.DownNow(); }
	            return true;
	        case "ElevatorDoor1":
	            if (Elevator.ElevatorAnim.isPlaying || Elevator.ElevatorDoor.isPlaying) break;
	            if (Ready()) { if (!Elevator.ClosedLift) Elevator.CloseElevator(); else Elevator.OpenElevator(); Ring.SetActive(false); }
	            return true;
	        // Coffin locks: each reveals a bomb unless it is the coffin holding the chosen item (NumberChoosen 1 / 2).
	        case "1_CoffinLockSpiderCellar1":
	        case "1_CoffinLockSpiderCellar2":
	        {
	            bool first = name.EndsWith("1");
	            if (Ready())
	            {
	                Hit<Animation>().Play(first ? "CoffinLock1Open" : "CoffinLock2Open");
	                HitGO.name = "CoffinOpened";
	                Hit<AudioSource>().Play();
	                if (OM.NumberChoosen != (first ? 1 : 2)) (first ? Bomb1 : Bomb2).SetActive(true);
	            }
	            return true;
	        }
	        case "BastuDoor":
	            if (Sauna.Door.GetComponent<Animation>().isPlaying || Sauna.Handle.GetComponent<Animation>().isPlaying) break;
	            if (Ready()) { Sauna.OpenDoor(); Ring.SetActive(false); }
	            return true;
	        case "BastuBom":
	            if (Sauna.Door.GetComponent<Animation>().isPlaying || Sauna.Handle.GetComponent<Animation>().isPlaying || !Sauna.Closed) break;
	            if (Ready()) { Sauna.HandleModify(); Ring.SetActive(false); }
	            return true;
	        case "BastuSpak":
	            if (Sauna.Switch.GetComponent<Animation>().isPlaying) break;
	            if (Ready()) { Sauna.ModifySwitch(); Ring.SetActive(false); }
	            return true;
	        case "prisonDoor":
	            if (PrisonCaptureTrigger.PrisonDoor.GetComponent<Animation>().isPlaying || !PrisonCaptureTrigger.Locked || PrisonCaptureTrigger.StoppedStuff) break;
	            if (Ready()) { PrisonCaptureTrigger.AttemptOpen(); Ring.SetActive(false); }
	            return true;
	        case "Sprint1":
	            if (!PrisonCaptureTrigger.Locked) break;
	            if (Ready()) { Hit<Animation>().Play("sprintAnim"); HitGO.name = "InteractedSprint"; Ring.SetActive(false); }
	            return true;
	        case "PresentOpenable":
	            if (Ready()) { Hit<OpenPresent>().RandomGift(); Ring.SetActive(false); }
	            return true;
	        case "EggEasterInteractable":
	            if (Ready()) { Hit<EasterEgg>().StartEggBumping(); Ring.SetActive(false); }
	            return true;
	    }
	    var present = HitGO.GetComponent<Present>();
	    if (present && !present.Fading)
	    {
	        if (Ready()) { Hit<Present>().UnwrapGift(); Ring.SetActive(false); }
	        return true;
	    }
	    var monitor = HitGO.GetComponent<MonitorSubBasement>();
	    if (monitor && !monitor.Audio.isPlaying)
	    {
	        if (Ready()) { Hit<MonitorSubBasement>().HitSwitch(); Ring.SetActive(false); }
	        return true;
	    }
	    if (name == "BellNoisyInteractive" && !HitGO.GetComponent<AudioSource>().isPlaying)
	    {
	        if (Ready())
	        {
	            Hit<Animation>().Play("Klocka");
	            Hit<Animation>()["Klocka"].speed = .85f;
	            HitGO.GetComponent<AudioSource>().Play();
	            Noise(NoiseGrandpa, NoiseSPLoudBell); Noise(NoiseGranny, NoiseSPLoudBell);
	            Ring.SetActive(false);
	        }
	        return true;
	    }
	    if (name == "ToiletNoisyFlushCollider" && !HitGO.GetComponent<AudioSource>().isPlaying)
	    {
	        if (Ready())
	        {
	            HitGO.GetComponent<AudioSource>().Play();
	            Noise(NoiseGrandpa, NoiseSPToilet); Noise(NoiseGranny, NoiseSPToilet);
	            Ring.SetActive(false);
	        }
	        return true;
	    }
	    if (name == "FlashLightOnTable")
	    {
	        if (Ready())
	        {
	            Ring.SetActive(false);
	            Audio.PlayOneShot(PickFlashlight);
	            HitGO.SetActive(false);
	            FLM.InHandFlashLight.SetActive(true);
	        }
	        return true;
	    }
	    return false;
	}
	private void HandleButtonClick() { buttonClicked = true; }
}
