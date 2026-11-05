using System.Collections;
using UnityEngine;

public partial class RobotShoot : MonoBehaviour
{
	public GameObject Smoke;
	public GameObject SpawnImpact;
	public AudioClip ImpactS;
	public AudioClip ShootS;
	public Transform Player;
	public Transform Rotater;
	public Transform Spawner;
	public Transform Spawner2;
	public float waiting;
	public float MaxWait;
	public float Distance;
	public Transform Target;
	public bool Shooting;
	public float ShootSpeed;
	public AudioSource Shot;
	public EnemyController EC;
	public PlayerStatus Status;
	public GameObject Impactus;
	public GameObject BloodEffect;
	public WindowJumping WJ;
	public LayerMask Masks;
	public AI_RobotEnemy AI;
}
