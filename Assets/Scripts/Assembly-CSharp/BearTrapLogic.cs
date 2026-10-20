using System.Collections;
using UnityEngine;

public class BearTrapLogic : MonoBehaviour
{
	public AudioClip PlacedTrap;

	public AudioClip AggroSound;

	public bool Placed;

	public bool TrappedPlayer;

	public Animation Anim;

	public AnimationClip Clip;

	public GameObject RayB;

	public GameObject ScEFF;

	public GameObject Noise;

	public GameObject Player;

	public float RayDistance;

	public bool PlayerHit;

	public Transform RayCaster;

	public bool ItemCaughtIt;

	public float removeSpeed;

	public bool triggerableByItem;

	public Collider interactCollider;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator TimerDes()
	{
		return null;
	}

	private IEnumerator TimerDesItem()
	{
		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	public void ShootGun()
	{
	}
}
