using System.Collections;
using UnityEngine;

public class Freeze_Grandpa : MonoBehaviour
{
	public GameObject[] Parts;

	public GameObject BodyOriginal;

	public GameObject ParentOfIt;

	public AudioClip FrostBreak;

	public bool Broke;

	public float TimerGone;

	public GameObject EC;

	public GameObject Cane;

	public GameObject Shotgun;

	public GameObject ImpactB;

	public GameObject Hat;

	public GameObject Head;

	public GameObject armItem;

	public GameObject Bone1;

	public GameObject Bone2;

	public MeshRenderer CaneM;

	public GameObject Wings;

	public GameObject Staff;

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
