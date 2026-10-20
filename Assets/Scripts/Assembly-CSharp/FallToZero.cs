using System.Collections;
using UnityEngine;

public class FallToZero : MonoBehaviour
{
	public FallingHolder FH;

	public FallZeroCheck FZC;

	public bool Working;

	public float DurationMaxedOut;

	private void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator TimerFunction()
	{
		return null;
	}
}
