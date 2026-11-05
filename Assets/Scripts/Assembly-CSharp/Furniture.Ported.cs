using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/Furniture.c (Granny Legacy, IL2CPP x64).
public partial class Furniture
{
	private static void ResetMove(GameObject go, Vector3 pos, Quaternion rot, int layer)
	{
	    if (!go) return;
	    go.transform.position = pos;
	    go.transform.rotation = rot;
	    go.layer = layer;
	    var st = go.GetComponentInChildren<SoundTrigger>();
	    if (st) st.TimesDropped = 0;
	}

	private static void ResetFall(GameObject go, Vector3 pos, Quaternion rot, bool resetSound = false)
	{
	    if (!go) return;
	    go.transform.position = pos;
	    go.transform.rotation = rot;
	    var tf = go.GetComponent<TriggerFall>();
	    if (tf)
	    {
	        tf.Fell = false;
	        if (tf.Rigidbody) tf.Rigidbody.isKinematic = true;
	    }
	    if (resetSound)
	    {
	        var st = go.GetComponentInChildren<SoundTrigger>();
	        if (st) st.TimesDropped = 0;
	    }
	}

	private void Start()
	{
	    if (Table1) { StartPosTable1 = Table1.transform.position; StartRotTable1 = Table1.transform.rotation; }
	    if (Table2) { StartPosTable2 = Table2.transform.position; StartRotTable2 = Table2.transform.rotation; }
	    if (Lamp1) { StartPosLamp1 = Lamp1.transform.position; StartRotLamp1 = Lamp1.transform.rotation; }
	    if (Lamp2) { StartPosLamp2 = Lamp2.transform.position; StartRotLamp2 = Lamp2.transform.rotation; }
	    if (Lamp3) { StartPosLamp3 = Lamp3.transform.position; StartRotLamp3 = Lamp3.transform.rotation; }
	    if (CandleStick1) { StartPosCandleStick1 = CandleStick1.transform.position; StartRotCandleStick1 = CandleStick1.transform.rotation; }
	    if (CandleStick2) { StartPosCandleStick2 = CandleStick2.transform.position; StartRotCandleStick2 = CandleStick2.transform.rotation; }
	    if (CandleStick3) { StartPosCandleStick3 = CandleStick3.transform.position; StartRotCandleStick3 = CandleStick3.transform.rotation; }
	    if (Tavla1) { StartPosTavla1 = Tavla1.transform.position; StartRotTavla1 = Tavla1.transform.rotation; }
	    if (Tavla2) { StartPosTavla2 = Tavla2.transform.position; StartRotTavla2 = Tavla2.transform.rotation; }
	    if (Tavla3) { StartPosTavla3 = Tavla3.transform.position; StartRotTavla3 = Tavla3.transform.rotation; }
	    if (Tavla4) { StartPosTavla4 = Tavla4.transform.position; StartRotTavla4 = Tavla4.transform.rotation; }
	    if (Tavla5) { StartPosTavla5 = Tavla5.transform.position; StartRotTavla5 = Tavla5.transform.rotation; }
	    if (Tav1) { StartPosTav1 = Tav1.transform.position; StartRotTav1 = Tav1.transform.rotation; }
	    if (Tav2) { StartPosTav2 = Tav2.transform.position; StartRotTav2 = Tav2.transform.rotation; }
	    if (Tav3) { StartPosTav3 = Tav3.transform.position; StartRotTav3 = Tav3.transform.rotation; }
	    if (T1) { StartPosT1 = T1.transform.position; StartRotT1 = T1.transform.rotation; }
	    if (T2) { StartPosT2 = T2.transform.position; StartRotT2 = T2.transform.rotation; }
	    if (T3) { StartPosT3 = T3.transform.position; StartRotT3 = T3.transform.rotation; }
	    if (Galge) { StartPosGalge = Galge.transform.position; StartRotGalge = Galge.transform.rotation; }
	    if (Chair) { StartPosChair = Chair.transform.position; StartRotChair = Chair.transform.rotation; }
	    if (Door) { StartPosDoor = Door.transform.position; StartRotDoor = Door.transform.rotation; }
	    if (Box) { StartPosBox = Box.transform.position; StartRotBox = Box.transform.rotation; }
	    if (Ped) { StartPosPed = Ped.transform.position; StartRotPed = Ped.transform.rotation; }
	    if (Doll) { StartPosDoll = Doll.transform.position; StartRotDoll = Doll.transform.rotation; }
	    if (Can) { StartPosCan = Can.transform.position; StartRotCan = Can.transform.rotation; }
	}

	public void CleanUp()
	{
	    var moves = LayerMask.NameToLayer("Moves");
	    ResetMove(Table1, StartPosTable1, StartRotTable1, moves);
	    ResetMove(Table2, StartPosTable2, StartRotTable2, moves);
	    ResetMove(Lamp1, StartPosLamp1, StartRotLamp1, moves);
	    ResetMove(Lamp2, StartPosLamp2, StartRotLamp2, moves);
	    ResetMove(Lamp3, StartPosLamp3, StartRotLamp3, moves);
	    ResetMove(Doll, StartPosDoll, StartRotDoll, moves);
	    ResetMove(Ped, StartPosPed, StartRotPed, moves);
	    ResetMove(Box, StartPosBox, StartRotBox, moves);
	    ResetMove(Chair, StartPosChair, StartRotChair, moves);
	    ResetFall(CandleStick1, StartPosCandleStick1, StartRotCandleStick1, true);
	    ResetFall(CandleStick2, StartPosCandleStick2, StartRotCandleStick2, true);
	    ResetFall(CandleStick3, StartPosCandleStick3, StartRotCandleStick3, true);
	    ResetFall(Tavla1, StartPosTavla1, StartRotTavla1);
	    ResetFall(Tavla2, StartPosTavla2, StartRotTavla2);
	    ResetFall(Tavla3, StartPosTavla3, StartRotTavla3);
	    ResetFall(Tavla4, StartPosTavla4, StartRotTavla4);
	    ResetFall(Tavla5, StartPosTavla5, StartRotTavla5);
	    ResetFall(Tav1, StartPosTav1, StartRotTav1);
	    ResetFall(Tav2, StartPosTav2, StartRotTav2);
	    ResetFall(Tav3, StartPosTav3, StartRotTav3);
	    ResetFall(T1, StartPosT1, StartRotT1);
	    ResetFall(T2, StartPosT2, StartRotT2);
	    ResetFall(T3, StartPosT3, StartRotT3);
	    ResetFall(Galge, StartPosGalge, StartRotGalge, true);
	    ResetFall(Can, StartPosCan, StartRotCan, true);

	    if (UnitsFurni != null)
	    {
	        for (int i = 0; i < UnitsFurni.Length; i++)
	        {
	            if (UnitsFurni[i]) UnitsFurni[i].CleanUpFurniture();
	        }
	    }
	}
}
