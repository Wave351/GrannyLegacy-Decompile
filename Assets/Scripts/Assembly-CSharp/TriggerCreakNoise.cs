using System.Collections;
using UnityEngine;

public class TriggerCreakNoise : MonoBehaviour
{
	public bool CanCreak;

	public GameObject NoiseObj;

	public float Number;

	private void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator WaitForNextCreak()
	{
		return null;
	}
}
