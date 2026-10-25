using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class KeyBindManage : MonoBehaviour
{
	public GameObject PrimaryB;
	public GameObject SecondaryB;
	public GameObject DropB;
	public GameObject CrouchB;
	public GameObject ShootB;
	public KeyCode PrimaryInteraction;
	public KeyCode SecondaryInteraction;
	public KeyCode Dropping;
	public KeyCode Crouching;
	public KeyCode Shooting;
	public float CurrentKeyBindTab;
	public List<KeyCode> allowedKeys;
	public bool WaitingForKey;
	public KeyCode NewKey;
	public GameObject KeyBindMenu;
	public Button RebindPrimaryB;
	public Button RebindSecondaryB;
	public Button RebindDropB;
	public Button RebindCrouchB;
	public Button RebindShootB;
	public GameObject PressEnterText;
	public Button[] BackButton;
	public PickRay PickRay;
	public DoorRay DoorRay;
	public CrouchHolder CrouchHolder;
	public RayRemoveTraps BearRay;
	public TMP_Text PrimaryText;
	public TMP_Text SecondaryText;
	public TMP_Text DropText;
	public TMP_Text CrouchText;
	public TMP_Text ShootText;
}
