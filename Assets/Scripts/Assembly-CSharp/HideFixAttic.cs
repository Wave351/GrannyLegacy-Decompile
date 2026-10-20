using UnityEngine;

public class HideFixAttic : MonoBehaviour
{
	public PlayerStatus PlayerStatus;

	public Transform PosFixPlayerOn;

	public AI_Granny Granny;

	public AI_Grandpa Grandpa;

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}
}
