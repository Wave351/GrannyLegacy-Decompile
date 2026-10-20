using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Camera_FovSave.c (Granny Legacy, IL2CPP x64).
public partial class Camera_FovSave
{
	private void Start()
	{
		int fov = PlayerPrefs.GetInt("FOV");
		if (fov >= 0 && fov <= 7)
		{
			GetComponent<Camera>().fieldOfView = 60f + 5f * fov;
		}
		UnityEngine.Object.Destroy(GetComponent<Camera_FovSave>());
		if (PlayerPrefs.GetInt("OcclusionCullingOP") == 0)
		{
			GetComponent<Camera>().useOcclusionCulling = false;
		}
	}
}
