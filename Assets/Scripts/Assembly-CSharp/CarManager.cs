using System.Collections;
using UnityEngine;

public partial class CarManager : MonoBehaviour
{
	public float WallHealth;
	public Texture2D Wall1;
	public Texture2D Wall2;
	public Texture2D Wall3;
	public Texture2D Wall1NM;
	public Texture2D Wall2NM;
	public Texture2D Wall3NM;
	public Renderer WallR;
	public GameObject CrashMotorHuv;
	public GameObject EngineStartSound;
	public GameObject EngineWave;
	public AudioSource EngineOffBacked;
	public AudioSource CarReverse;
	public AudioSource CarForward;
	public GameObject KeyInserted;
	public AudioSource FrontDamageWall;
	public GameObject MotorHuvDown;
	public GameObject CarHitTriggersKillGranny;
	public GameObject CarHitTriggersKillGranny1;
	public GameObject CarHitTriggersKillGranny2;
	public Animation PlayerHeadAnim;
	public Animation CarAnim;
	public HandlePuzzles Puzzles;
	public ParticleSystem DustEffect;
	public GameObject BrokenWallPieces;
	public GameObject EscapeMusicP1;
	public Escapes EscapeManager;
	public bool CarMoving;
	public bool Forward;
	public bool CarOn;
	public CarHide CarHide;
	public GameObject HandKeyCheck;
	public PickRay RayP;
	public bool SmashedWallFirst;
	public GameObject StartB;
	public GameObject ReverseB;
	public GameObject ForwardB;
	public GameObject CarGoes;
	public KeyCode StartKey;
	public KeyCode ReverseKey;
	public KeyCode ForwardKey;
}
