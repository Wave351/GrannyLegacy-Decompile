using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pumpkin : MonoBehaviour
{
	public NavMeshAgent agent;

	public Animation animPumpkin;

	public float forceLaunch;

	public float hitCount;

	public float hitsNeeded;

	public bool patrolling;

	public bool launched;

	public AudioClip fartClip;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void RandomWaypoint()
	{
	}

	public Vector3 PointSelect(float radius)
	{
		return default(Vector3);
	}

	public void TriggerLaunch()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnCollisionEnter(Collision other)
	{
	}

	private IEnumerator RecoverAndStandUp()
	{
		return null;
	}
}
