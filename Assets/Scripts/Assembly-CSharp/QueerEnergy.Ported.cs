using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/QueerEnergy.c (Granny Legacy, IL2CPP x64).
public partial class QueerEnergy
{
	private void Start()
	{
	    rigid.freezeRotation = true;
	    Vector3 dir = UnityEngine.Random.onUnitSphere;
	    dir.y = Mathf.Clamp(dir.y, .1f, .5f);
	    rigid.AddForce(dir * 8, ForceMode.VelocityChange);
	}
	private void Update()
	{
	    if (rigid.velocity.magnitude < 1)
	    {
	        Vector3 dir = UnityEngine.Random.onUnitSphere;
	        dir.y = Mathf.Clamp(dir.y, -.1f, .1f);
	        rigid.velocity = dir * 10;
	    }
	    if (rigid.velocity.sqrMagnitude > .1f) transform.forward = rigid.velocity.normalized;
	}
	private void OnCollisionEnter(Collision collision)
	{
	    float speed = UnityEngine.Random.Range(4f, 15f);
	    Vector3 dir = Vector3.Reflect(rigid.velocity.normalized, collision.contacts[0].normal);
	    dir = (dir + UnityEngine.Random.insideUnitSphere * .6f).normalized;
	    dir.y = Mathf.Clamp(dir.y, -.1f, .1f);
	    rigid.velocity = dir * 4 * speed;
	    Instantiate(bumpEffect, transform.position, transform.rotation);
	    UnityEngine.Random.Range(0, 100);
	}
}
