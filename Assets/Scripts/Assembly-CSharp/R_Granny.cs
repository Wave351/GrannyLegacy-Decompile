using System.Collections;
using UnityEngine;

public class R_Granny : MonoBehaviour
{
	public GameObject[] ObjectsDisable;

	public Renderer Skin;

	public Renderer F1;

	public Renderer F2;

	public Color SkinC;

	public Color FC;

	public Color SkinPoisoned;

	public float TimerFade;

	public float TimerGone;

	public bool Fadin;

	public Texture_Changing TC;

	public bool Poisoned;

	public GameObject BatBloodyLight;

	public GameObject GrannyStuff;

	public GameObject RepositionBatPos;

	public Transform Hand;

	public Transform PositionBatBloody;

	public bool PositionBat;

	public GameObject PS;

	public GameObject EC;

	public GameObject Crossbow;

	public GameObject PepperSpray;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private IEnumerator TimerGo()
	{
		return null;
	}
}
