using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using com.ootii.Actors.BoneControllers;

public class AI_MomSpider : MonoBehaviour
{
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

	public bool RunningToPoint;

	public bool IsAngry;

	public bool IsChasing;

	public bool IsWalking;

	public bool IsIdle;

	public bool IsHit;

	public bool IsEating;

	public float TimerMaxNewNav;

	public float TimerChoosing;

	public float DistanceFromNav;

	public bool PlayerClose;

	public Transform Run1;

	public Transform Run2;

	public Enter_SC SC;

	public bool CapableOfWebs;

	public GameObject Web;

	public Transform WebSpawnP;

	public bool WebPregnant;

	public float TimerAddWeb;

	public float TimerMaxWeb;

	public Animation Anim;

	public AnimationClip WalkAnim;

	public AnimationClip Idle;

	public AnimationClip Stunned;

	public AnimationClip EatPlayer;

	public Eyes_MomSpider Eyes;

	public BoneController BC;

	public Transform PlayerHead;

	public Transform PlayerHeadHand;

	public Transform LookAtP;

	public ScreenEffects ScreenEffects;

	public BGManager BG;

	public AudioSource Steps;

	public AudioSource Breath;

	public AudioSource HitScream;

	public AudioSource MomEat;

	public Material Mom;

	public Texture MomNormal;

	public Texture MomAngry;

	public Texture MomNormalNM;

	public Texture MomAngryNM;

	public Color MomNormalEyes;

	public Color MomAngryEyes;

	public TeddyHunt TH;

	public Transform NewNav;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void MomCaughtPlayer()
	{
	}

	private IEnumerator ResetEntireThing()
	{
		return null;
	}

	public void HitMom()
	{
	}

	private IEnumerator MomHit()
	{
		return null;
	}

	public void ChaseAction()
	{
	}

	public void StopChase()
	{
	}

	public void ChooseWaypoint()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	public void AddNewNav()
	{
	}
}
