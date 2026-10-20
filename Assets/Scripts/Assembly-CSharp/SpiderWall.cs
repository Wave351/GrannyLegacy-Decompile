using UnityEngine;

public class SpiderWall : MonoBehaviour
{
	public Animation Anim;

	public GameObject FootStepAudio;

	public Transform[] TravelingPoints;

	public Transform CurrentPoint;

	public float WalkSpeed;

	public float RotationSpeed;

	public float IdleTimeMin;

	public float IdleTimeMax;

	public bool isWalking;

	public float idleTimer;

	public float idleTargetTime;

	public GameObject DeadBodySpider;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void IdleStatus()
	{
	}

	public void DecideOnrandomPoint()
	{
	}

	public void DeathInitiates()
	{
	}
}
