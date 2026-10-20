using System.Collections;
using UnityEngine;

public class SpiderJumpscare : MonoBehaviour
{
	public GameObject Spider;

	public float TimerBackSpider;

	public bool AbleToSpider;

	public AudioSource Source;

	public void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator SpiderAction()
	{
		return null;
	}

	public void SpiderTimerOff()
	{
	}

	private IEnumerator Restart()
	{
		return null;
	}
}
