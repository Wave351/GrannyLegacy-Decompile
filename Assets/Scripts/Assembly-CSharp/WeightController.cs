using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class WeightController : MonoBehaviour
{
	public float Weight;
	public float WeightFade;
	public float MaxWeight;
	public TMP_Text TextKG;
	public float WeightUnlock;
	public Renderer Lamp;
	public float previousWeight;
	public AudioClip AddedWeight;
	public AudioClip RemovedWeight;
	public AudioSource AS;
	public Animation OpenDoor;
	public bool StillIntact;
	public bool IfOpen;
	public bool DoneIt;
	public bool ShotgunInPlace;
	public Dictionary<string, float> ItemWeights;
	public HashSet<ItemSeedData> CurrentItems;
}
