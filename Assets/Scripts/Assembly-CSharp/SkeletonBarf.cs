using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkeletonBarf : MonoBehaviour
{
	public NavMeshAgent agent;

	public Animation anim;

	public bool patrolling;

	public bool barfing;

	public AudioClip barfClip;

	public AudioClip splashBarf;

	public Image barfScreen;

	public ParticleSystem barfEffect;

	public Transform rayPoint;

	public LayerMask layers;

	public Transform player;

	public Collider collider;

	public bool fadeBarf;

	public float valueBarfAlpha;

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

	private IEnumerator BarfEffectStuff()
	{
		return null;
	}

	private IEnumerator TimerBackIdle()
	{
		return null;
	}

	public void BarfScreenOccur()
	{
	}

	private IEnumerator TimerBarfScreen()
	{
		return null;
	}
}
