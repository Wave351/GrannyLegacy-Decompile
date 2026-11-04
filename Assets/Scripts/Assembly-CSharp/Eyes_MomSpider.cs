using UnityEngine;

public partial class Eyes_MomSpider : MonoBehaviour
{
	public Camera CameraView;
	public float ViewRange;
	public GameObject Player;
	public bool IsChasing;
	public bool Seeing;
	public float TimerStopVision;
	public float MaxVisionConvinced;
	public float SquareMagnitude;
	public GameObject ScreamSee;
	public PlayerStatus PlayerStatus;
	public AI_MomSpider AI;
	public LayerMask LayerIgnore;
}
