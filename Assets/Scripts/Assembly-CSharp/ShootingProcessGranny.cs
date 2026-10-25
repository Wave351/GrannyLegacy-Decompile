using UnityEngine;

public partial class ShootingProcessGranny : MonoBehaviour
{
	public Transform SpraySP;
	public GameObject SprayEnergy;
	public float ForceSendSprayEnergy;
	public ParticleSystem SprayEffect;
	public Transform DartSP;
	public GameObject DartShoot;
	public float ForceSend;
	public AudioSource ShootDartS;
	public GameObject Uncharged;
	public GameObject Charged;
	public GameObject DartIn;
	public AI_Granny AI;
	public Transform Player;
	public Transform PlayerLeft;
	public Transform PlayerRight;
	public Animation Anim;
}
