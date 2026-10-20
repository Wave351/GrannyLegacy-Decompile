using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CandyBar : MonoBehaviour
{
	public NavMeshAgent agent;

	public Animation anim;

	public bool patrolling;

	public bool ableToAttack;

	public bool loved;

	public Image heartScreen;

	public Transform rayPoint;

	public LayerMask layers;

	public Transform player;

	public bool fadeHeart;

	public float valueHeartAlpha;

	public Transform shootPoint;

	public GameObject loveLetterObj;

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
