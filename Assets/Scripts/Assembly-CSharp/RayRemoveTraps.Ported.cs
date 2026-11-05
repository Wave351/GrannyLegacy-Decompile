using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/RayRemoveTraps.c (Granny Legacy, IL2CPP x64).
public partial class RayRemoveTraps
{
	public RayRemoveTraps() { RaycastDis = 5; SpeedRev = .2f; }
	// Holding the interact key while looking at the bear trap that caught the player (or a web) frees them.
	private void Update()
	{
	    FillBar.fillAmount = ValueClock;
	    RaycastHit hit;
	    var falling = Player.GetComponent<FallingHolder>();
	    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastDis, LS) && !Paused.IsPaused
	        && !falling.isFalling && !PlayerStatus.IsJumpscared)
	    {
	        if (hit.collider.CompareTag("BearTrapActivated"))
	        {
	            var trap = hit.collider.GetComponent<BearTrapLogic>();
	            if (trap.TrappedPlayer && !trap.ItemCaughtIt && Trapped)
	            {
	                TrapRemoval.SetActive(true);
	                if (!Input.GetKey(KeyInteract)) { ValueClock = 0; return; }
	                ValueClock += Time.deltaTime * trap.removeSpeed;
	                if (ValueClock < 1) return;
	                AudioSource.PlayClipAtPoint(PlayerPrefs.GetInt("Textures") == 1 ? KillTrapNM : KillTrap, hit.collider.transform.position);
	                Trapped = false;
	                Destroy(hit.collider.gameObject);
	                ScreenEffects.Hurt = false;
	                var fps = Player ? Player.GetComponent<MobileFPS>() : null;
	                if (fps) fps.AbleToMove = true;
	                return;
	            }
	        }
	        else if (hit.collider.name == "WebTrap" && !hit.collider.GetComponent<WebLogic>().Anim.isPlaying)
	        {
	            TrapRemoval.SetActive(true);
	            if (!Input.GetKey(KeyCode.F)) { ValueClock = 0; return; }
	            var web = hit.collider.GetComponent<WebLogic>();
	            if (web) web.RemoveAttempt();
	            return;
	        }
	    }
	    ValueClock = 0;
	    TrapRemoval.SetActive(false);
	}
}
