using System.Collections;
using UnityEngine;

public class ItemSetAsChild : MonoBehaviour
{
	public bool PlacedItemIn;

	public bool CodeOnly;

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
