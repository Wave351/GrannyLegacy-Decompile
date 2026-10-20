using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LittleFucker : MonoBehaviour
{
	public NavMeshAgent Agent;

	public AudioSource Sounds;

	public AudioClip Laughter;

	public AudioClip Aggro;

	public AudioClip Smack;

	public PickRay PickRaycastScript;

	public Transform Player;

	public float WalkSpeed;

	public float RunSpeed;

	public Vector3 DestinationTarget;

	public bool WanderingPathCheck;

	public float IdleTimer;

	public float IdleMax;

	public bool Attacking;

	public Animator Anim;

	public AudioSource FootStepAudio;

	public AudioClip[] Steps;

	public float DetectionRange;

	public float DetectionRangeNear;

	public bool Interested;

	public bool Chasing;

	public LayerMask Layers;

	public Transform PlayerHead;

	public Transform RaycastH;

	public Animation AnimCam;

	public GameObject NoiseObjGrandpa;

	public GameObject NoiseObjGranny;

	public GameObject CollisionTrapObject;

	public bool Active;

	public bool Hurt;

	public bool Unwrapped;

	public GameObject Present;

	public float TOffset;

	public float TOffsetBack;

	public float ToffsetChangeTimer;

	public float ToffsetChangeTimerBack;

	public float CrossFadeSpeed;

	public string currentAnimation;

	public string newAnimation;

	public bool NormalLogicStatus;

	public bool StareAtPlayer;

	public float StareWeight;

	public Transform HeadBone;

	public Vector3 SLP;

	public float LookSpeed;

	public float WeightTransition;

	public bool CanLook;

	public float SmW;

	public AnimationClip FallDownClip;

	private void Update()
	{
	}

	public void InitiateAI()
	{
	}

	public void FootStepPlay()
	{
	}

	public void WanderLogic()
	{
	}

	private IEnumerator AttackSequence()
	{
		return null;
	}

	private IEnumerator Rest()
	{
		return null;
	}

	public void KnockedOut()
	{
	}

	private IEnumerator KnockoutRoutine()
	{
		return null;
	}

	public void OnAnimatorIK(int layerIndex)
	{
	}
}
