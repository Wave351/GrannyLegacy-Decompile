using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class CribTrigger : MonoBehaviour
{
	public Material TeddyM;
	public Material SlendrinaBodyM;
	public Material SlendrinaHairM;
	public Color StartSlendrinaBody;
	public Color StartSlendrinaHair;
	public Color StartTeddy;
	public GameObject Slendrina;
	public GameObject GlowEffect;
	public bool TeddyFade;
	public float SpeedFadeSlendrina;
	public float SpeedFadeSlendrinaH;
	public bool SlendrinaFades;
	public ParticleSystem SlendrinaEvaporate;
	public ParticleSystem Smoke;
	public GameObject TeddyObj;
	public GameObject Granny;
	public GameObject Grandpa;
	public Transform Pos1;
	public Transform Pos2;
	public Transform PlayerP;
	public Image WhiteScreen;
	public float AlphaWhite;
	public bool FadeWhiteScreenIn;
	public Animation SlendrinaAnim;
	public GameObject ScreamAudio;
	public GameObject EnemyHouse;
	public GameObject ItemDropPoint;
	public Animation ShakeAnim;
	public Renderer[] RS;
	public bool FadeTheHats;
	public float FadeHatSpeed;
	private Material[] mats;
	private float alpha;
	public bool HatAvailable;
}
