using UnityEngine;
using UnityEngine.AI;

public class SaunaNoiseFix : MonoBehaviour
{
	public SaunaDeath Sdeath;

	public Transform RespawnPoint;

	public Transform RespawnPointNo;

	public NavMeshObstacle Obstacle;

	private void OnTriggerStay(Collider other)
	{
	}
}
