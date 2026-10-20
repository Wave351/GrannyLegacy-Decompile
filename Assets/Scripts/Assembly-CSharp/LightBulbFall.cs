using UnityEngine;

public class LightBulbFall : MonoBehaviour
{
	public bool Fell;

	public AudioClip LightOver;

	public Renderer Renderer;

	public Color ShutdownColor;

	private void OnTriggerEnter(Collider other)
	{
	}
}
