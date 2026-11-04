using UnityEngine;

public partial class KickHatch : MonoBehaviour
{
	public AudioClip KickSound;
	public bool Kicked;
	public bool ISYet;
	public GameObject Trigger;
	public float MoveLimit;
	public float MoveSpeed;
	private float XStartPosition;
	private Rigidbody rb;
}
