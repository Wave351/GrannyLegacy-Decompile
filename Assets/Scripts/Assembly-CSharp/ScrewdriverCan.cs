using UnityEngine;

public class ScrewdriverCan : MonoBehaviour
{
	public AudioSource Audio;

	public AudioClip Sound;

	public GameObject Screw;

	public bool Hit;

	private void OnCollisionEnter(Collision collision)
	{
	}
}
