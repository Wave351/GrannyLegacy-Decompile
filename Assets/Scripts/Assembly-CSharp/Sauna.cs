using UnityEngine;

public class Sauna : MonoBehaviour
{
	public GameObject Door;

	public Collider Player;

	public Collider PlayerH;

	public bool Closed;

	public bool PutHandle;

	public GameObject Handle;

	public AudioClip DoorOpen;

	public AudioClip DoorClose;

	public AudioClip DoorLocked;

	public AudioClip SmashSound;

	public AudioClip Handle1;

	public GameObject Switch;

	public bool SwitchOn;

	public ParticleSystem SteamSauna;

	public float CurrentValSauna;

	public AudioSource SaunaSteamSound;

	public float SpeedSteamSound;

	public SaunaDeath Sdeath;

	private void Update()
	{
	}

	public void HandleModify()
	{
	}

	public void ModifySwitch()
	{
	}

	public void RemoveHandle()
	{
	}

	public void OpenDoor()
	{
	}
}
