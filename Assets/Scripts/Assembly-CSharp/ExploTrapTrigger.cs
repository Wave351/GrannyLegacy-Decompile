using System.Collections;
using UnityEngine;

public class ExploTrapTrigger : MonoBehaviour
{
	public GameObject ExplosionObj;

	public AudioClip BeepWarning;

	public bool TriggeredBoom;

	public Transform PointExpl;

	private void OnTriggerEnter(Collider o)
	{
	}

	private IEnumerator TimerExplosion()
	{
		return null;
	}

	private IEnumerator WaiterBoom()
	{
		return null;
	}
}
