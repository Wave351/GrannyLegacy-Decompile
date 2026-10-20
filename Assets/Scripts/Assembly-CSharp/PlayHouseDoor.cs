using System.Collections;
using UnityEngine;

public class PlayHouseDoor : MonoBehaviour
{
	public bool OpenedHouse;

	public bool AttemptOpen;

	public Animation DoorAnim;

	public AudioClip LekhusLocked;

	public void AttemptLock()
	{
	}

	public IEnumerator LockedAnim()
	{
		return null;
	}
}
