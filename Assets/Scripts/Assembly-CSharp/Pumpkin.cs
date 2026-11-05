using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class Pumpkin : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animation animPumpkin;
	public float forceLaunch;
	public float hitCount;
	public float hitsNeeded;
	public bool patrolling;
	public bool launched;
	public AudioClip fartClip;
}
