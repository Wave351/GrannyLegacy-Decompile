using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/WindowJumping.c (Granny Legacy, IL2CPP x64).
public partial class WindowJumping
{
	// Climbing through the window: the player is hidden while the camera plays the jump animation, then reappears on the other side.
	private void StartJump(Transform at, Vector3 position, Animation camAnim, bool outside)
	{
	    IsJumping = true;
	    Player.transform.rotation = at.rotation;
	    Player.transform.position = position;
	    Window.Play("WindowOpenClose");
	    var fps = Player.GetComponent<MobileFPS>();
	    if (!fps) return;
	    fps.ResetRotation();
	    var anim = Camera.GetComponent<Animation>();
	    if (!anim) return;
	    anim.Play("idle");
	    Camera.transform.parent = camAnim.transform;
	    Camera.transform.localEulerAngles = Vector3.zero;
	    Camera.transform.localPosition = Vector3.zero;
	    camAnim.gameObject.SetActive(true);
	    BGManager.InHouse = !outside;
	    BGManager.InOutside = outside;
	}
	public void JumpOutside()
	{
	    Player.SetActive(false);
	    StartJump(PosOutside, PosOutside.position, CamGoOutside, true);
	    StartCoroutine(GoingOutside());
	}
	private IEnumerator GoingOutside()
	{
	    yield return new WaitForSeconds(Timer1);
	    Land(CamGoOutside, true);
	}
	public void JumpInside()
	{
	    IsJumping = true;
	    Player.transform.rotation = PosInside.rotation;
	    Player.SetActive(false);
	    Vector3 p = PosInside.position;
	    StartJump(PosInside, new Vector3(p.x, 14.2918f, p.z), CamGoInside, false);
	    StartCoroutine(GoingInside());
	}
	private IEnumerator GoingInside()
	{
	    var anim = CamGoInside ? CamGoInside.GetComponent<Animation>() : null;
	    if (!anim) yield break;
	    Timer2 = anim.clip.length;
	    yield return new WaitForSeconds(Timer2);
	    Land(CamGoInside, false);
	}
	private void Land(Animation camAnim, bool outside)
	{
	    Camera.transform.parent = CamPivot.transform;
	    if (!outside) Player.SetActive(true);
	    var anim = Camera ? Camera.GetComponent<Animation>() : null;
	    if (!anim) return;
	    anim.Play("idle");
	    if (outside) Player.SetActive(true);
	    Camera.transform.localPosition = Vector3.zero;
	    Camera.transform.localRotation = Quaternion.identity;
	    WentOutside = outside; IsJumping = false;
	    Camera.transform.localScale = Vector3.one;
	    camAnim.gameObject.SetActive(false);
	}
}
