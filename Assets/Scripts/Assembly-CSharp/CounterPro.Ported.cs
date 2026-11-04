using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/CounterPro.c (Granny Legacy, IL2CPP x64).
public partial class CounterPro
{
	public CounterPro() { CurrentList = 1; }
	private void Update()
	{
	    int page = CurrentList == 1 ? 1 : CurrentList == 2 ? 2 : CurrentList == 3 ? 3 : CurrentList == 4 ? 4 : 0;
	    if (page == 0) return;
	    ListNumb.text = page + " / 4";
	    BackB.SetActive(page != 1); NextB.SetActive(page != 4);
	    L1.SetActive(page == 1); L2.SetActive(page == 2); L3.SetActive(page == 3); L4.SetActive(page == 4);
	}
	public void Back() { CurrentList -= 1; }
	public void Next() { CurrentList += 1; }
}
