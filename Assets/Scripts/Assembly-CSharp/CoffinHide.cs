using UnityEngine;
using UnityEngine.UI;

public partial class CoffinHide : MonoBehaviour
{
	public PlayerStatus PlayerStatus;
	public float Coffin;
	public bool InCoffin;
	public bool NearCoffin;
	public Image ButtonSprite;
	public Sprite InSprite;
	public Sprite OutSprite;
	public GameObject CoffinDoor;
	public float MaxDown;
	public float MaxUp;
	public float YValue;
	public float ZValue;
	public float ValueCoffinCurrent;
	public float SpeedCoffinChange;
	public AudioSource Audio;
	public AudioClip GetIn;
	public AudioClip GetOut;
	public Transform PlayerNewPos;
	public GameObject Player;
	public GameObject OriginalCam;
	public GameObject CamInside;
	public GameObject CamHand;
	public float TimerAgain;
	public float MaxAgain;
	public FollowHand Handheld;
	public Canvas Canvas;
	public PickRay RayP;
	public PlayerStatus StatusPL;
	public Eyes_Granny GrannyEye;
	public Eyes_Grandpa GrandpaEye;
	public AI_Slendrina AISlendrina;
}
