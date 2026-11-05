using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class SnowManAI : MonoBehaviour
{
	public NavMeshAgent Agent;
	public GameObject SnowBall;
	public Transform SnowBallThrowPoint;
	public SnowManLogic SML;
	public Transform Player;
	public ScreenEffects ScreenEffects;
	public float AmountOfTimesHit;
	public bool Interested;
	public Quaternion NewRotation;
	public Transform SnowManBody;
	public float DistanceWander;
	public float CloseDis;
	public float PRCT;
	public Vector3 CurTarget;
	public bool TestingScene;
	public float TimerThrow;
	public float TimerMaxThrow;
	public GameObject NavObstacle;
	public bool ItsOff;
}
