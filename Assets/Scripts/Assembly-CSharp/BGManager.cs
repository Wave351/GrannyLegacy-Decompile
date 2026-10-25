using UnityEngine;
using UnityEngine.UI;

public partial class BGManager : MonoBehaviour
{
	public GameObject MusicG;
	public Toggle Music;
	public AudioSource Attic;
	public AudioSource Outside;
	public AudioSource House;
	public AudioSource Cellar;
	public AudioSource HouseMusic;
	public AudioSource CellarMusic;
	public AudioSource ChaseNormal;
	public AudioSource ChaseMom;
	public WindowJumping WJ;
	public bool CanFade;
	public bool InAttic;
	public bool InHouse;
	public bool InOutside;
	public bool InCellar;
	public float Speed;
	public float MaxHouse;
	public float MaxAttic;
	public float MaxOutside;
	public float MaxCellar;
	public float MaxHouseMusic;
	public float MaxCellarMusic;
	public bool MomHunting;
	public bool GrannyHunting;
	public float MaxMom;
	public float MaxGranny;
	public float FadeBackBG;
	public float SpeedChaseFade;
	public AudioSource FanNoise;
}
