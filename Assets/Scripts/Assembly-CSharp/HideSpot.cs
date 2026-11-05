using UnityEngine;

// Shared by the restored hiding spots (BedHide, CarHide, CoffinHide): the native code repeats this check inline.
// Hiding while an enemy is looking (with under 0.7 s of lost sight) gets the player spotted; hiding just after
// she lost sight of them makes her forget where they went.
public static class HideSpot
{
	public static void Enter(Eyes_Granny granny, Eyes_Grandpa grandpa)
	{
		if (granny.Seeing && granny.TimerStopVision < .7f) granny.PlayerSpotted = true;
		else if (!granny.Seeing && .7f < granny.TimerStopVision) granny.AI.UnSeenPlayer = true;
		if (grandpa.Seeing && grandpa.TimerStopVision < .7f) grandpa.PlayerSpotted = true;
		else if (!grandpa.Seeing && .7f < grandpa.TimerStopVision) grandpa.AI.UnSeenPlayer = true;
	}

	// Restoration safety net: the player object is switched off while hidden, so a trigger exit can be missed and
	// leave the "near" flag stuck on (icon visible and R working anywhere). Clear it once the player is out of the
	// spot and more than a metre from its trigger.
	public static bool StillNear(Component spot, GameObject player, bool near, bool hidden)
	{
		if (!near || hidden || player == null || !player.activeInHierarchy) return near;
		var trigger = spot.GetComponent<Collider>();
		if (trigger == null) return near;
		Vector3 p = player.transform.position;
		return (trigger.ClosestPoint(p) - p).sqrMagnitude <= 1f;
	}
}
