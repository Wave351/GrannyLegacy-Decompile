using System.Collections;
using UnityEngine;

public class AtticSpider : MonoBehaviour
{
	public float WalkSpeed;

	public float SpeedRotate;

	public Vector3 MeatPos;

	public Transform PlayerP;

	public PlayerStatus PlayerStatus;

	public Animator Animator;

	public GameObject Footsteps;

	public Vector3 StartPos;

	public float DistanceDet;

	public bool MeatGoes;

	public bool CloseToMeat;

	public GameObject MeatPlate;

	public float EatingTime;

	public ScreenEffects ScreenEffects;

	public AudioSource HissNoise;

	public AudioClip Aggro;

	public AudioClip Die;

	public AudioClip Sting;

	public bool IsTargetingPlayer;

	public bool IsKilling;

	public bool isEating;

	public bool KillingProcess;

	public bool Died;

	public EnemyController EC;

	public TeddyHunt TH;

	public GameObject Blood;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Dies()
	{
	}

	public void MeatIsThere()
	{
	}

	public void GetMad()
	{
	}

	public void MoveToPos(Vector3 pos)
	{
	}

	private IEnumerator KilledSpider()
	{
		return null;
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private IEnumerator StingKill()
	{
		return null;
	}

	private IEnumerator EatMeat()
	{
		return null;
	}
}
