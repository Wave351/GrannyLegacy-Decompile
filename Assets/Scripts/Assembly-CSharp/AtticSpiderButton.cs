using System.Collections;
using UnityEngine;

public class AtticSpiderButton : MonoBehaviour
{
	public Animation WoodPiece;

	public GameObject Spider;

	public SpiderAggro SpiderAggro;

	public AtticSpider AtticSpider;

	public void StopSpider()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private IEnumerator TimerStop()
	{
		return null;
	}
}
