using System.Collections;
using UnityEngine;

public class Freezing : MonoBehaviour
{
	public bool Prepared;

	public bool Triggerd;

	public Renderer FreezeTrap;

	public Texture2D ReadyToFreeze;

	public Texture2D OldText;

	public ParticleSystem FrozeEffect;

	public GameObject ScEFF;

	public GameObject EnemyStuff;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private IEnumerator WaitRetagged()
	{
		return null;
	}
}
