using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FlashLightManager.c (Granny Legacy, IL2CPP x64).
public partial class FlashLightManager
{
	public FlashLightManager() { OverrideAmbient = true; AmbColor = new Color(0.5f, 0.5f, 0.5f, 1f); AmbIntensity = 1f; }
	private void Start()
	{
		if (PlayerPrefs.GetInt("FlashLightIsOn", 0) != 1) return;
		LightmapSettings.lightmaps = new LightmapData[0]; DynamicGI.UpdateEnvironment();
		if (OverrideAmbient) { RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat; RenderSettings.ambientLight = AmbColor; RenderSettings.ambientIntensity = AmbIntensity; }
		if (DisReflectProbes) foreach (var probe in UnityEngine.Object.FindObjectsByType<ReflectionProbe>(FindObjectsSortMode.None)) probe.enabled = false;
		SetUpFlashLightMode();
		if (SMRGranny != null) foreach (var renderer in SMRGranny) if (renderer != null) renderer.sharedMaterial = SwitchRealTimeGranny;
		if (CarModel != null) CarModel.mesh = CarModelHD;
		if (CarTrunk != null) CarTrunk.mesh = CarTrunkHD;
		if (CarTailgate != null) CarTailgate.mesh = CarTailgateHD;
	}
	public void DisableFlashlightStatus()
	{
		if (InHandFlashLight != null) InHandFlashLight.SetActive(false);
		if (FlashlightPrefab != null && DropPoint != null)
		{
			var light = UnityEngine.Object.Instantiate(FlashlightPrefab, DropPoint.position, DropPoint.rotation);
			var body = light.GetComponent<Rigidbody>(); if (body != null) body.AddForce(light.transform.forward * 6f, ForceMode.Impulse);
			StartCoroutine(WaitingFlashBackOnTable());
		}
	}
	private IEnumerator WaitingFlashBackOnTable() { yield return new WaitForSeconds(5.5f); if (TableFlashlight != null) TableFlashlight.SetActive(true); }
	public void SetUpFlashLightMode()
	{
		FlashLightIsActive = true;
		if (TableFlashlight != null) TableFlashlight.SetActive(true);
		if (InHandFlashLight != null) InHandFlashLight.SetActive(false);
		if (AllFlashLights != null) foreach (var light in AllFlashLights) if (light != null) light.SetActive(true);
	}
}
