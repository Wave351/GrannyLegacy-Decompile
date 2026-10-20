using UnityEngine;

public partial class FreeSpotRule : MonoBehaviour
{
	public bool deactivateSpotSpawn;
	public bool specificAreaOnly;
	public float itemsWithinSame;
	public float itemsWithinMax;
	public FreeSpotRule[] linkedSpots;
}
