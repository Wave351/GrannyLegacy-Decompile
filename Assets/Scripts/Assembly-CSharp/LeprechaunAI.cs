using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LeprechaunAI : MonoBehaviour
{
	public NavMeshAgent agent;

	public Animation animLepre;

	public AudioClip huh;

	public AudioClip fuckYou;

	public AudioClip barfCoin;

	public float pointDistanceSelect;

	public bool triggered;

	public bool capableOfAngry;

	public float angryTimer;

	public GameObject coin;

	public float forceSpitOut;

	public Transform player;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	public void SelectRandomPointTravel()
	{
	}

	private IEnumerator AlertedEvent()
	{
		return null;
	}
}
