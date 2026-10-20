using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
	public Image BloodScreen1;

	public Image BloodBitenScreen;

	public Image BloodScreenHit;

	public Image BloodScreenGameover;

	public Image BloodScreenDay4;

	public Image FrostScreen;

	public bool Hurt;

	public bool Frozen;

	public AudioClip Unfrozen;

	public float Blood;

	public float FreezeVolume;

	public float SpeedFadeUp;

	public float SpeedFadeDown;

	public PostProcessProfile Profile;

	public ChromaticAberration CA;

	public Vignette V;

	public DepthOfField DOF;

	public Grain G;

	public bool Blinded;

	public float SpeedBlindnessTransition;

	public Image BlindScreen;

	public float BlindnessValue;

	public Vector2[] VignetteBlindnessPresets;

	public float TimerBlindness;

	public float TimerMaxBlindnessRecover;

	public bool NormalSee;

	public float SpeedGo;

	public float SpeedAppear;

	public float Scare1;

	public float Scare2;

	public float Max1;

	public float Max2;

	public Animation Camera;

	public GameObject Player;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void FrozePlayer()
	{
	}

	private IEnumerator TimerUnfroze()
	{
		return null;
	}

	public void BlindedAction()
	{
	}

	public void FanHurt()
	{
	}

	private IEnumerator FanFinger()
	{
		return null;
	}
}
