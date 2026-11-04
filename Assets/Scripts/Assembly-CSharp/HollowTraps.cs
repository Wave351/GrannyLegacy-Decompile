using System.Collections.Generic;
using UnityEngine;

public partial class HollowTraps : MonoBehaviour
{
	public Transform[] spawnsTrap;
	public GameObject hollowTrap;
	public float timerSpawnTraps;
	public float maxTimerSpawnTrap;
	public MobileFPS FPS;
	public List<GameObject> currentTraps;
}
