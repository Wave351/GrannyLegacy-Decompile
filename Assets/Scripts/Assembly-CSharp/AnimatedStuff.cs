using System.Collections;
using UnityEngine;

public class AnimatedStuff : MonoBehaviour
{
	public float OpeningTimer;

	public float ClosingTimer;

	public AudioSource Audio;

	public AudioClip CloseSound;

	public AudioClip OpenSound;

	public string ClosedName;

	public string OpenName;

	public Animation AnimatedObject;

	public AnimationClip OpenAnimation;

	public AnimationClip CloseAnimation;

	public bool CanBeLayer;

	public bool IfLockKyl;

	public bool EnabledAnim;

	public void OpenAction()
	{
	}

	public void CloseAction()
	{
	}

	public void AttemptClose()
	{
	}

	private IEnumerator OpeningAction()
	{
		return null;
	}

	private IEnumerator ClosingAction()
	{
		return null;
	}
}
