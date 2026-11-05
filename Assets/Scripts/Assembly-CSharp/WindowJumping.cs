using System.Collections;
using UnityEngine;

public partial class WindowJumping : MonoBehaviour
{
	public bool WentOutside;
	public bool IsJumping;
	public float Timer1;
	public float Timer2;
	public GameObject Player;
	public Animation CamGoOutside;
	public Animation CamGoInside;
	public GameObject Camera;
	public GameObject CamPivot;
	public Transform PosOutside;
	public Transform PosInside;
	public Animation Window;
	public BGManager BGManager;
}
