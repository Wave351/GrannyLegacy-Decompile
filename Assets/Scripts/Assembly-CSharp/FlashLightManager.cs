using System.Collections;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
	public bool FlashLightIsActive;

	public GameObject[] AllFlashLights;

	public GameObject TableFlashlight;

	public GameObject InHandFlashLight;

	public bool OverrideAmbient;

	public Color AmbColor;

	public float AmbIntensity;

	public bool DisReflectProbes;

	public Renderer[] SMRGranny;

	public Material SwitchRealTimeGranny;

	public GameObject FlashlightPrefab;

	public Transform DropPoint;

	public MeshFilter CarModel;

	public MeshFilter CarTrunk;

	public MeshFilter CarTailgate;

	public Mesh CarModelHD;

	public Mesh CarTrunkHD;

	public Mesh CarTailgateHD;

	private void Start()
	{
	}

	public void DisableFlashlightStatus()
	{
	}

	private IEnumerator WaitingFlashBackOnTable()
	{
		return null;
	}

	public void SetUpFlashLightMode()
	{
	}
}
