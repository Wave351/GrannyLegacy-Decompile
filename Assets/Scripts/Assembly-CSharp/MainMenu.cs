using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Toggle ExtraLocks;

	public Toggle Vsync;

	public Toggle NM;

	public Toggle Music;

	public Toggle Dark;

	public Toggle Granny;

	public Toggle Spider;

	public Toggle Extras;

	public Toggle ImageEff;

	public Toggle Trap;

	public Toggle ExTraps;

	public Toggle Flash;

	public Toggle Robot;

	public Toggle SP;

	public Toggle Smooth;

	public Toggle Lights;

	public Toggle Slendrina;

	public Toggle Limping;

	public Toggle FlashLight;

	public Toggle Glitches;

	public Toggle MotionBlur;

	public Toggle DepthOfField;

	public Toggle ChaseEffect;

	public Toggle AmbientOcclusion;

	public Toggle Bloom;

	public Toggle Occlus;

	public Slider GrannyWP;

	public Slider VersionControl;

	public Slider Grandpa;

	public Slider Quality;

	public Slider Difficulty;

	public Slider CustomChase;

	public Slider Volume;

	public Slider FOV;

	public Slider PresetS;

	public Slider Outlines;

	public Slider Holiday;

	public TMP_Text GrannyWP_Text;

	public TMP_Text Ver_Text;

	public TMP_Text Outine_Text;

	public TMP_Text Grandpa_Text;

	public TMP_Text Ghase_Text;

	public TMP_Text Diff_Text;

	public TMP_Text Quality_Text;

	public TMP_Text Preset_Text;

	public TMP_Text FOV_Text;

	public TMP_Text Holiday_Text;

	public GameObject MusicG;

	public AudioSource Sounds;

	public AudioClip SwitchToggle;

	public GameObject CamHallway;

	public GameObject CamBasement;

	public GameObject CamBackyard;

	public GameObject FirstP;

	public GameObject SecondP;

	public GameObject TipMenu;

	public bool SettingsSet;

	public PostProcessProfile Profile;

	public AmbientOcclusion AO;

	public MotionBlur MB;

	public ChromaticAberration CA;

	public Vignette V;

	public DepthOfField DOF;

	public Grain G;

	public Bloom B;

	public GameObject ExtremeLock;

	public GameObject Box_Diff1;

	public GameObject Box_Diff2;

	public GameObject Box_Diff3;

	public GameObject Box_Diff4;

	public GameObject Box_Diff5;

	public GameObject Box_Diff6;

	public GameObject GrannyP;

	public GameObject GrandpaP;

	public GameObject Came;

	public GameObject Shooty;

	public GameObject Bats;

	public GameObject Crossbow;

	public GameObject Spray;

	public GameObject B1;

	public GameObject B2;

	public Transform PosGrannySpawn;

	public GameObject SlendrinaMenu;

	public Transform[] SlendrinaTeleportPos;

	public float TimerSlendrina;

	public float TimerSlendrinaMax;

	public GameObject HatGranny;

	public GameObject HatGranpa;

	public GameObject HatSlendrina;

	public GameObject NMBought;

	public GameObject GrandpaBought;

	public GameObject ExtrasBought;

	public GameObject CustomCBought;

	public GameObject CHBought;

	public GameObject TrapBought;

	public GameObject VerCBought;

	public GameObject FlashBought;

	public GameObject ExtrapsBought;

	public GameObject PURCHASEDNM;

	public GameObject PURCHASEDCC;

	public GameObject PURCHASEDGR;

	public GameObject PURCHASEDSH;

	public GameObject PURCHASEDEX;

	public GameObject PURCHASEDIM;

	public GameObject PURCHASEDCH;

	public GameObject PURCHASEDTRAP;

	public GameObject PURCHASEDEXTRAPS;

	public GameObject PURCHASEDVER;

	public GameObject PURCHASEDFLASH;

	public GameObject PURCHASEDHOL;

	public GameObject HolCoverButton;

	public GameObject ShootyCoverButton;

	public Material NormalSky;

	public Material NMSky;

	public AudioSource BuySound;

	public AudioSource BackyardBG;

	public AudioClip BNormal;

	public AudioClip BNM;

	public TMP_Text CoinAmount;

	public GameObject TextNotEnough;

	public GameObject Cam;

	public string PlayerPrefsTest;

	public Image BlackFade;

	public Image EX_Door;

	public Image EX_Car;

	public Image EX_Pipe;

	public Image EX_Robo;

	public Image EX_Both;

	public Image IM_Both;

	public Image NoSee;

	public Image NoWP;

	public Image NoDie;

	public Image HARD_Both;

	public Image HOL_Esc;

	public Image EASY_Escape;

	public Image EASY_Car;

	public Image NOR_Escape;

	public Image NOR_Grandpa;

	public Image NOR_Both;

	public Image TeddyCrib;

	public Image HARD_Pipe;

	public Image HARD_Car;

	public Image NOR_Robo;

	public Image HARD_BothRobo;

	public Image HARD_NoDyingBoth;

	public Image EX_RoboBoth;

	public Image EX_AllRouteNo;

	public Image EX_AllRouteYes;

	public Image IM_Door;

	public Image IM_Car;

	public Image IM_Pipe;

	public Image IM_Robo;

	public Image IM_NoSee;

	public Image IM_NoWP;

	public Image HOL_Robo;

	public Image HOL_Both;

	public Image HOL_AllRoutesExtrasBoth;

	public GameObject ArachnoTDis;

	public GameInputManager GINPUT;

	public GameObject FlashO;

	public GameObject LightBulbs;

	public Sprite[] OutlineExampleCount;

	public Image OutlineImageUI;

	public GameObject[] AllOptions;

	public GameObject TextDidntUnlockAnything;

	public bool AbleToTest;

	public GameObject Trophy;

	public AudioSource MusicMenu;

	public AudioSource BGSound;

	public GameObject SliderPreset;

	public GameObject SeedButton;

	public Menu_Seed MS;

	public GameObject FlashLightObject;

	public GameObject textNoticeVERControl;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void SetVolume(float volume)
	{
	}

	public void AttemptBuy_NM()
	{
	}

	public void AttemptBuy_GR()
	{
	}

	public void AttemptBuy_SH()
	{
	}

	public void AttemptBuy_IM()
	{
	}

	public void AttemptBuy_EX()
	{
	}

	public void AttemptBuy_CC()
	{
	}

	public void AttemptBuy_CH()
	{
	}

	public void AttemptBuy_TRAP()
	{
	}

	public void AttemptBuy_EXTRAPS()
	{
	}

	public void AttemptBuy_ROBO()
	{
	}

	public void AttemptBuy_FLASH()
	{
	}

	public void AttemptBuy_VERC()
	{
	}

	public void AttemptBuy_HO()
	{
	}

	public void OnClickSound()
	{
	}

	public void OnClickPlay()
	{
	}

	public void PlayLoad()
	{
	}

	private IEnumerator Timerloading()
	{
		return null;
	}

	public void OnApplicationQuit()
	{
	}

	public void LoadGame()
	{
	}

	public void UnlockTrophy(int t)
	{
	}
}
