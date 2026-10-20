using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using com.ootii.Actors.BoneControllers;

public class AI_Granny : MonoBehaviour
{
	public AI_Grandpa Ally;

	public Animation Anim;

	public AnimationClip Idle;

	public AnimationClip WalkAnim;

	public AnimationClip JumpscarePlayer;

	public AnimationClip UnderBed;

	public AnimationClip Tranquilized;

	public AnimationClip Search;

	public AnimationClip Pepper;

	public AnimationClip Frozen;

	public AudioSource JumpscareHit;

	public AudioSource JumpscareHitNM;

	public AudioSource JumpscareBed;

	public AudioSource HitSound;

	public float DistanceJumpscare;

	public float Walk_Speed;

	public float Run_Speed;

	public float Walk_Anim;

	public float Run_Anim;

	public NavMeshAgent Agent;

	public Vector3 CurrentTarget;

	public int CurrentWaypoint;

	public List<Transform> Waypoints;

	public int WaypointNumb;

	public Transform Player;

	public BoneController BC;

	public Transform PlayerHead;

	public PlayerStatus PlayerStatus;

	public WindowJumping WJ;

	public Furniture Furni;

	public bool UnSeenPlayer;

	public Eyes_Granny EnemyVision;

	public bool IsInSauna;

	public bool SaunaLocked;

	public float TimerInSauna;

	public float TimerInDangerSauna;

	public float TimerSaunaMax;

	public Transform SaunaPoint;

	public Transform SaunaPoint2;

	public SaunaDeath Sdeath;

	public bool IsDying;

	public GameObject Crossbow;

	public GameObject Spray;

	public GameObject Bats;

	public bool UsingWeaponProjectile;

	public float TimerShoot;

	public float TimerMaxShoot;

	public bool IsBlind;

	public float BlindTimer;

	public GameObject EyeLock;

	public bool PepperedEnemy;

	public GameObject SensorSprayKill;

	public bool CaughtPlayer;

	public bool IsOpeningDoor;

	public bool IsSearching;

	public bool IsAngry;

	public bool IsFollowingSound;

	public bool IsChasing;

	public bool IsWalking;

	public bool IsIdle;

	public float TimerNearNoise;

	public float TimerMaxNoise;

	public float TimerMaxNewNav;

	public float TimerChoosing;

	public float SearchTimer;

	public float MaxSearch;

	public AudioSource GrannyVoiceSource;

	public AudioClip[] VoiceLinesEnemy;

	public float DistanceFromNav;

	public float DistanceFromPlayer;

	public bool PlayerClose;

	public float DoorDistance;

	public float TimerDoorOpening;

	public float TimerMaxDoor;

	public bool CanOpenClosets;

	public float LockedDoorAttempts;

	public AudioClip LockedDoor;

	public TeddyHunt TH;

	public GameObject NoiseObj;

	public Transform OpenDoorRay;

	public string NoiseObjectTag;

	public BGManager MusicBG;

	public ScreenEffects ScreenEffects;

	public bool TrapPregnant;

	public float TimerDropTrap;

	public float TimerMaxTrap;

	public float TimerAfterSearchTrap;

	public float TimerAfterSearchTrapMax;

	public GameObject BearTrap;

	public GameObject BearTrapNM;

	public Transform PointTrap;

	public float MaxTraps;

	public GameObject[] traps;

	public bool PlacingTrap;

	public bool NeedsToBirthTrap;

	public float DistanceRayPL;

	public Transform RayCheckP;

	public bool PlayerTouchedRay;

	public LayerMask LayerExP;

	public Transform BedCam1;

	public Transform BedCam2;

	public Transform BedCam3;

	public float TimerBedCaught;

	public float MaxBedCaughtTimer;

	public Vector3 PlayerPos;

	public Transform PlayerSpot;

	public EnemyController EC;

	public Paused Pause;

	public AudioSource FootstepS;

	public LayerMask LayerEx;

	public Vector3 DefaultRaycastDoors;

	public Transform BackyardDoorTransform;

	public bool ChooseWeapon;

	public GameObject DishesDone;

	public AnimationClip WashingDishes;

	public Transform SP_Dishes;

	public float TimerWashingDishes;

	public float TimerMaxDishes;

	public bool AtDishes;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
	}

	public void DeadDisable()
	{
	}

	public void StopAI()
	{
	}

	public void ResetAIDecision()
	{
	}

	public void ShootDart()
	{
	}

	public void FrozenEnemy()
	{
	}

	public void PepperHit()
	{
	}

	private IEnumerator UsageProjectile()
	{
		return null;
	}

	private IEnumerator PepperedTimer()
	{
		return null;
	}

	private IEnumerator FreezeProcess()
	{
		return null;
	}

	private IEnumerator DartProcess()
	{
		return null;
	}

	public void SensorPlayer()
	{
	}

	public void ChaseAction()
	{
	}

	public void StopChase()
	{
	}

	public void LookAtThings(GameObject ThatThing, float RotateSpeed)
	{
	}

	public void SpeakEnemy()
	{
	}

	public void MurderNoiseObjs()
	{
	}

	public void ResetCertainTimer()
	{
	}

	public void ChooseWaypoint()
	{
	}

	private IEnumerator SmackTimer()
	{
		return null;
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}
}
