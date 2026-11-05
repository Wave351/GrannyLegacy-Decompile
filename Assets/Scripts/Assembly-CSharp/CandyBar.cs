using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public partial class CandyBar : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animation anim;
	public bool patrolling;
	public bool ableToAttack;
	public bool loved;
	public Image heartScreen;
	public Transform rayPoint;
	public LayerMask layers;
	public Transform player;
	public bool fadeHeart;
	public float valueHeartAlpha;
	public Transform shootPoint;
	public GameObject loveLetterObj;
}
