using System.Collections;
using UnityEngine;

public class Ornament_Bomb : MonoBehaviour
{
	public GameObject Explosion;

	public AudioClip TimerBombAnnounce;

	public bool Started;

	private void OnCollisionEnter(Collision other)
	{
	}

	private IEnumerator TimerTick()
	{
		return null;
	}
}
