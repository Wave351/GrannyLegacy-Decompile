using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickRay : MonoBehaviour
{
	public GameObject Button;

	public GameObject Ring;

	public GameObject Drop1;

	public GameObject Player;

	public float RaycastDis;

	public float RaycastCheckItemDis;

	public bool CanDropDis;

	public float DropForceItem;

	public Transform DropP;

	public Transform DropPPlank;

	public Transform DropPFreezeTrap;

	public Transform DropPointGridsPos;

	public Transform DropPointNormalPos;

	public ChargingAnimHold CAH;

	public WeightController WC;

	public PlayerStatus PlayerStatus;

	public CrowManage CM;

	public WindowJumping WJ;

	public AtticSpider AS;

	public Escapes Escapes;

	public Transform SpawnDart;

	public GameObject DartShoot;

	public GameObject UnchargedCrossbow;

	public GameObject ChargedCrossbow;

	public float ShootSpeed;

	public bool IsCharged;

	public GameObject ShootCB1;

	public GameObject ShootB1;

	public float ImpactS1;

	public float Gun1RayDistance;

	public Transform RayShoot1;

	public bool LoadedGun;

	public GameObject ShootB2;

	public float ImpactS2;

	public GameObject MuzzleFront;

	public float Gun2RayDistance;

	public Transform RayShoot2;

	public float AmmoAmount;

	public Transform ShotgunHandMain;

	public Transform ShotgunHandPipes;

	public Vector3 ShotgunHandMain_ZeroPos;

	public Vector3 ShotgunHandMain_ZeroRot;

	public AudioClip EmptyGun;

	public Animation ShootHead;

	public GameObject BloodEffect;

	public GameObject ExplosionGas;

	public bool IsPOWER;

	public GameObject MoreAmmo1;

	public GameObject MoreAmmo2;

	public GameObject FuelTankButton;

	public GameObject RotateWinchButton;

	public GameObject UseB;

	public GameObject SprayB;

	public ParticleSystem EffectSpray;

	public float SprayLeft;

	public bool IsCloseRemote;

	public float ChargePoints;

	public bool MeatPlaced;

	public bool DartHasEnergy;

	public TMP_Text Space;

	public TMP_Text Shoot1;

	public TMP_Text Shoot2;

	public TMP_Text Shoot3;

	public TMP_Text SprayShoot;

	public TMP_Text Use;

	public Image Crosshair;

	public Image EButton_Door;

	public Image EButton_Pick;

	public Image EButtonWinch;

	public GameObject AmmoCounterUI;

	public GameObject AmmoUI1;

	public GameObject AmmoUI2;

	public GameObject ItemDrop;

	public GameObject O_FT;

	public GameObject O_PTRAP;

	public GameObject ImpactLord;

	public GameObject H_DSH;

	public GameObject H_CP;

	public GameObject H_HA;

	public GameObject H_MK;

	public GameObject H_PK;

	public GameObject H_SK;

	public GameObject H_CO;

	public GameObject H_CR;

	public GameObject H_TD;

	public GameObject H_WK;

	public GameObject H_SD;

	public GameObject H_PL;

	public GameObject H_BA;

	public GameObject H_PP1;

	public GameObject H_PP2;

	public GameObject H_PP3;

	public GameObject H_PP4;

	public GameObject H_PLAYK;

	public GameObject H_ME;

	public GameObject H_TE;

	public GameObject H_CW1;

	public GameObject H_CW2;

	public GameObject H_WH;

	public GameObject H_SG;

	public GameObject H_GAS;

	public GameObject H_PS1;

	public GameObject H_PS2;

	public GameObject H_PS3;

	public GameObject H_CK;

	public GameObject H_CB;

	public GameObject H_SP;

	public GameObject H_WR;

	public GameObject H_EP;

	public GameObject H_MEAT;

	public GameObject H_SPK;

	public GameObject H_BO;

	public GameObject H_SPRAY;

	public GameObject H_RC;

	public GameObject H_BS;

	public GameObject H_FT;

	public GameObject H_RPK;

	public GameObject H_WC;

	public GameObject H_WS;

	public GameObject H_SPIDERK;

	public GameObject H_CC;

	public GameObject H_TM;

	public GameObject H_VAS;

	public GameObject H_VAS2;

	public GameObject H_VAS3;

	public GameObject H_DATA;

	public GameObject H_BATON;

	public GameObject H_EK;

	public GameObject H_PTRAP;

	public GameObject H_RAT;

	public GameObject H_ORNFREEZE;

	public GameObject H_ORNBOMB;

	public GameObject H_FUSE;

	public GameObject T_HighlightItem;

	public GameObject T_NeedShit;

	public AudioClip PickUp;

	public AudioClip PickUpSoft;

	public AudioClip PickUpKey;

	public AudioClip UsedKey;

	public AudioClip Screw;

	public AudioClip CodeVerify;

	public AudioClip DragSwitch;

	public AudioClip UnlockDoor;

	public AudioClip LockedSafe;

	public AudioClip AttemptOpenDoor;

	public AudioClip PlaceS;

	public AudioClip LockedTrunk;

	public AudioClip LoadGun;

	public AudioClip ReloadGun;

	public AudioClip LoadPrepareGun;

	public AudioClip PickCross;

	public AudioClip LoadCross;

	public AudioClip ShootCross;

	public AudioClip ClickRemote;

	public AudioClip FillSeedy;

	public AudioClip ImpactSound;

	public bool buttonClicked;

	public bool buttonClicked2;

	public HandlePuzzles HP;

	public EnemyController EnemyControl;

	public Inventory Inventory;

	public LayerMask GunLayers;

	public bool TookMelon;

	public bool PiecePickedUp;

	public bool BookPickedUp;

	public GameObject Noisy1;

	public GameObject Noisy2;

	public LayerMask LayersIncluded;

	public ParticleSystem ElectricEffectBaton;

	public GameObject ParticleEffectElectricSpawn;

	public GameObject MelonBlown;

	public LittleFucker LF;

	public GameObject ScrewdriverStanding;

	public KeyCode MainInteract;

	public KeyCode DropKey;

	public KeyCode ShootKey;

	private void Update()
	{
	}

	public void PickShotgun()
	{
	}

	public void PickShooty()
	{
	}

	public void ShootGun1()
	{
	}

	public void ShootGun2()
	{
	}

	private void HandleButtonClick()
	{
	}

	private void HandleButtonClick1()
	{
	}

	public void DeathAction()
	{
	}

	public void CheckItemDropping()
	{
	}

	public void FellObjectBackyard()
	{
	}

	public void StopSetOfTexts()
	{
	}

	public void TurnOffItemTexts()
	{
	}

	public void PickBaton()
	{
	}
}
