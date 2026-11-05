using UnityEngine;
using UnityEngine.AI;

// Restoration guard shared by AI_Granny/AI_Grandpa door checks. The original only casts the door ray straight ahead,
// so an enemy reaching a closed door at an angle hits the wall beside it and keeps pushing toward the player until it
// happens to turn far enough. This also tries the direction the agent is actually steering (its next path corner),
// and a ray from slightly behind in case the door was shut on top of the enemy.
public static class EnemyDoorRay
{
	public static bool IsDoor(Collider c)
	{
		return c && (c.GetComponentInParent<AnimatedStuff>() || c.GetComponentInParent<Sauna>() || c.GetComponentInParent<PrisondoorGranny>()
		             || c.GetComponentInParent<AtticDoorManage>() || c.GetComponentInParent<BackdoorManage>());
	}

	// `working` is the door the enemy is busy opening (or rattling, if locked). It is checked first so that a new
	// destination (for example a noise elsewhere) can't turn the steering ray away from it mid-attempt; in the original
	// the enemy only ever starts on a door straight ahead, so it always finished opening before following the noise.
	public static bool Cast(Transform ray, NavMeshAgent agent, float distance, LayerMask layers, out RaycastHit hit, Collider working = null)
	{
		hit = default;
		if (!ray) return false;
		if (working && working.enabled && working.gameObject.activeInHierarchy)
		{
			Vector3 to = working.ClosestPoint(ray.position) - ray.position;
			if (to.sqrMagnitude < .0001f) to = working.bounds.center - ray.position;
			RaycastHit stick;
			if (to.sqrMagnitude > .0001f && Physics.Raycast(ray.position, to.normalized, out stick, distance, layers) && stick.collider == working)
			{ hit = stick; return true; }
		}
		bool ahead = Physics.Raycast(ray.position, ray.forward, out hit, distance, layers);
		if (ahead && IsDoor(hit.collider)) return true;
		if (agent && agent.enabled && agent.hasPath)
		{
			Vector3 dir = agent.steeringTarget - ray.position; dir.y = 0;
			RaycastHit steer;
			if (dir.sqrMagnitude > .01f && Physics.Raycast(ray.position, dir.normalized, out steer, distance, layers) && IsDoor(steer.collider))
			{ hit = steer; return true; }
		}
		if (ahead) return true;
		return Physics.Raycast(ray.position - ray.forward * .75f, ray.forward, out hit, distance + .75f, layers);
	}
}
