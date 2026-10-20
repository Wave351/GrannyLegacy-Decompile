using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpectralGranny : MonoBehaviour
{
	public NavMeshAgent Agent;

	public float WanderRadius;

	public float WanderSpeed;

	public float LungeSpeed;

	public float LungeRadius;

	private float LungeTimer;

	public float LungeTimerMax;

	public bool IsLunging;

	public Animation Anim;

	public float TimerScream;

	public AudioSource ScreamSpec;

	public bool CanKill;

	public float IntroMoveSpeed;

	public EnemyController EC;

	public PlayerStatus PS;

	public Transform GrannyPos;

	public GameObject GrannyP;

	private IEnumerator Start()
	{
		return null;
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	public void ChooseWaypoint()
	{
	}

	public void StartLunge()
	{
	}

	private IEnumerator SpectralNoiseLunge()
	{
		return null;
	}
}
