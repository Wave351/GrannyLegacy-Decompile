using System.Collections;
using UnityEngine;

public class ItemSetDrawer : MonoBehaviour
{
	public bool PlacedItemIn;

	public GameObject SlendrinaItemsStore;

	public GameObject Item;

	public Animation Anim;

	public AnimationClip CloseDoor;

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
