using System.Collections;
using UnityEngine;

public partial class Elevator : MonoBehaviour
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
}
