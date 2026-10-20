using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RatEnemy : MonoBehaviour
{
	public NavMeshAgent Agent;

	public float WaitingTime;

	public bool WaitStatus;

	public bool HasToTravel;

	public Animator Anim;

	public float Radius;

	public float WaitingTimer;

	public AudioClip RatSound;

	public AudioClip RatDie;

	public bool CanHit;

	public GameObject NoiseOBJ;

	public ScreenEffects ScreenEffects;

	public float RotMax;

	public Transform Target;

	public LayerMask Layer;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator ResetCanHit()
	{
		return null;
	}

	public void AlignToGround()
	{
	}
}
