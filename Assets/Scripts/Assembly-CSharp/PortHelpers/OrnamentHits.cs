using UnityEngine;

// Shared check from Ornament_Freeze / Ornament_Bomb.OnCollisionEnter (same tag/name list in both).
public static class OrnamentHits
{
	public static bool IsBreakSurface(GameObject other)
	{
		string tag = other.tag;
		string name = other.name;
		return tag == "golv" || tag == "grus" || tag == "Noice" || name == "PalletsBackyard" || name == "Main_HouseWalls" || name == "OldCellar";
	}
}
