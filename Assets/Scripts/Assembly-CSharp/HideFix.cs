using UnityEngine;

public class HideFix : MonoBehaviour
{
	public PlayerStatus PlayerStatus;

	public Transform PosFixPlayerOn;

	public AI_Granny Granny;

	public AI_Grandpa Grandpa;

	public float DistancePlayerDetected;

	public Transform PlayerTransform;

	public LayerMask RaycastMask;

	public bool isPlayerHidden;

	public bool InTR;

	public bool ForBY;

	public BGManager BGM;

	private void Update()
	{
	}

	public void EnterHideState()
	{
	}

	public void ExitHideState()
	{
	}
}
