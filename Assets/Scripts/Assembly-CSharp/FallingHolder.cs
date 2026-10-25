using System.Collections;
using UnityEngine;

public partial class FallingHolder : MonoBehaviour
{
	public CharacterController ch;
	public MobileFPS FPS;
	public PlayerStatus PlayerStatus;
	public PickRay Ray1;
	public DoorRay Ray2;
	public Animation playerAnimation;
	public float FallDurationHold;
	public float FallChecker;
	public float FallMega;
	public float Speed;
	public float groundCheckDis;
	public LayerMask groundLayer;
	public GameObject LandSound;
	public AudioSource Falling;
	public AudioSource LandTiny;
	public bool CanFallSound;
	public bool isFalling;
	public bool isLanding;
	public bool Fell;
	public bool Damaged;
	public bool DeathFall;
	public float fallDuration;
	public float DurateCan;
	public Transform Cam;
	public bool CanCamSmooth;
	public Transform CamPivot;
	public GameObject BloodSplashHead;
}
