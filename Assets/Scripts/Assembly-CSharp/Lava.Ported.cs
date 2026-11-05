using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Lava.c (Granny Legacy, IL2CPP x64).
public partial class Lava
{
	public Lava() { lavaRiseSpeed = 2.5f; }
	private void Start()
	{
	    int diff = PlayerPrefs.GetInt("Diff");
	    var orange = new Color(1, .5134416f, 0, 1);
	    foreach (var t in texts) t.color = orange;
	    foreach (var e in uiEl) e.color = orange;
	    switch (diff)
	    {
	        case 0: lavaRiseSpeed = 1.15f; break;
	        case 1: lavaRiseSpeed = 1.4f; break;
	        case 2: lavaRiseSpeed = 1.6f; break;
	        case 3: lavaRiseSpeed = 1.75f; break;
	        case 4: lavaRiseSpeed = 1.95f; break;
	        case 5: lavaRiseSpeed = 2.2f; break;
	    }
	}
	// Lava mode: outside the secret cellar the lava rises in steps (riseTimerMax apart, with a warning 15 s before),
	// stays up for unriseTimerMax, and after the fifth "ultimate" rise sinks back. Picking up each weapon spawns a radio message.
	private void Update()
	{
	    var audio = GetComponent<AudioSource>();
	    if (!risen)
	    {
	        if (!SC.Entered_SC) riseTimer += Time.deltaTime;
	        if (15 <= riseTimer && !warned)
	        {
	            warned = true;
	            if (lavaText) lavaText.GetComponent<TMP_Text>().text = "The <u>lava</u> is <b>rising</b> soon...";
	            lavaText.Add();
	            lavaText.gameObject.SetActive(true);
	        }
	        if (riseTimerMax <= riseTimer)
	        {
	            riseTimer = 0;
	            risen = true;
	            string[] messages = { "The <u>lava</u> is <b>rising!</b>", "The <u>lava</u> is <b>rising!</b> +1", "The <u>lava</u> is <b>rising!</b> +2",
	                "The <u>lava</u> is <b>rising!</b> +3", "The <u>lava</u> is <b>rising!</b> +ULTIMATE RISE" };
	            int step = (int)pointsReached;
	            if (step >= 0 && step <= 4 && step == pointsReached)
	            {
	                if (step == 4) audio.PlayOneShot(lavaMaxSound);
	                pointsReached = step + 1;
	                lavaText.GetComponent<TMP_Text>().text = messages[step];
	            }
	            lavaText.Add();
	            lavaText.gameObject.SetActive(true);
	            particleLavaBurst.Play();
	            audio.PlayOneShot(burstLava);
	            audio.PlayOneShot(lavaComeUp);
	            warned = false;
	        }
	        if (didTop)
	        {
	            if (lavaPoint_1 < lavaVerticalHeight) lavaVerticalHeight -= Time.deltaTime * lavaRiseSpeed;
	            else { didTop = false; riseTimer = 0; }
	        }
	        if (.8f < lavaAudios.pitch) lavaAudios.pitch -= Time.deltaTime;
	    }
	    else
	    {
	        riseTimer += Time.deltaTime;
	        if (unriseTimerMax <= riseTimer)
	        {
	            riseTimer = 0; risen = false; warned = false;
	            if (pointsReached == 5)
	            {
	                didTop = true;
	                pointsReached = 0;
	                if (lavaText) lavaText.GetComponent<TMP_Text>().text = "The <u>lava</u> is returning to its previous state.";
	                lavaText.Add();
	                lavaText.gameObject.SetActive(true);
	            }
	        }
	        float[] points = { lavaPoint_2, lavaPoint_3, lavaPoint_4, lavaPoint_5, lavaPoint_6 };
	        int p = (int)pointsReached;
	        if (p >= 1 && p <= 5 && p == pointsReached && lavaVerticalHeight < points[p - 1])
	            lavaVerticalHeight += Time.deltaTime * lavaRiseSpeed;
	        if (lavaAudios.pitch < 1.3f) lavaAudios.pitch += Time.deltaTime;
	    }
	    lavaBody.transform.localPosition = new Vector3(0, lavaVerticalHeight, 0);
	    // Red screen while the player burns.
	    var c = lavaScreen.color;
	    if (!PS.Killed) { if (0 < c.a) c.a -= Time.deltaTime; }
	    else if (c.a < 1.01f) c.a += Time.deltaTime * 1.5f;
	    lavaScreen.color = c;
	    Radio(crossbowHand, ref pickedCrossBow);
	    Radio(shotgunHand, ref pickedShotgun);
	    Radio(pepperHand, ref pickedPepperSpray);
	    Radio(freezeHand, ref pickedFreeze);
	    Radio(doubleHand, ref pickedDoubleShotgun);
	    if (bedCam.activeSelf) NewDayReset();
	}
	private void Radio(GameObject hand, ref bool picked)
	{
	    if (!hand.activeSelf || picked) return;
	    picked = true;
	    Instantiate(radioTF, PS.CameraPivot.transform.position, Quaternion.identity);
	}
	public void NewDayReset()
	{
	    didTop = false; warned = false;
	    pointsReached = 0;
	    risen = false;
	    lavaVerticalHeight = -16.5f;
	}
}
