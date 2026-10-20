using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_RobotEnemy : MonoBehaviour
{
	public Camera CameraView;

	public float ViewRange;

	public GameObject Player;

	public bool IsChasing;

	public bool Seeing;

	public float TimerStopVision;

	public float MaxVisionConvinced;

	public AudioSource Talk;

	public AudioClip[] YappingClips;

	public List<Transform> Waypoints;

	public int WaypointNumb;

	public int CurrentWaypoint;

	public Vector3 CurrentTarget;

	public float SquareMagnitude;

	public PlayerStatus PlayerStatus;

	public bool PlayerSpotted;

	public GameObject NoiseObjGranny;

	public LayerMask ExLayer;

	public Transform OpenDoorRay;

	public NavMeshAgent Agent;

	public Animation Anim;

	public AnimationClip Patrol;

	public AnimationClip Shoot;

	public float TimerShoot;

	public float TimerStopShoot;

	public Transform Spawn;

	public bool Dead;

	public GameObject DeadNoise;

	public AudioClip BeepGo;

	public GameObject PL;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void KillRobot()
	{
	}

	public void ChooseWaypoint()
	{
	}
}
