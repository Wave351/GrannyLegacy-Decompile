using System.Collections;
using UnityEngine;

public class CompartmentItemSetup : MonoBehaviour
{
	public bool PlacedItemIn;

	public Transform RedirectPosition;

	public string[] SpecificTags;

	private void Start()
	{
	}

	private IEnumerator SetItOff()
	{
		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
