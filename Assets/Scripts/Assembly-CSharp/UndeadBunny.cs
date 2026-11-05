using UnityEngine;
using UnityEngine.AI;

public partial class UndeadBunny : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animation anim;
	public float timerIdle;
	public float timerMaxIdle;
	public bool idle;
	public GameObject egg;
	public float changeEgg;
	public AudioClip fart;
	public Vector3 scale;
}
