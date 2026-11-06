using UnityEngine;

// Added for the decompile (not in the original game): drives the "Nightmare Fog" option.
// The game ships three red fog volumes (Nightmare-Stuff/Fogs) that are never switched on and only cover a small
// area. This keeps one of them around the player so the fog fills every room they go to: particles are spawned in
// a box around the player in world space, so fog left behind stays put while new fog appears ahead.
// Only used with Nightmare textures (the menu greys the option out otherwise).
public class NightmareFogFollow : MonoBehaviour
{
	public Transform Target;
	public Vector3 Size = new Vector3(26f, 7f, 26f);
	public int Particles = 260;   // fog sprites alive around the player at once

	private ParticleSystem fog;

	public static void Setup(GameObject nmStuff, Transform player)
	{
		if (nmStuff == null || player == null) return;
		Transform fogs = nmStuff.transform.Find("Fogs");
		if (fogs == null) return;
		ParticleSystem[] systems = fogs.GetComponentsInChildren<ParticleSystem>(true);
		if (systems.Length == 0) return;
		ParticleSystem template = systems[0];
		for (int i = 1; i < systems.Length; i++) systems[i].gameObject.SetActive(false);

		var go = template.gameObject;
		go.transform.SetParent(null, false);   // Nightmare-Stuff (its parent) is off without NM textures
		go.transform.SetPositionAndRotation(player.position, Quaternion.identity);
		go.transform.localScale = Vector3.one;
		go.SetActive(true);
		var follow = go.AddComponent<NightmareFogFollow>();
		follow.Target = player;
		follow.Configure(template);
	}

	private void Configure(ParticleSystem system)
	{
		fog = system;
		fog.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

		var main = fog.main;
		main.simulationSpace = ParticleSystemSimulationSpace.World;
		main.scalingMode = ParticleSystemScalingMode.Local;
		main.loop = true;
		main.startLifetime = 6f;
		main.startSize = new ParticleSystem.MinMaxCurve(3.5f, 5.5f);
		main.maxParticles = 2000;
		main.startSpeed = new ParticleSystem.MinMaxCurve(0.05f, 0.35f);   // slow drift, in any direction (box shape)
		main.startColor = new Color(0.4433962f, 0.056470275f, 0.056470275f, 0.7294118f);   // the original red

		var shape = fog.shape;
		shape.enabled = true;
		shape.shapeType = ParticleSystemShapeType.Box;
		shape.position = Vector3.zero;
		shape.rotation = Vector3.zero;
		shape.scale = Size;

		var emission = fog.emission;
		emission.enabled = true;
		emission.rateOverTime = Particles / main.startLifetime.constant;

		fog.Simulate(main.startLifetime.constant, true, true);   // start already filled
		fog.Play(true);
	}

	private void LateUpdate()
	{
		if (Target != null) transform.position = Target.position;
	}
}
