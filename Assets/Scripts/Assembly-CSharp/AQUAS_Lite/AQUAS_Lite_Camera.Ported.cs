using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AQUAS_Lite_Camera.c (Granny Legacy, IL2CPP x64).
namespace AQUAS_Lite
{
	public partial class AQUAS_Lite_Camera
	{
		// AQUAS water asset: makes sure the camera renders a depth texture for the water shader.
		private void Start() { Set(); }
		private void Set()
		{
		    var cam = GetComponent<Camera>();
		    if (cam != null && cam.depthTextureMode == DepthTextureMode.None) cam.depthTextureMode = DepthTextureMode.Depth;
		}
	}
}
