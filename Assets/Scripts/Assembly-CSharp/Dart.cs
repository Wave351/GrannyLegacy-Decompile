using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public partial class Dart : MonoBehaviour
{
	public bool DidTheJob;
	private bool DidBounce;
	public float BounceForce;
	public ParentConstraint PC;
	public GameObject EC;
	public GameObject SyringeEffect;
	public Transform SyringeEffectSpawn;
	public bool EnergyFilled;
	public GameObject ElectricBomb;
}
