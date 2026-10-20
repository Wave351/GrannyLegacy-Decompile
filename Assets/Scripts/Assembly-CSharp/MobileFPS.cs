using System.Collections;
using UnityEngine;

public class MobileFPS : MonoBehaviour
{
	public Transform playerCamera;

	public CharacterController characterController;

	public Camera playerCamera2;

	public Transform CamPivot;

	public float moveSpeed;

	public float SpeedMove;

	public float SpeedMoveCrouch;

	public float FallSpeed;

	public bool isMoving;

	private Vector3 moveDirection;

	public float cameraRotationSpeed;

	public float maxXRotation;

	public float minXRotation;

	public Animation CameraAnim;

	public bool RotateXReser;

	public float rotationX;

	public float SpeedFade;

	public bool IsCrouched;

	public bool isAllowedToMove;

	public bool AbleToMove;

	public bool CanFade;

	public bool CamK;

	public bool InWeb;

	public Transform groundCheck;

	public float groundCheckRadius;

	public LayerMask groundLayerMask;

	public FallingHolder FallingHolder;

	public PlayerStatus PS;

	public Days SystemDay;

	public AtticSpider AtticSpider;

	public GameObject PlayerStuff;

	public bool InElev;

	public float ElevatorYAxis;

	public Vector3 LastElevPos;

	public GameObject ElevatorObj;

	public bool WasOnPlatform;

	private void Start()
	{
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void GetTouchInput()
	{
	}

	private void HandlePlayerMovement(bool isGrounded)
	{
	}

	private void HandlePlayerMovementS(bool isGrounded)
	{
	}

	public void ApplyFinalMovements()
	{
	}

	public void HandleVerticalMovement()
	{
	}

	public void AdjustSpeed(float newSpeed)
	{
	}

	public void ReserRotation()
	{
	}

	public void DragHead()
	{
	}

	private IEnumerator DragHeadTimer()
	{
		return null;
	}

	public void ResetRotationSlendrina()
	{
	}

	public void ResetRotation()
	{
	}

	public void OnControllerColliderHit(ControllerColliderHit OBJ)
	{
	}
}
