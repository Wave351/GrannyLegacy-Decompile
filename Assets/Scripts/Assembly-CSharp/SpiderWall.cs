using UnityEngine;

public partial class SpiderWall : MonoBehaviour
{
	public Animation Anim;
	public GameObject FootStepAudio;
	public Transform[] TravelingPoints;
	public Transform CurrentPoint;
	public float WalkSpeed;
	public float RotationSpeed;
	public float IdleTimeMin;
	public float IdleTimeMax;
	public bool isWalking;
	public float idleTimer;
	public float idleTargetTime;
	public GameObject DeadBodySpider;
}
