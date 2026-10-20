using UnityEngine;

public class Enter_SC : MonoBehaviour
{
	public BGManager BG;

	public bool Entered_SC;

	public Exit_SC ExitSC1;

	public Exit_SC ExitSC2;

	public WaterSplash WaterSplash;

	public AudioSource Mom1;

	public AudioSource Mom2;

	public AudioSource Mom3;

	public GameObject MomMesh;

	public Eyes_Granny EnemyEyes1;

	public Eyes_Grandpa EnemyEyes2;

	public FallingHolder FH;

	public WaterSplash WS;

	public Renderer HatChristmas;

	public Renderer HalloweenHat;

	public Renderer Glasses;

	public Renderer Lepra;

	public Renderer Valentines;

	public Renderer Bunny;

	private void OnTriggerEnter(Collider other)
	{
	}
}
