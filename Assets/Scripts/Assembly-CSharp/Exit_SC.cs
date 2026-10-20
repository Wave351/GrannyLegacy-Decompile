using UnityEngine;

public class Exit_SC : MonoBehaviour
{
	public BGManager BG;

	public bool Exited_SC;

	public Enter_SC EnterSC;

	public Exit_SC Other_ExitSC;

	public AudioSource Mom1;

	public AudioSource Mom2;

	public AudioSource Mom3;

	public GameObject MomMesh;

	public AI_MomSpider AI;

	public WaterSplash WSplash;

	public Renderer HatChristmas;

	public Renderer HalloweenHat;

	public Renderer Glasses;

	public Renderer Lepra;

	public Renderer Valentines;

	public Renderer Bunny;

	public void StopNewDay()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
