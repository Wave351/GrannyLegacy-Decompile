using System.Collections;
using UnityEngine;

public class MelonContainItem : MonoBehaviour
{
	public bool PlacedItemIn;

	public Transform MelonDropPoint;

	public Transform CarKey;

	public Transform ECKey;

	public Transform PDKey;

	public Transform MasKey;

	public Transform Cog;

	public Transform PlayHouse;

	public Transform Remote;

	public Transform RustyKey;

	public Transform SparkPlug;

	public Transform WPKey;

	private void Start()
	{
	}

	private IEnumerator StopPlacedItemLogic()
	{
		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
