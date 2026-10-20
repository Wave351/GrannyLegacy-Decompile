using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
	public GameObject Player;

	public PickRay PickRay;

	public Days Days;

	public GameObject TimerUI;

	public float TimerCurrentRun;

	public TMP_Text TimerText;

	public GameObject BedAwakenBody;

	public GameObject UIMain;

	public GameObject TextUI;

	public Image BlackScreen;

	public GameObject RagdollPlayer;

	public Transform SpawnRagdollPos;

	public Transform SpawnRagdollPos2;

	public GameObject BloodSpatterHit;

	public bool CanBloodSpatter;

	public ScreenEffects ScreenEffects;

	public FlashLightManager FLM;

	public bool IsJumpscared;

	public bool Killed;

	public AudioClip SoundHurt;

	public bool LookAtSpider;

	public bool LookAtGranny;

	public bool LookAtGrandpa;

	public float LookSpeed;

	public GameObject PauseSet;

	public GameObject GrannyPLook;

	public GameObject GrandpaPLook;

	public GameObject SpiderPLook;

	public GameObject Camera;

	public GameObject CameraPivot;

	public Vector3 FinalLookDirection;

	public GameObject MomPlayer;

	public float MaxFOV;

	public float SpeedFOVFade;

	public Camera PlayerCam;

	public bool HidingFromMom;

	public bool HidingUnderThings;

	public bool HidingInClosets;

	public bool HidingFromHouseEnemy;

	public GameObject TeddyHand;

	public bool HasTeddy;

	public bool InCoffinBackyard;

	public bool InCoffinAttic;

	public bool InBed1;

	public bool InBed2;

	public bool InBed3;

	public bool InCar;

	public CoffinHide CoffinHide1;

	public CoffinHide CoffinHide2;

	public CarHide CarHide;

	public BedHide BedHide1;

	public BedHide BedHide2;

	public BedHide BedHide3;

	public Transform BedCam1;

	public Transform BedCam2;

	public Transform BedCam3;

	public GameObject Granny;

	public GameObject Grandpa;

	public TeddyHunt TH;

	public EnemyController EC;

	public GameObject E1;

	public GameObject E2;

	public Escapes Escapedo;

	public ClosetUnhid CUnh1;

	public ClosetUnhid CUnh2;

	public ClosetUnhid CUnh3;

	public ClosetUnhid CUnh4;

	public bool TimerStopped;

	public HideVision HV;

	public Animation ShakeAnim;

	public Eyes_Granny EyesGranny;

	public Eyes_Grandpa EyesGrandpa;

	public PlayerVsEnemyCollisionTrigger PVE;

	private void Update()
	{
	}

	public void PlayerGettingStopped()
	{
	}

	public void DamageFeet()
	{
	}

	public void DamageSpider()
	{
	}

	public void MomDeath()
	{
	}

	private IEnumerator MomDeathCount()
	{
		return null;
	}

	public void GrannyCaughtYou()
	{
	}

	public void GrannyCaughtYouBed()
	{
	}

	public void GrandpaCaughtYou()
	{
	}

	public void GrandpaCaughtYouBed()
	{
	}

	public void KnockDeath()
	{
	}

	public void NormalDeath()
	{
	}

	public void SpawnRagdoll()
	{
	}

	private IEnumerator DeathCount()
	{
		return null;
	}

	private IEnumerator DeathCountBed(float Timer)
	{
		return null;
	}
}
