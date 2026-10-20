using UnityEngine;

public class Water : MonoBehaviour
{
	public float XSpeed;

	public float YSpeed;

	public float OffsetX;

	public float OffsetY;

	public AudioClip WaterSplash;

	public Renderer WaterM;

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
