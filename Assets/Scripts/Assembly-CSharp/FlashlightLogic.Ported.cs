using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/FlashlightLogic.c (Granny Legacy, IL2CPP x64).
public partial class FlashlightLogic
{
	public FlashlightLogic() { Whiteis = true; FullyDarkColorItUp = true; MetallicValue = 1; AlwaysStandard = true; CastShadows = true; CheckTheOriginal = true; }
	// Flashlight mode ("FlashLightIsOn"): items get lit Standard materials so the flashlight shows them.
	private void Start()
	{
	    if (Render == null && GetComponent<Renderer>() != null) Render = GetComponent<Renderer>();
	    bool flashlight = PlayerPrefs.GetInt("FlashLightIsOn") == 1;
	    if (Render && flashlight)
	    {
	        OriginalMaterials = Render.sharedMaterials;
	        OriginalTextures = new Texture[OriginalMaterials.Length];
	        for (int i = 0; i < OriginalMaterials.Length; i++)
	            if (OriginalMaterials[i] != null) OriginalTextures[i] = OriginalMaterials[i].mainTexture;
	    }
	    if (!flashlight) { Destroy(GetComponent<FlashlightLogic>()); return; }
	    foreach (var m in Render.materials)
	    {
	        if (AlwaysStandard) m.shader = Shader.Find("Standard");
	        else if (ShaderOptional != null) m.shader = ShaderOptional;
	        else return;
	        if (CastShadows) { Render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On; Render.receiveShadows = true; }
	        if (FullyDarkColorItUp && !CustomColor)
	        {
	            var c = Render.material.color;
	            m.color = new Color(.4717f, .4717f, .4717f, c.a);
	            m.SetFloat("_Metallic", MetallicValue);
	            if (GlossDown) m.SetFloat("_Glossiness", 0);
	        }
	        else if (Whiteis && !FullyDarkColorItUp)
	        {
	            m.color = Color.white;
	            if (GlossDown) m.SetFloat("_Glossiness", 0);
	        }
	        else if (CustomColor)
	        {
	            m.color = ColorMat;
	            m.SetFloat("_Metallic", MetallicValue);
	            if (GlossDown) m.SetFloat("_Glossiness", 0);
	        }
	        else return;
	        m.SetFloat("_BumpScale", 0);
	        if (CutOut)
	        {
	            m.SetFloat("_Mode", 1);
	            if (m.HasProperty("_Cutoff")) m.SetFloat("_Cutoff", .5f);
	            m.SetInt("_SrcBlend", 1); m.SetInt("_DstBlend", 0); m.SetInt("_ZWrite", 1);
	            m.DisableKeyword("_ALPHABLEND_ON"); m.DisableKeyword("_ALPHAPREMULTIPLY_ON"); m.EnableKeyword("_ALPHATEST_ON");
	            m.renderQueue = 2450;
	            m.SetOverrideTag("RenderType", "TransparentCutout");
	        }
	    }
	    if (!SecondaryMaterialColor) return;
	    var mats = Render.materials;
	    if (mats.Length > 0 && mats[0] != null) mats[0].color = SecondaryOptionalColor;
	}
	// Keeps the original textures on the replacement materials.
	private void Update()
	{
	    if (!CheckTheOriginal || !Render || OriginalMaterials == null) return;
	    var mats = Render.sharedMaterials;
	    bool changed = false;
	    for (int i = 0; i < mats.Length; i++)
	    {
	        if (mats[i] == null || i >= OriginalMaterials.Length || OriginalMaterials[i] == null) continue;
	        var tex = i < OriginalTextures.Length ? OriginalTextures[i] : OriginalMaterials[i].mainTexture;
	        if (mats[i].mainTexture != tex) { mats[i].mainTexture = tex; changed = true; }
	    }
	    if (changed) Render.sharedMaterials = mats;
	}
}
