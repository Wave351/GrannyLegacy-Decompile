using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class DoorEnd : MonoBehaviour
{
	public Image Black;

	public GameObject Granny;

	public GameObject Grandpa;

	public GameObject Slendrina;

	public Animation GrannyAnim;

	public Animation GrandpaAnim;

	public GameObject TeddyGrandpa;

	public GameObject TeddyHeld;

	public GameObject Shotgun;

	public GameObject Cane;

	public GameObject Bats;

	public GameObject Crossbow;

	public GameObject Spray;

	public GameObject NM;

	public GameObject Cam1;

	public GameObject Cam2;

	public Animation Move;

	public Material MSlendrina;

	public Material MHair;

	public Transform GrannyHomeSpawn;

	public Transform GrannyHomeCameBackSpawn;

	public Transform GrannyHomeGoodEndingSpawn;

	public Transform GrandpaSpawnEnding;

	public Transform TeddyGroundSpawn;

	public Renderer[] EyesTransition;

	public PostProcessProfile P;

	public ChromaticAberration CA;

	public DepthOfField DOF;

	public Grain G;

	public Vignette V;

	public Color KrampusEyesGrandpa;

	public Renderer GrandpaEyes;

	public GameObject HatGrandpa;

	public Renderer GrandpaHorns;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator TimerCam1()
	{
		return null;
	}
}
