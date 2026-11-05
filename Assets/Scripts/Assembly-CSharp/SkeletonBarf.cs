using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public partial class SkeletonBarf : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animation anim;
	public bool patrolling;
	public bool barfing;
	public AudioClip barfClip;
	public AudioClip splashBarf;
	public Image barfScreen;
	public ParticleSystem barfEffect;
	public Transform rayPoint;
	public LayerMask layers;
	public Transform player;
	public Collider collider;
	public bool fadeBarf;
	public float valueBarfAlpha;
}
