using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class LeprechaunAI : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animation animLepre;
	public AudioClip huh;
	public AudioClip fuckYou;
	public AudioClip barfCoin;
	public float pointDistanceSelect;
	public bool triggered;
	public bool capableOfAngry;
	public float angryTimer;
	public GameObject coin;
	public float forceSpitOut;
	public Transform player;
}
