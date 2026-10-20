using UnityEngine;

public class PlayerVsEnemyCollisionTrigger : MonoBehaviour
{
	public Collider player;

	public Collider[] collidersAvoid;

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	public void TurnBackOn()
	{
	}
}
