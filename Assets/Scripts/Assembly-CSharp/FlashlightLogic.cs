using UnityEngine;

public class FlashlightLogic : MonoBehaviour
{
	public Renderer Render;

	public Color ColorMat;

	public Color SecondaryOptionalColor;

	public Shader ShaderOptional;

	public Material[] OriginalMaterials;

	public Texture[] OriginalTextures;

	public bool Whiteis;

	public bool CustomColor;

	public bool FullyDarkColorItUp;

	public float MetallicValue;

	public bool AlwaysStandard;

	public bool CastShadows;

	public bool GlossDown;

	public bool SecondaryMaterialColor;

	public bool DisableEmission;

	public bool CheckTheOriginal;

	public bool CutOut;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
