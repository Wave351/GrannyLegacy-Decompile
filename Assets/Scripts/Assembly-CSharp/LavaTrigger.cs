using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
	public Lava L;

	public PlayerStatus PS;

	public AudioClip lavaDieSound;

	public FallingHolder FH;

	private void OnTriggerEnter(Collider other)
	{
	}
}
