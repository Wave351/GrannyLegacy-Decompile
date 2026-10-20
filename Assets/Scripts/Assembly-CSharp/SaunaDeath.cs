using UnityEngine;

public class SaunaDeath : MonoBehaviour
{
	public AI_Granny Granny;

	public AI_Grandpa Grandpa;

	public bool GrannyIn;

	public bool GrandpaIn;

	public bool GrannyPresence;

	public bool GrandpaPresence;

	public Sauna Sauna;

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}
}
