using System.Collections;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	public GameObject Trap;

	public GameObject TrapNM;

	public Transform SP1;

	public Transform SP2;

	public Transform SP3;

	public Transform SP4;

	public Transform SP5;

	public bool Enabled;

	private void OnTriggerEnter(Collider o)
	{
	}

	private IEnumerator TimerEnable()
	{
		return null;
	}
}
