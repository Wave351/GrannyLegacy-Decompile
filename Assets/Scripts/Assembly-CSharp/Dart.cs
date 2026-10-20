using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Dart : MonoBehaviour
{
	public bool DidTheJob;

	private bool DidBounce;

	public float BounceForce;

	public ParentConstraint PC;

	public GameObject EC;

	public GameObject SyringeEffect;

	public Transform SyringeEffectSpawn;

	public bool EnergyFilled;

	public GameObject ElectricBomb;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnCollisionEnter(Collision other)
	{
	}

	private IEnumerator TimerStop()
	{
		return null;
	}

	private IEnumerator TimerGo()
	{
		return null;
	}

	public void InitiateDrop()
	{
	}
}
