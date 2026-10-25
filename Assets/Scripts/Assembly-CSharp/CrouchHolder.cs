using System.Collections;
using UnityEngine;

public partial class CrouchHolder : MonoBehaviour
{
	public CharacterController controller;
	public MobileFPS FPS2;
	public FallingHolder HolderF;
	public FootStep StepsHold;
	public KeyCode CrouchKey;
	public Transform checkGround;
	public Animation Cam;
	public Animation Anim;
	public AnimationClip ResetAnimation;
	public AudioClip C;
	public AudioClip S;
	public AudioSource Hold;
	public bool On1;
	public bool On2;
	public bool isCrouching;
	public bool IsCrouched;
	public bool Starter;
	public bool Disabled;
	public Transform UpCheck;
	public float RadiusSphere;
	public float RadiusSphere2;
	public float RadiusSphere3;
	public bool IsBelow;
	public LayerMask MasksCrouch;
	public Vector3 originalCenter;
	public float originalHeight;
	public Vector3 originalPosition;
}
