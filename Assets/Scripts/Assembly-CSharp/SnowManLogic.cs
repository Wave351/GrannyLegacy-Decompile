using System.Collections;
using UnityEngine;

public class SnowManLogic : MonoBehaviour
{
	public bool WithinArea;

	public AudioSource StareSound;

	public Transform SnowManHead;

	public Transform Player;

	public float RotateSpeed;

	public Coroutine DC;

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	private IEnumerator DeactivateSnowManStare()
	{
		return null;
	}
}
