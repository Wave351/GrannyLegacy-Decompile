using System.Collections;
using UnityEngine;

public partial class CrowManage : MonoBehaviour
{
	public Animator NestBird;
	public GameObject BirdEat;
	public Animation Flying;
	public AudioSource Audio;
	public AudioClip Attack;
	public AudioClip Crow;
	public GameObject FlyToFood;
	public GameObject FlyToNest;
	public GameObject PutFood;
	public ScreenEffects ScreenEffects;
	public Transform SPNoise;
	public GameObject Noise;
	public GameObject ColliderPickUp;
	public GameObject BloodKill;
	public GameObject DoorBur;
	public bool Killed;
	public TeddyHunt TH;
	public EnemyController EC;
	public bool HurtTemp;
}
