using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ImpactBodies.c (Granny Legacy, IL2CPP x64).
public partial class ImpactBodies
{
	public ImpactBodies()
	{
		RadiusImpact = 20f;
		PushImpact = 15f;
	}

	private void Start()
	{
		foreach (Collider hit in Physics.OverlapSphere(transform.position, RadiusImpact))
		{
			Rigidbody body = hit.GetComponent<Rigidbody>();
			if (body != null)
			{
				Vector3 direction = (hit.transform.position - transform.position).normalized;
				body.AddForce(direction * PushImpact, ForceMode.Impulse);
			}
		}
	}
}
