using UnityEngine;

public class KickHatch : MonoBehaviour
{
	public AudioClip KickSound;

	public bool Kicked;

	public bool ISYet;

	public GameObject Trigger;

	public float MoveLimit;

	public float MoveSpeed;

	private float XStartPosition;

	private Rigidbody rb;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
