using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HalloweenColorGrade : MonoBehaviour
{
	public PostProcessProfile Profile;

	public ColorGrading CG;

	public PostProcessProfile Profile2;

	public ColorGrading CG2;

	public PostProcessProfile Profile3;

	public ColorGrading CG3;

	public float PostExposureEV_Normal;

	public float HueShift_Normal;

	public float Saturation_Normal;

	public float Contrast_Normal;

	public Color colorNormal;

	public float PostExposureEV_HA;

	public float HueShift_HA;

	public float Saturation_HA;

	public float Contrast_HA;

	public Color colorHA;

	public float PostExposureEV_CH;

	public float HueShift_CH;

	public float Saturation_CH;

	public float Contrast_CH;

	public Texture2D SlendrinaHalloween;

	public Texture2D SlendrinaNormal;

	public Texture_Changing TSlendrina;

	public Material SlendrinaM;

	public Texture2D NM;

	public GameObject MusicNormal;

	public GameObject MusicHalloween;

	public GameObject MusicChristmas;

	public Material TeddyM;

	public Material TeddyM2;

	public Texture2D TeddyNormal;

	public Texture2D TeddyHalloween;

	public Texture2D TeddyNM;

	public GameObject Teddy_Elf;

	public GameObject Teddy_Summer;

	public GameObject Teddy_Lepre;

	public GameObject Teddy_Valentine;

	public GameObject Teddy_Ears;

	public Renderer CandyCane;

	public GameObject LilSanta;

	public Texture_Changing TCWalls;

	public Texture2D NormalWalls;

	public Texture2D JollyWalls;

	public GameObject JollyDecoUI;

	public Texture2D BatNormal;

	public Texture2D BatNM;

	public Texture2D BatGay;

	public Renderer BatBloody;

	public GameObject[] grandpaPrideAccessories;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
