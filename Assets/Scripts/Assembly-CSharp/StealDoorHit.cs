using UnityEngine;

public class StealDoorHit : MonoBehaviour
{
	public AudioClip Unlocked;

	public GameObject Target;

	public GameObject Handle;

	public bool Hit;

	public GameObject StealDoor;

	private void OnTriggerEnter(Collider other)
	{
	}

	public void OpenIt()
	{
	}
}
