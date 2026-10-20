using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/SprayPower.c (Granny Legacy, IL2CPP x64).
public partial class SprayPower
{
	private void OnParticleCollision(GameObject hit)
	{
		ParticleSystem system = GetComponent<ParticleSystem>();
		if (system == null || !system.isPlaying)
		{
			return;
		}
		if (hit.tag == "spiderMom" && !AIMom.IsHit)
		{
			AIMom.HitMom();
		}
		if (hit.name == "PepperGrannyEye" && !hit.GetComponent<P_hitGranny>().Granny.IsBlind)
		{
			hit.GetComponent<P_hitGranny>().HitAttempt();
		}
		if (hit.name == "CrowNest" && !hit.GetComponentInParent<CrowManage>().HurtTemp)
		{
			hit.GetComponentInParent<CrowManage>().HurtBird();
		}
		if (hit.tag == "granpa" && !PHG.Grandpa.IsBlind)
		{
			PHG.HitAttempt();
		}
		if (hit.tag != "Santa")
		{
			return;
		}
		GameObject santa = GameObject.Find("LittleSanta");
		if (santa != null)
		{
			LittleFucker little = santa.GetComponent<LittleFucker>();
			if (little != null && !little.Hurt)
			{
				little.KnockedOut();
			}
		}
	}
}
