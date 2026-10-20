using System.Collections;
using UnityEngine;

public class Freeze_Granny : MonoBehaviour
{
	public GameObject[] Parts;

	public GameObject ParentOfIt;

	public AudioClip FrostBreak;

	public bool Broke;

	public float TimerGone;

	public GameObject ImpactB;

	public MeshCollider CollTop;

	public GameObject Bat;

	public GameObject Candy;

	public GameObject Broom;

	public GameObject Arrow;

	public GameObject Spray;

	public GameObject Crossbow;

	public GameObject EC;

	public Renderer[] BatsOff;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator KillAll()
	{
		return null;
	}

	public void DestructionRagdolls()
	{
	}
}
