using UnityEngine;

public partial class SunnyGrannyEnemy : MonoBehaviour
{
	public GameObject player;
	public AudioSource sunAudio;
	public Light light;
	public int spotNumb;
	public Transform[] spotsToTeleport;
	public int currentSpot;
	public GameObject explosion;
	public Transform sunHead;
	public Transform sunHeadParent;
	public float timerInPlayer;
	public float boostUpLightSpeed;
	public bool inSpot;
	public float timerMoveOn;
}
