using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public Animation ElevatorAnim;

	public Animation ElevatorDoor;

	public AudioSource Audio;

	public AudioSource HissSound;

	public AudioClip DoorsOpen;

	public AudioClip DoorsClose;

	public bool ClosedLift;

	public bool IsUp;

	public bool CanLift;

	public bool HalloweenThemed;

	public AudioClip[] HalloweenThemesElevator;

	public void AttemptUseElevator()
	{
	}

	public void DownNow()
	{
	}

	private IEnumerator ElevatorUp()
	{
		return null;
	}

	private IEnumerator ElevatorDown()
	{
		return null;
	}

	public void OpenElevator()
	{
	}

	public void CloseElevator()
	{
	}
}
