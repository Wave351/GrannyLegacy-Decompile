using UnityEngine;

public partial class DoorRay : MonoBehaviour
{
	public float RaycastDis;
	public GameObject Ring;
	public bool buttonClicked;
	public GameObject Player;
	public PickRay PickRay;
	public PlayerStatus PlayerStatus;
	public ObjectsManager OM;
	public KeyCode InteractKey;
	public ScreenEffects ScreenEffects;
	public WindowJumping WJ;
	public GameObject NoiseGranny;
	public GameObject NoiseGrandpa;
	public GameObject MicroSparks;
	public Transform NoiseSPWMicro;
	public Transform NoiseSPLoudBell;
	public Transform NoiseSPToilet;
	public AudioSource Audio;
	public AudioClip ClosedDoor;
	public AudioClip SteelClosed;
	public AudioClip PickFlashlight;
	public Elevator Elevator;
	public GuillotineAnim GA;
	public Sauna Sauna;
	public PrisonCaptureTrigger PrisonCaptureTrigger;
	public GameObject Bomb1;
	public GameObject Bomb2;
	public FlashLightManager FLM;
}
