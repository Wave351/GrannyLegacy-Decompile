using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Skull.c (Granny Legacy, IL2CPP x64).
public partial class Skull
{
	public Skull() { RayDis = 15f; LookSpeed = 5f; AbleTo = true; }
	// Wall skull: laughs once when it sees the player (then 7 s cooldown) and keeps turning to face them while seen.
	private void Update()
	{
	    RayCast.LookAt(Player);
	    RaycastHit hit;
	    bool seen = Physics.Raycast(RayCast.position, RayCast.forward, out hit, RayDis, LayerMasks) && hit.collider.gameObject.tag == "Player";
	    if (!seen) { Attentive = false; return; }
	    if (!Attentive)
	    {
	        if (AbleTo)
	        {
	            Attentive = true;
	            Laugh.Play("SkullLaugh");
	            GetComponent<AudioSource>().Play();
	            AbleTo = false;
	            StartCoroutine(TimerAfterSkull());
	        }
	        if (!Attentive) return;
	    }
	    Vector3 dir = Player.position - transform.position;
	    dir.y = 0;
	    if (dir == Vector3.zero) return;
	    Vector3 target = Quaternion.LookRotation(dir).eulerAngles;
	    Vector3 local = transform.localRotation.eulerAngles;
	    transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(local.x, target.y, local.z), Time.deltaTime * LookSpeed);
	}
	private IEnumerator TimerAfterSkull()
	{
	    yield return new WaitForSeconds(7f);
	    AbleTo = true;
	}
}
