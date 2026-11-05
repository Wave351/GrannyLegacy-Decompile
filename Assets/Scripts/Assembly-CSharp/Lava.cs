using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class Lava : MonoBehaviour
{
	public Transform lavaBody;
	public TextManager lavaText;
	public AudioSource lavaAudios;
	public PlayerStatus PS;
	public float lavaVerticalHeight;
	public float lavaPoint_1;
	public float lavaPoint_2;
	public float lavaPoint_3;
	public float lavaPoint_4;
	public float lavaPoint_5;
	public float lavaPoint_6;
	public float lavaRiseSpeed;
	public float pointsReached;
	public float riseTimer;
	public float riseTimerMax;
	public float unriseTimerMax;
	public bool risen;
	public bool didTop;
	public bool warned;
	public AudioClip burstLava;
	public AudioClip lavaComeUp;
	public AudioClip lavaMaxSound;
	public ParticleSystem particleLavaBurst;
	public Image lavaScreen;
	public bool pickedCrossBow;
	public bool pickedShotgun;
	public bool pickedPepperSpray;
	public bool pickedFreeze;
	public bool pickedDoubleShotgun;
	public GameObject crossbowHand;
	public GameObject shotgunHand;
	public GameObject pepperHand;
	public GameObject freezeHand;
	public GameObject doubleHand;
	public GameObject radioTF;
	public GameObject bedCam;
	public TMP_Text[] texts;
	public Image[] uiEl;
	public Enter_SC SC;
}
