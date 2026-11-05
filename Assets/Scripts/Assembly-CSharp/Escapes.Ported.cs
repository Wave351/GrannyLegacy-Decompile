using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.ootii.Actors.BoneControllers;

// Ported from Annotated-Pseudocode/Escapes.c (Granny Legacy, IL2CPP x64).
public partial class Escapes
{
	private void Start()
	{
	    if (RobotParent && PosSpawn)
	        RobotParent.transform.position = PosSpawn.position;
	    if (ChestDoor) ChestDoor.SetActive(false);
	}

	public void ElimBody()
	{
	    if (BloodScreens) BloodScreens.SetActive(false);
	    var gFreeze = GameObject.Find("GrannyFreezeDoll(Clone)");
	    if (gFreeze) Destroy(gFreeze);
	    var gpFreeze = GameObject.Find("GrandpaFreezeDoll(Clone)");
	    if (gpFreeze) Destroy(gpFreeze);
	    var shotgun = GameObject.Find("Shotgun(Clone)");
	    if (shotgun) Destroy(shotgun);
	    var gRag = GameObject.Find("GrannyRagdoll(Clone)");
	    if (gRag) Destroy(gRag);
	    var gpRag = GameObject.Find("GranpaRagdoll(Clone)");
	    if (gpRag) Destroy(gpRag);
	}

	private void StopGameplayInputAndUI()
	{
	    if (PauseLogic) PauseLogic.enabled = false;
	    if (GrannyS) GrannyS.SetActive(false);
	    if (GrandpaS) GrandpaS.SetActive(false);
	    if (BG)
	    {
	        BG.GrannyHunting = false;
	        if (BG.HouseMusic) BG.HouseMusic.gameObject.SetActive(false);
	        if (BG.CellarMusic) BG.CellarMusic.gameObject.SetActive(false);
	    }
	    if (MobileFPS)
	    {
	        var crouch = MobileFPS.GetComponent<CrouchHolder>();
	        if (crouch) crouch.enabled = false;
	        var fall = MobileFPS.GetComponent<FallingHolder>();
	        if (fall) fall.enabled = false;
	        var fps = MobileFPS.GetComponent<MobileFPS>();
	        if (fps)
	        {
	            fps.AbleToMove = false;
	            fps.isAllowedToMove = false;
	        }
	    }
	    if (Ray) Ray.enabled = false;
	    if (SpaceT) SpaceT.SetActive(false);
	    if (HandHold) HandHold.SetActive(false);
	    if (MasterK) MasterK.SetActive(false);
	    if (UIElem) UIElem.SetActive(false);
	    if (Texts) Texts.SetActive(false);
	    EscapeCoinsEarn();
	}

	private void RecordAchievements(string escapeType)
	{
	    if (PlayerPrefs.GetInt("GlitchesAndShit") != 0) return;

	    int diff = PlayerPrefs.GetInt("Diff");
	    bool granny = PlayerPrefs.GetInt("Granny") == 1;
	    int gp = PlayerPrefs.GetInt("Grandpa");
	    bool grandpa = gp == 1 || gp == 2;
	    bool both = granny && grandpa;
	    bool either = granny || grandpa;

	    if (diff == 3)
	    {
	        if (escapeType == "Door") PlayerPrefs.SetInt("EX_Door", 1);
	        else if (escapeType == "Car") PlayerPrefs.SetInt("EX_Car", 1);
	        else if (escapeType == "Pipe") PlayerPrefs.SetInt("EX_Pipe", 1);
	        else if (escapeType == "Robo") PlayerPrefs.SetInt("EX_Robo", 1);
	    }

	    if (diff == 0 && either)
	    {
	        PlayerPrefs.SetInt("EASY_Escape", 1);
	        if (escapeType == "Car") PlayerPrefs.SetInt("EASY_Car", 1);
	    }

	    if (diff == 1)
	    {
	        if (either)
	        {
	            PlayerPrefs.SetInt("NOR_Escape", 1);
	            if (escapeType == "Robo") PlayerPrefs.SetInt("NOR_Robo", 1);
	        }
	        if (grandpa && !granny) PlayerPrefs.SetInt("NOR_Grandpa", 1);
	        if (both) PlayerPrefs.SetInt("NOR_Both", 1);
	    }

	    if (diff == 2)
	    {
	        if (escapeType == "Car" && either) PlayerPrefs.SetInt("HARD_Car", 1);
	        if (escapeType == "Pipe" && either) PlayerPrefs.SetInt("HARD_Pipe", 1);
	        if (both)
	        {
	            if (escapeType == "Robo") PlayerPrefs.SetInt("HARD_BothRobo", 1);
	            PlayerPrefs.SetInt("HARD_Both", 1);
	        }
	    }

	    if (diff == 3)
	    {
	        if (both)
	        {
	            if (escapeType == "Robo") PlayerPrefs.SetInt("EX_BothRobo", 1);
	            PlayerPrefs.SetInt("EX_Both", 1);
	        }
	        if (!SeenOnce) PlayerPrefs.SetInt("Unseen", 1);
	        if (!UsedWPOnce) PlayerPrefs.SetInt("NoWP", 1);
	    }

	    if (diff == 4)
	    {
	        if (!DiedOnce) PlayerPrefs.SetInt("NoDie", 1);
	        if (both) PlayerPrefs.SetInt("IM_Both", 1);
	        if (either)
	        {
	            if (escapeType == "Door") PlayerPrefs.SetInt("IM_Door", 1);
	            else if (escapeType == "Car") PlayerPrefs.SetInt("IM_Car", 1);
	            else if (escapeType == "Pipe") PlayerPrefs.SetInt("IM_Pipe", 1);
	            else if (escapeType == "Robo") PlayerPrefs.SetInt("IM_Robo", 1);
	        }
	        if (!SeenOnce) PlayerPrefs.SetInt("IM_NoSee", 1);
	        if (!UsedWPOnce) PlayerPrefs.SetInt("IM_NoWP", 1);
	    }

	    if (diff == 2 && !DiedOnce && both)
	    {
	        PlayerPrefs.SetInt("HARD_NoDyingBoth", 1);
	    }

	    if (diff == 5)
	    {
	        if (granny) PlayerPrefs.SetInt("HOL_Esc", 1);
	        if (escapeType == "Robo" && either) PlayerPrefs.SetInt("HOL_Robo", 1);
	        if (both) PlayerPrefs.SetInt("HOL_Both", 1);
	    }

	    if (HPA && HPA.SetHolRoutesUp)
	    {
	        PlayerPrefs.SetInt("HOL_AllRoutesExtrasBoth", 1);
	    }
	}

	private void FinalizeEscape()
	{
	    ElimBody();
	    if (TimerUISwitch && UINew) TimerUISwitch.transform.parent = UINew.transform;
	    if (PS) PS.TimerStopped = true;
	}

	public void EscapeDoor()
	{
	    StopGameplayInputAndUI();
	    if (Black) Black.CrossFadeAlpha(1f, 0.1f, false);
	    StartCoroutine(WaitDoor());
	    RecordAchievements("Door");
	    FinalizeEscape();
	}

	public void EscapeCar()
	{
	    if (PauseLogic) PauseLogic.enabled = false;
	    if (GrannyS) GrannyS.SetActive(false);
	    if (GrandpaS) GrandpaS.SetActive(false);
	    if (BG)
	    {
	        if (BG.ChaseNormal) BG.ChaseNormal.gameObject.SetActive(false);
	        if (BG.HouseMusic) BG.HouseMusic.gameObject.SetActive(false);
	        if (BG.CellarMusic) BG.CellarMusic.gameObject.SetActive(false);
	    }
	    if (MobileFPS)
	    {
	        var crouch = MobileFPS.GetComponent<CrouchHolder>();
	        if (crouch) crouch.enabled = false;
	        var fall = MobileFPS.GetComponent<FallingHolder>();
	        if (fall) fall.enabled = false;
	        var fps = MobileFPS.GetComponent<MobileFPS>();
	        if (fps) { fps.AbleToMove = false; fps.isAllowedToMove = false; }
	    }
	    if (Ray) Ray.enabled = false;
	    if (SpaceT) SpaceT.SetActive(false);
	    if (HandHold) HandHold.SetActive(false);
	    if (MasterK) MasterK.SetActive(false);
	    if (UIElem) UIElem.SetActive(false);
	    if (Texts) Texts.SetActive(false);
	    EscapeCoinsEarn();
	    if (CamPlayerCar) CamPlayerCar.SetActive(false);

	    if (PlayerPrefs.GetInt("Granny") == 1)
	    {
	        if (GrannyS) GrannyS.SetActive(true);
	        if (EC) EC.GrannyKilled = false;
	        if (Granny)
	        {
	            Granny.SetActive(true);
	            var agent = Granny.GetComponent<NavMeshAgent>();
	            if (agent) agent.enabled = false;
	            var ai = Granny.GetComponent<AI_Granny>();
	            if (ai)
	            {
	                if (ai.EnemyVision) ai.EnemyVision.enabled = false;
	                if (ai.BC) ai.BC.enabled = false;
	                if (ai.EyeLock) ai.EyeLock.SetActive(false);
	                if (ai.Anim && ai.WalkAnim) ai.Anim.CrossFade(ai.WalkAnim.name);
	                ai.enabled = false;
	            }
	            if (PosGrannyEndCar)
	            {
	                Granny.transform.position = PosGrannyEndCar.position;
	                Granny.transform.rotation = PosGrannyEndCar.rotation;
	            }
	            if (CarGrannyParentTransform) Granny.transform.parent = CarGrannyParentTransform;
	        }
	    }

	    int gp = PlayerPrefs.GetInt("Grandpa");
	    if (gp == 1 || gp == 2)
	    {
	        if (GrandpaS) GrandpaS.SetActive(true);
	        if (EC) EC.GrandpaKilled = false;
	        if (Grandpa)
	        {
	            Grandpa.SetActive(true);
	            var agent = Grandpa.GetComponent<NavMeshAgent>();
	            if (agent) agent.enabled = false;
	            var ai = Grandpa.GetComponent<AI_Grandpa>();
	            if (ai)
	            {
	                if (ai.EnemyVision) ai.EnemyVision.enabled = false;
	                if (ai.BC) ai.BC.enabled = false;
	                if (ai.Skin && ai.MeshGood) ai.Skin.sharedMesh = ai.MeshGood;
	                if (ai.Skin && ai.OldShader) ai.Skin.material.shader = ai.OldShader;
	                if (ai.Skin) ai.Skin.material.color = ai.OldColor;
	                if (ai.Anim && ai.WalkAnim) ai.Anim.CrossFade(ai.WalkAnim.name);
	                var foot = ai.Anim ? ai.Anim.GetComponent<EnemyFootstep>() : null;
	                if (foot) foot.CurrentGround = 2f;
	                ai.enabled = false;
	            }
	            if (PosGrandpaEndCar)
	            {
	                Grandpa.transform.position = PosGrandpaEndCar.position;
	                Grandpa.transform.rotation = PosGrandpaEndCar.rotation;
	            }
	            if (CarGrandpaParentTransform) Grandpa.transform.parent = CarGrandpaParentTransform;
	        }
	        if (GrandpaCarCutsceneRun) GrandpaCarCutsceneRun.SetActive(true);
	        if (gp == 1)
	        {
	            if (Cane) Cane.SetActive(true);
	            if (Shotgun) Shotgun.SetActive(false);
	        }
	        else if (gp == 2)
	        {
	            if (Cane) Cane.SetActive(false);
	            if (Shotgun) Shotgun.SetActive(true);
	        }
	    }

	    if (PlayerPrefs.GetInt("Granny") == 1)
	    {
	        if (CamGranny) CamGranny.SetActive(true);
	    }
	    else
	    {
	        if (CamNoGranny) CamNoGranny.SetActive(true);
	    }

	    StartCoroutine(WaitCar());
	    RecordAchievements("Car");
	    ElimBody();
	    RenderSettings.fog = false;
	    if (TimerUISwitch && UINew) TimerUISwitch.transform.parent = UINew.transform;
	    if (PS) PS.TimerStopped = true;
	    if (PlayerPrefs.GetInt("Teddied") == 1 && TeddyRoboPipe) TeddyRoboPipe.SetActive(true);
	}

	public void EscapeCellar()
	{
	    if (PauseLogic) PauseLogic.enabled = false;
	    if (MomSpider)
	    {
	        var align = MomSpider.GetComponent<MomAlign>();
	        if (align) align.enabled = false;
	        var aiMom = MomSpider.GetComponent<AI_MomSpider>();
	        if (aiMom)
	        {
	            if (aiMom.Eyes && aiMom.Eyes.ScreamSee) aiMom.Eyes.ScreamSee.SetActive(false);
	            if (aiMom.Eyes) aiMom.Eyes.enabled = false;
	            aiMom.enabled = false;
	        }
	        var agent = MomSpider.GetComponent<NavMeshAgent>();
	        if (agent) agent.enabled = false;
	        if (MomNewPos)
	        {
	            MomSpider.transform.position = MomNewPos.position;
	            MomSpider.transform.rotation = MomNewPos.rotation;
	        }
	    }
	    if (BoneControllerMom)
	    {
	        var look = BoneControllerMom.GetMotor<com.ootii.Actors.BoneControllers.LookAtMotor>();
	        if (look != null) look.TargetTransform = PlayerTarget; BoneLookFallback.SetTarget(BoneControllerMom, PlayerTarget);
	        BoneControllerMom.enabled = true;
	    }
	    if (MomBodyRSpider) MomBodyRSpider.localRotation = Quaternion.identity;
	    if (MomAnim) MomAnim.CrossFade("MomIdle");

	    if (GrannyS) GrannyS.SetActive(false);
	    if (GrandpaS) GrandpaS.SetActive(false);
	    if (BG)
	    {
	        BG.GrannyHunting = false;
	        BG.MomHunting = false;
	        if (BG.HouseMusic) BG.HouseMusic.gameObject.SetActive(false);
	        if (BG.CellarMusic) BG.CellarMusic.gameObject.SetActive(false);
	    }
	    if (MobileFPS)
	    {
	        var crouch = MobileFPS.GetComponent<CrouchHolder>();
	        if (crouch) crouch.enabled = false;
	        var fall = MobileFPS.GetComponent<FallingHolder>();
	        if (fall) fall.enabled = false;
	        var fps = MobileFPS.GetComponent<MobileFPS>();
	        if (fps) { fps.AbleToMove = false; fps.isAllowedToMove = false; }
	    }
	    if (Ray) Ray.enabled = false;
	    if (SpaceT) SpaceT.SetActive(false);
	    if (HandHold) HandHold.SetActive(false);
	    if (MasterK) MasterK.SetActive(false);
	    if (UIElem) UIElem.SetActive(false);
	    if (Texts) Texts.SetActive(false);
	    if (SpiderCellarMusic) SpiderCellarMusic.SetActive(true);
	    EscapeCoinsEarn();
	    if (ScreenEffects) ScreenEffects.NormalSee = false;
	    if (Black) Black.CrossFadeAlpha(1f, 0.4f, false);
	    StartCoroutine(WaitCellar());
	    RecordAchievements("Pipe");
	    FinalizeEscape();
	    if (PlayerPrefs.GetInt("Teddied") == 1 && TeddyRoboPipe) TeddyRoboPipe.SetActive(true);
	}

	public void EscapeRobo()
	{
	    StopGameplayInputAndUI();
	    if (ScreenEffects) ScreenEffects.NormalSee = false;
	    if (Black) Black.CrossFadeAlpha(1f, 0.4f, false);
	    StartCoroutine(RoboGrandparents());
	    StartCoroutine(WaitRobo());
	    RecordAchievements("Robo");
	    FinalizeEscape();
	    if (PlayerPrefs.GetInt("Teddied") == 1 && TeddyRoboPipe) TeddyRoboPipe.SetActive(true);
	}

	public void EscapeCoinsEarn()
	{
	    float coins = PlayerPrefs.GetFloat("Coins", 0f);
	    int diff = PlayerPrefs.GetInt("Diff");
	    int granny = PlayerPrefs.GetInt("Granny");
	    int grandpa = PlayerPrefs.GetInt("Grandpa");
	    bool hasGranny = granny == 1;
	    bool hasGrandpa = grandpa == 1 || grandpa == 2;
	    bool slendrina = PlayerPrefs.GetInt("SlendrinaEnemy") == 1;

	    if (diff == 0)
	    {
	        float add = (hasGranny || hasGrandpa) ? 2f : 1f;
	        coins += add;
	        if (slendrina) coins += 0.5f;
	    }
	    else if (diff == 1)
	    {
	        float add = (hasGranny || hasGrandpa) ? 3f : 1.5f;
	        coins += add;
	        if (slendrina) coins += 0.75f;
	    }
	    else if (diff == 2)
	    {
	        float add = (hasGranny || hasGrandpa) ? 4f : 2f;
	        coins += add;
	        if (slendrina) coins += 1f;
	    }
	    else if (diff == 3)
	    {
	        float add = (hasGranny && hasGrandpa) ? 6.5f : ((hasGranny || hasGrandpa) ? 5f : 0f);
	        coins += add;
	        if (slendrina) coins += 1.5f;
	    }
	    else if (diff == 4)
	    {
	        float add = (hasGranny && hasGrandpa) ? 8.5f : ((hasGranny || hasGrandpa) ? 7f : 0f);
	        coins += add;
	        if (slendrina) coins += 2f;
	    }
	    else if (diff == 5)
	    {
	        float add = (hasGranny && hasGrandpa) ? 12.5f : ((hasGranny || hasGrandpa) ? 10f : 0f);
	        coins += add;
	        if (slendrina) coins += 3f;
	    }
	    PlayerPrefs.SetFloat("Coins", coins);
	}

	private IEnumerator WaitDoor()
	{
	    yield return new WaitForSeconds(5f);
	    SceneManager.LoadScene("EndScene");
	}

	private IEnumerator WaitCar()
	{
	    yield return new WaitForSeconds(8.5f);
	    if (Black) Black.CrossFadeAlpha(1f, 2f, false);
	    yield return new WaitForSeconds(3f);
	    if (TheEndT) TheEndT.SetActive(true);
	    yield return new WaitForSeconds(4f);
	    if (TheEndT) TheEndT.SetActive(false);
	    yield return new WaitForSeconds(2f);
	    SceneManager.LoadScene("Menu");
	}

	private IEnumerator WaitCellar()
	{
	    yield return new WaitForSeconds(1.3f);
	    if (MobileFPS) MobileFPS.SetActive(false);
	    if (Black) Black.CrossFadeAlpha(0f, 1.5f, false);
	    if (CellarCam)
	    {
	        CellarCam.gameObject.SetActive(true);
	        if (PlayerPrefs.GetInt("Granny") == 1)
	        {
	            CellarCam.Play("escapeSceneCam");
	            if (GrannyS) GrannyS.SetActive(true);
	            if (EC) EC.GrannyKilled = false;
	            if (Granny)
	            {
	                Granny.SetActive(true);
	                var agent = Granny.GetComponent<NavMeshAgent>();
	                if (agent) agent.enabled = false;
	                var ai = Granny.GetComponent<AI_Granny>();
	                if (ai)
	                {
	                    if (ai.EnemyVision) ai.EnemyVision.enabled = false;
	                    if (ai.BC) ai.BC.enabled = false;
	                    if (ai.EyeLock) ai.EyeLock.SetActive(false);
	                    if (ai.Anim) ai.Anim.Play("Granny_SpiderCellarEnd");
	                    ai.enabled = false;
	                }
	                if (PosGrannyEndSC)
	                {
	                    Granny.transform.position = PosGrannyEndSC.position;
	                    Granny.transform.rotation = PosGrannyEndSC.rotation;
	                }
	            }
	        }
	        else
	        {
	            CellarCam.Play("escapeSceneCamNoGranny");
	        }
	    }
	    yield return new WaitForSeconds(13f);
	    if (Black) Black.CrossFadeAlpha(1f, 2f, false);
	    yield return new WaitForSeconds(3f);
	    if (TheEndT) TheEndT.SetActive(true);
	    yield return new WaitForSeconds(4f);
	    if (TheEndT) TheEndT.SetActive(false);
	    yield return new WaitForSeconds(2f);
	    SceneManager.LoadScene("Menu");
	}

	private IEnumerator WaitRobo()
	{
	    yield return new WaitForSeconds(1.3f);
	    RenderSettings.fog = false;
	    if (RoboAnim) RoboAnim.Play("EscapeRobot");
	    if (MobileFPS) MobileFPS.SetActive(false);
	    if (SoundAttic) SoundAttic.SetActive(false);
	    if (Black) Black.CrossFadeAlpha(0f, 1.5f, false);
	    yield return new WaitForSeconds(16f);
	    if (Black) Black.CrossFadeAlpha(1f, 0f, false);
	    yield return new WaitForSeconds(3f);
	    if (TheEndT) TheEndT.SetActive(true);
	    yield return new WaitForSeconds(4f);
	    if (TheEndT) TheEndT.SetActive(false);
	    yield return new WaitForSeconds(2f);
	    SceneManager.LoadScene("Menu");
	}

	private IEnumerator RoboGrandparents()
	{
	    yield return new WaitForSeconds(8f);
	    if (PlayerPrefs.GetInt("Granny") == 1)
	    {
	        if (GrannyS) GrannyS.SetActive(true);
	        if (EC) EC.GrannyKilled = false;
	        if (Granny)
	        {
	            Granny.SetActive(true);
	            var agent = Granny.GetComponent<NavMeshAgent>();
	            if (agent) agent.enabled = false;
	            var ai = Granny.GetComponent<AI_Granny>();
	            if (ai)
	            {
	                if (ai.EnemyVision) ai.EnemyVision.enabled = false;
	                if (ai.BC) ai.BC.enabled = false;
	                if (ai.EyeLock) ai.EyeLock.SetActive(false);
	                if (ai.Anim && ai.Search) ai.Anim.CrossFade(ai.Search.name);
	                ai.enabled = false;
	            }
	            if (PosGrannyEnd)
	            {
	                Granny.transform.position = PosGrannyEnd.position;
	                Granny.transform.rotation = PosGrannyEnd.rotation;
	            }
	        }
	    }

	    int gp = PlayerPrefs.GetInt("Grandpa");
	    if (gp == 1 || gp == 2)
	    {
	        if (GrandpaS) GrandpaS.SetActive(true);
	        if (EC) EC.GrandpaKilled = false;
	        if (Grandpa)
	        {
	            Grandpa.SetActive(true);
	            var agent = Grandpa.GetComponent<NavMeshAgent>();
	            if (agent) agent.enabled = false;
	            var ai = Grandpa.GetComponent<AI_Grandpa>();
	            if (ai)
	            {
	                if (ai.EnemyVision) ai.EnemyVision.enabled = false;
	                if (ai.BC) ai.BC.enabled = false;
	                if (ai.Skin && ai.MeshGood) ai.Skin.sharedMesh = ai.MeshGood;
	                if (ai.Skin && ai.OldShader) ai.Skin.material.shader = ai.OldShader;
	                if (ai.Skin) ai.Skin.material.color = ai.OldColor;
	                if (ai.Anim && ai.Search) ai.Anim.CrossFade(ai.Search.name);
	                ai.enabled = false;
	            }
	            if (PosGrandpaEnd)
	            {
	                Grandpa.transform.position = PosGrandpaEnd.position;
	                Grandpa.transform.rotation = PosGrandpaEnd.rotation;
	            }
	        }
	    }
	}
}
