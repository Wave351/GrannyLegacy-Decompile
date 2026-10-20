using System.Collections;
using UnityEngine;

public class PrisonCaptureTrigger : MonoBehaviour
{
	public GameObject PrisonDoor;

	public AudioClip DoorClose;

	public AudioClip LockedSound;

	public float Sprints;

	public bool Locked;

	public bool StoppedStuff;

	public GameObject CollFall;

	public GameObject NoiseOBJ;

	public GameObject NoiseOBJ2;

	public Renderer CamLamp;

	public AudioClip Beep;

	public float TimerBeep;

	private void Update()
	{
	}

	public void AttemptOpen()
	{
	}

	private IEnumerator ColorLamp()
	{
		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
