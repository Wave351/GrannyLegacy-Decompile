using System.Collections;
using UnityEngine;

public class TrapPoison : MonoBehaviour
{
	public bool Poisoning;

	public bool Used;

	public ParticleSystem Sys;

	public AudioSource Sus;

	public GameObject Trigger;

	private IEnumerator Waiter()
	{
		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
