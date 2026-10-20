using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public GameObject GrannyStuff;

	public GameObject GrandpaStuff;

	public GameObject Grandpa;

	public GameObject Granny;

	public GameObject Slendrina;

	public GameObject MomCrawl;

	public GameObject MomBody;

	public GameObject MomSpider;

	public Transform PositionSpawnMom;

	public GameObject MomPutDownSound;

	public GameObject ActivateTriggers;

	public Transform MomSpiderPosSend;

	public FallingHolder FallingHolder;

	public bool PutDownMom;

	public bool MomKilled;

	public GameObject DollFreezeMom;

	public GameObject MomFullBody;

	public GameObject SpiderKey;

	public HE_Spawn SpawnCheck;

	public Transform Pos1Granny;

	public Transform Pos2Granny;

	public Transform Pos3Granny;

	public Transform Pos4Granny;

	public Transform Pos1Grandpa;

	public Transform Pos2Grandpa;

	public Transform Pos3Grandpa;

	public Transform Pos4Grandpa;

	public Transform SlendrinaStartPos;

	public bool GrannyKilled;

	public float TimerGrannyRecover;

	public float MaxTimerRecoverGranny;

	public TMP_Text GrannyGoneText;

	public bool GrandpaKilled;

	public float TimerGrandpaRecover;

	public float MaxTimerRecoverGrandpa;

	public TMP_Text GrandpaGoneText;

	public Transform RagdollPSGranny;

	public GameObject RagdollGranny;

	public GameObject GrannyFreezeDoll;

	public Transform RagdollPSGrandpa;

	public Transform RagdollPSGrandpaBed;

	public GameObject RagdollGrandpa;

	public GameObject GrandpaFreezeDoll;

	public GameObject GrandpaBed;

	public GameObject ShotgunGrandpaBed;

	public float TimerInBed;

	public float MaxBedSleep;

	public GameObject ShotgunGrandpa;

	public TeddyHunt TH;

	public SaunaDeath SDeath;

	public GameObject N1;

	public GameObject N2;

	public EnemyFootstep EF1;

	public EnemyFootstep EF2;

	public GameObject CanePBed;

	public Escapes ES;

	public Transform Bedroom1Spot;

	public Transform Bedroom2Spot;

	public Transform Bedroom3Spot;

	public Transform CouchSpot;

	public Transform ShotgunBedSpot_1;

	public Transform ShotgunBedSpot_2;

	public Transform ShotgunBedSpot_3;

	public Transform ShotgunCouchSpot;

	public AudioSource MomRoarDeath;

	public WeightController WC;

	public GameObject PepperHand;

	public GameObject CrossbowHand;

	public float spawnSelected;

	public bool speedrunAdditionSpawnOn;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GrannyFrozeDeath()
	{
	}

	public void GrannyDeath(bool Poisoned)
	{
	}

	public void GrannyDeathS()
	{
	}

	public void GrandpaFrozeDeath()
	{
	}

	public void GrandpaDeath(bool Poisoned)
	{
	}

	public void GrandpaDeathS()
	{
	}

	public void GrandpaDeathSBed()
	{
	}

	public void MurderNoise1()
	{
	}

	public void MurderNoise2()
	{
	}

	public void EnemyRearrange()
	{
	}

	public void SpawnGranny()
	{
	}

	public void SpawnGrandpa()
	{
	}

	public void GrandpaySleep(int SpotChoosen)
	{
	}

	public void SlendrinaSpawn()
	{
	}

	public void CrawlFaster()
	{
	}

	public void StartCrawlingMom()
	{
	}

	public void MomShoot()
	{
	}

	public void MomFroze()
	{
	}

	private IEnumerator TimerFreeze()
	{
		return null;
	}

	public void NavOBSDisable()
	{
	}

	public void MomFreed()
	{
	}

	private IEnumerator DisableRoarAfterCrawlFaster()
	{
		return null;
	}
}
