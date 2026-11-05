using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/PickRay.c (Granny Legacy, IL2CPP x64).
public partial class PickRay
{
	    private void ShowNeedText(string text)
	    {
	        StopSetOfTexts();
	        if (T_NeedShit != null)
	        {
	            T_NeedShit.SetActive(true);
	            var tm = T_NeedShit.GetComponent<TextManager>();
	            if (tm != null) tm.Add();
	            var tmp = T_NeedShit.GetComponent<TMP_Text>();
	            if (tmp != null) tmp.text = text;
	        }
	    }

	    public void HandleButtonClick()
	    {
	        buttonClicked = true;
	    }

	    private void HandleButtonClick1()
	    {
	        buttonClicked2 = true;
	    }

	    public void StopSetOfTexts()
	    {
	        if (T_NeedShit != null)
	        {
	            var tm = T_NeedShit.GetComponent<TextManager>();
	            if (tm != null) tm.Stop();
	        }
	    }

	    public void TurnOffItemTexts()
	    {
	        if (RotateWinchButton != null) RotateWinchButton.SetActive(false);
	        if (T_HighlightItem != null) T_HighlightItem.SetActive(false);
	    }

	    public void DeathAction()
	    {
	        CheckItemDropping();
	        if (T_NeedShit != null)
	        {
	            var tm = T_NeedShit.GetComponent<TextManager>();
	            if (tm != null) tm.Stop();
	        }
	        if (RotateWinchButton != null) RotateWinchButton.SetActive(false);
	        if (T_HighlightItem != null) T_HighlightItem.SetActive(false);
	        if (ShootCB1 != null) ShootCB1.SetActive(false);
	        if (ShootB1 != null) ShootB1.SetActive(false);
	        if (ShootB2 != null) ShootB2.SetActive(false);
	        if (UseB != null) UseB.SetActive(false);
	    }

	    public void PickShotgun()
	    {
	        if (MoreAmmo1 != null) MoreAmmo1.SetActive(true);
	        var audio = GetComponent<AudioSource>();
	        if (!LoadedGun)
	        {
	            if (audio != null && LoadGun != null) audio.PlayOneShot(LoadGun);
	            if (H_SG != null)
	            {
	                var anim = H_SG.GetComponentInChildren<Animation>();
	                if (anim != null) anim.Play("OpenEmpty");
	            }
	        }
	        else
	        {
	            if (audio != null && LoadPrepareGun != null) audio.PlayOneShot(LoadPrepareGun);
	            if (H_SG != null)
	            {
	                var anim = H_SG.GetComponentInChildren<Animation>();
	                if (anim != null) anim.Play("Load");
	            }
	        }
	    }

	    public void PickShooty()
	    {
	        if (MoreAmmo1 != null) MoreAmmo1.SetActive(true);
	        if (MoreAmmo2 != null) MoreAmmo2.SetActive(true);
	        if (AmmoAmount == 0f)
	        {
	            if (H_DSH != null)
	            {
	                var anim = H_DSH.GetComponent<Animation>();
	                if (anim != null)
	                {
	                    anim.Stop();
	                    anim.CrossFade("PickUpShootyEmpty");
	                }
	            }
	        }
	        else if (AmmoAmount == 1f || AmmoAmount == 2f)
	        {
	            if (H_DSH != null)
	            {
	                var anim = H_DSH.GetComponent<Animation>();
	                if (anim != null)
	                {
	                    anim.Stop();
	                    anim.CrossFade("PickUpShooty");
	                }
	            }
	        }
	    }

	    public void PickBaton()
	    {
	        buttonClicked = false;
	        CheckItemDropping();
	        TurnOffItemTexts();
	        if (H_BATON != null) H_BATON.SetActive(true);
	        if (Drop1 != null) Drop1.SetActive(true);
	        if (Ring != null) Ring.SetActive(false);
	        var audio = GetComponent<AudioSource>();
	        if (audio != null && PickUp != null)
	        {
	            audio.PlayOneShot(PickUp);
	        }
	    }

	    public void FellObjectBackyard()
	    {
	        if (T_NeedShit == null) return;
	        var tm = T_NeedShit.GetComponent<TextManager>();
	        if (tm != null)
	        {
	            tm.Stop();
	            T_NeedShit.SetActive(true);
	            tm.Add();
	            int curVer = PlayerPrefs.GetInt("CURVERSION");
	            var tmp = T_NeedShit.GetComponent<TMP_Text>();
	            if (tmp != null)
	            {
	                if (curVer == 0 || curVer == 1 || curVer == 2)
	                {
	                    tmp.text = "Item fell out of bounds. It has been teleported in a bedroom";
	                }
	                else
	                {
	                    tmp.text = "Item fell out of bounds. It has been teleported in the backyard";
	                }
	            }
	        }
	    }

	    public void CheckItemDropping()
	    {
	        if (H_CR != null && H_CR.activeSelf)
	        {
	            H_CR.SetActive(false);
	            if (IsCharged)
	            {
	                if (DropP != null && ItemDrop != null)
	                {
	                    GameObject dartObj = Instantiate(ItemDrop, DropP.position, DropP.rotation);
	                    if (dartObj != null)
	                    {
	                        var isData = dartObj.GetComponent<ItemSpawn>();
	                        if (isData != null)
	                        {
	                            isData.CountItem = 37f;
	                        }
	                    }
	                }
	                IsCharged = false;
	                if (DartHasEnergy)
	                {
	                    DartHasEnergy = false;
	                }
	            }
	            buttonClicked2 = false;
	            if (Drop1 != null) Drop1.SetActive(false);
	            if (DropP != null && ItemDrop != null)
	            {
	                GameObject crossbowObj = Instantiate(ItemDrop, DropP.position, DropP.rotation);
	                if (crossbowObj != null)
	                {
	                    var isData = crossbowObj.GetComponent<ItemSpawn>();
	                    if (isData != null)
	                    {
	                        isData.CountItem = 1f;
	                    }
	                }
	            }
	            return;
	        }

	        if (H_DSH != null && H_DSH.activeSelf)
	        {
	            H_DSH.SetActive(false);
	            buttonClicked2 = false;
	            if (Drop1 != null) Drop1.SetActive(false);
	            if (DropP != null && ItemDrop != null)
	            {
	                GameObject shootyObj = Instantiate(ItemDrop, DropP.position, DropP.rotation);
	                if (shootyObj != null)
	                {
	                    var isData = shootyObj.GetComponent<ItemSpawn>();
	                    if (isData != null)
	                    {
	                        isData.CountItem = 30f;
	                        if (ShotgunHandMain != null)
	                        {
	                            ShotgunHandMain.localPosition = ShotgunHandMain_ZeroPos;
	                            ShotgunHandMain.localEulerAngles = ShotgunHandMain_ZeroRot;
	                        }
	                        if (ShotgunHandPipes != null)
	                        {
	                            Vector3 curPipesRot = ShotgunHandPipes.localEulerAngles;
	                            ShotgunHandPipes.localEulerAngles = new Vector3(9.6f, curPipesRot.y, curPipesRot.z);
	                        }
	                    }
	                }
	            }
	            return;
	        }

	        if (H_FT != null && H_FT.activeSelf)
	        {
	            H_FT.SetActive(false);
	            buttonClicked2 = false;
	            if (Drop1 != null) Drop1.SetActive(false);
	            if (DropPFreezeTrap != null && O_FT != null)
	            {
	                GameObject ftObj = Instantiate(O_FT, DropPFreezeTrap.position, DropPFreezeTrap.rotation);
	                if (ftObj != null)
	                {
	                    var rb = ftObj.GetComponent<Rigidbody>();
	                    if (rb != null)
	                    {
	                        rb.AddForce(Vector3.forward * DropForceItem, ForceMode.Impulse);
	                    }
	                }
	            }
	            return;
	        }

	        if (H_PTRAP != null && H_PTRAP.activeSelf)
	        {
	            H_PTRAP.SetActive(false);
	            buttonClicked2 = false;
	            if (Drop1 != null) Drop1.SetActive(false);
	            if (DropPFreezeTrap != null && O_PTRAP != null)
	            {
	                GameObject ptObj = Instantiate(O_PTRAP, DropPFreezeTrap.position, DropPFreezeTrap.rotation);
	                if (ptObj != null)
	                {
	                    var rb = ptObj.GetComponent<Rigidbody>();
	                    if (rb != null)
	                    {
	                        rb.AddForce(Vector3.forward * DropForceItem, ForceMode.Impulse);
	                    }
	                }
	            }
	            return;
	        }

	        buttonClicked2 = false;
	        if (Drop1 != null) Drop1.SetActive(false);
	        if (Inventory != null)
	        {
	            Inventory.DropLogic();
	        }
	    }

	    public void ShootGun1()
	    {
	        if (RayShoot2 == null) return;
	        Vector3 dir = RayShoot2.TransformDirection(Vector3.forward);
	        if (ShootHead != null) ShootHead.Play("shootGun");
	        if (MuzzleFront != null && DropP != null) Instantiate(MuzzleFront, DropP.position, DropP.rotation);
	        if (Noisy1 != null && Player != null) Instantiate(Noisy1, Player.transform.position, Player.transform.rotation);
	        if (Noisy2 != null && Player != null) Instantiate(Noisy2, Player.transform.position, Player.transform.rotation);

	        if (!Physics.Raycast(RayShoot2.position, dir, out RaycastHit hit, Gun2RayDistance, GunLayers))
	        {
	            return;
	        }

	        if (IsPOWER && ExplosionGas != null)
	        {
	            Instantiate(ExplosionGas, hit.point, hit.collider.transform.rotation);
	        }

	        string hitName = hit.collider.gameObject.name;
	        string hitTag = hit.collider.gameObject.tag;

	        if (hitName == "CandleStick" || hitName == "CandleStick2" || hitName == "CandleStickCellar")
	        {
	            var rb = hit.collider.GetComponent<Rigidbody>();
	            if (rb != null) rb.isKinematic = false;
	        }

	        if (hitName == "WaterWheelMovable")
	        {
	            GameObject trigger = GameObject.Find("WaterWheelJumpscareTrigger");
	            if (trigger != null)
	            {
	                var vtp = trigger.GetComponent<VentTriggerPush>();
	                if (vtp != null) vtp.HitI();
	            }
	        }

	        if (hitName == "Cowleg")
	        {
	            var cr = hit.collider.GetComponent<CowlegRuin>();
	            if (cr != null) cr.RuinMeat();
	        }

	        if (hitName == "ShootableTargetCollider")
	        {
	            var sdh = hit.collider.GetComponent<StealDoorHit>();
	            if (sdh != null) sdh.OpenIt();
	        }

	        if (hitName == "SpiderAtticParent")
	        {
	            var sp = hit.collider.GetComponent<AtticSpider>();
	            if (sp != null) sp.Dies();
	        }

	        if (hitName == "RobotProtect")
	        {
	            var rob = hit.collider.GetComponent<AI_RobotEnemy>();
	            if (rob != null) rob.KillRobot();
	        }

	        if (hitName == "PlankAttic" || hitName == "PlankaOH")
	        {
	            var rb = hit.collider.GetComponent<Rigidbody>();
	            if (rb != null) rb.isKinematic = false;
	            var seed = hit.collider.gameObject.AddComponent<ItemSeedData>();
	            seed.itemName = "Plank";
	            hit.collider.gameObject.name = "PlankaKnocked";
	            hit.collider.gameObject.AddComponent<OutlineCheck>();
	        }

	        if (hitName == "SpiderAtticButtonClose")
	        {
	            var asb = hit.collider.GetComponent<AtticSpiderButton>();
	            if (asb != null) asb.StopSpider();
	        }

	        if (hitName == "CameraBase")
	        {
	            var cs = hit.collider.GetComponent<CameraSmack>();
	            if (cs != null) cs.SmackCam();
	        }

	        if (hitTag == "BearTrapActivated")
	        {
	            var btl = hit.collider.GetComponent<BearTrapLogic>();
	            if (btl != null) btl.ShootGun();
	        }

	        if (hitName == "Rat1" || hitName == "Rat2")
	        {
	            var rat = hit.collider.GetComponent<RatEnemy>();
	            if (rat != null) rat.WaitingTime = 120f;
	        }

	        if (hitName == "CrowNest" || hitName == "CrowEat")
	        {
	            if (CM != null) CM.KillAssBird();
	        }

	        if (hitTag == "mom")
	        {
	            if (EnemyControl != null && !EnemyControl.MomKilled)
	            {
	                EnemyControl.MomShoot();
	            }
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "bloodshootable")
	        {
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "spiderMom")
	        {
	            var mom = hit.collider.GetComponent<AI_MomSpider>();
	            if (mom != null && !mom.IsHit)
	            {
	                mom.HitMom();
	            }
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granpa")
	        {
	            var gp = hit.collider.GetComponent<AI_Grandpa>();
	            if (EnemyControl != null && !EnemyControl.GrandpaKilled)
	            {
	                if (gp != null && !gp.CaughtPlayer)
	                {
	                    EnemyControl.GrandpaDeathS();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granpasoffa")
	        {
	            var gp = hit.collider.GetComponent<AI_Grandpa>();
	            if (EnemyControl != null && !EnemyControl.GrandpaKilled)
	            {
	                if (gp != null && !gp.CaughtPlayer)
	                {
	                    EnemyControl.GrandpaDeathSBed();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granny")
	        {
	            var gr = hit.collider.GetComponent<AI_Granny>();
	            if (EnemyControl != null && !EnemyControl.GrannyKilled)
	            {
	                if (gr != null && !gr.CaughtPlayer)
	                {
	                    EnemyControl.GrannyDeathS();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        var isd = hit.collider.gameObject.GetComponent<ItemSeedData>();
	        if (isd != null)
	        {
	            if (isd.itemName == "Gas")
	            {
	                if (ExplosionGas != null) Instantiate(ExplosionGas, hit.collider.transform.position, hit.collider.transform.rotation);
	                Destroy(hit.collider.gameObject);
	            }
	            else if (isd.itemName == "Melon")
	            {
	                if (MelonBlown != null)
	                {
	                    MelonBlown.transform.position = hit.collider.transform.position;
	                    MelonBlown.transform.rotation = hit.collider.transform.rotation;
	                    MelonBlown.SetActive(true);
	                }
	                Destroy(hit.collider.gameObject);
	            }
	        }

	        if (hitTag == "Santa")
	        {
	            if (LF != null && !LF.Hurt)
	            {
	                LF.KnockedOut();
	            }
	        }

	        if (hitName == "Screwdriver")
	        {
	            hit.collider.gameObject.layer = LayerMask.NameToLayer("Item");
	        }

	        if (hitName == "CanScrewDriver")
	        {
	            hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
	            if (ScrewdriverStanding != null) ScrewdriverStanding.layer = LayerMask.NameToLayer("Item");
	        }

	        if (hitName == "RemoteControlRat")
	        {
	            var rr = hit.collider.gameObject.GetComponent<RemoteRat>();
	            if (rr != null) rr.Impact();
	        }

	        if (hitName == "SpiderWall")
	        {
	            var sw = hit.collider.gameObject.GetComponentInParent<SpiderWall>();
	            if (sw != null) sw.DeathInitiates();
	        }

	        var rbHit = hit.collider.GetComponent<Rigidbody>();
	        if (rbHit != null)
	        {
	            rbHit.AddForce(dir * ImpactS1, ForceMode.Impulse);
	        }
	    }

	    public void ShootGun2()
	    {
	        if (RayShoot2 == null) return;
	        Vector3 dir = RayShoot2.TransformDirection(Vector3.forward);
	        if (ShootHead != null) ShootHead.Play("shootGun");
	        if (MuzzleFront != null && DropP != null) Instantiate(MuzzleFront, DropP.position, DropP.rotation);
	        if (Noisy1 != null && Player != null) Instantiate(Noisy1, Player.transform.position, Player.transform.rotation);
	        if (Noisy2 != null && Player != null) Instantiate(Noisy2, Player.transform.position, Player.transform.rotation);

	        if (!Physics.Raycast(RayShoot2.position, dir, out RaycastHit hit, Gun2RayDistance, GunLayers))
	        {
	            return;
	        }

	        if (IsPOWER && ExplosionGas != null)
	        {
	            Instantiate(ExplosionGas, hit.point, hit.collider.transform.rotation);
	        }

	        if (ImpactSound != null)
	        {
	            AudioSource.PlayClipAtPoint(ImpactSound, hit.point);
	        }

	        string hitName = hit.collider.gameObject.name;
	        string hitTag = hit.collider.gameObject.tag;

	        if (hitName == "CandleStick" || hitName == "CandleStick2" || hitName == "CandleStickCellar")
	        {
	            var rb = hit.collider.GetComponent<Rigidbody>();
	            if (rb != null) rb.isKinematic = false;
	        }

	        if (hitName == "WaterWheelMovable")
	        {
	            GameObject trigger = GameObject.Find("WaterWheelJumpscareTrigger");
	            if (trigger != null)
	            {
	                var vtp = trigger.GetComponent<VentTriggerPush>();
	                if (vtp != null) vtp.HitI();
	            }
	        }

	        if (hitName == "Cowleg")
	        {
	            var cr = hit.collider.GetComponent<CowlegRuin>();
	            if (cr != null) cr.RuinMeat();
	        }

	        if (hitName == "ShootableTargetCollider")
	        {
	            var sdh = hit.collider.GetComponent<StealDoorHit>();
	            if (sdh != null) sdh.OpenIt();
	        }

	        if (hitTag == "BearTrapActivated")
	        {
	            var btl = hit.collider.GetComponent<BearTrapLogic>();
	            if (btl != null) btl.ShootGun();
	        }

	        if (hitName == "Rat1" || hitName == "Rat2")
	        {
	            var rat = hit.collider.GetComponent<RatEnemy>();
	            if (rat != null) rat.WaitingTime = 120f;
	        }

	        if (hitTag == "bloodshootable")
	        {
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitName == "SpiderAtticParent")
	        {
	            var sp = hit.collider.GetComponent<AtticSpider>();
	            if (sp != null) sp.Dies();
	        }

	        if (hitName == "RobotProtect")
	        {
	            var rob = hit.collider.GetComponent<AI_RobotEnemy>();
	            if (rob != null) rob.KillRobot();
	        }

	        if (hitName == "CrowNest" || hitName == "CrowEat")
	        {
	            if (CM != null) CM.KillAssBird();
	        }

	        if (hitName == "PlankAttic" || hitName == "PlankaOH")
	        {
	            var rb = hit.collider.GetComponent<Rigidbody>();
	            if (rb != null) rb.isKinematic = false;
	            var seed = hit.collider.gameObject.AddComponent<ItemSeedData>();
	            seed.itemName = "Plank";
	            hit.collider.gameObject.name = "PlankaKnocked";
	            hit.collider.gameObject.AddComponent<OutlineCheck>();
	        }

	        if (hitName == "SpiderAtticButtonClose")
	        {
	            var asb = hit.collider.GetComponent<AtticSpiderButton>();
	            if (asb != null) asb.StopSpider();
	        }

	        if (hitName == "CameraBase")
	        {
	            var cs = hit.collider.GetComponent<CameraSmack>();
	            if (cs != null) cs.SmackCam();
	        }

	        if (hitTag == "spiderMom")
	        {
	            var mom = hit.collider.GetComponent<AI_MomSpider>();
	            if (mom != null && !mom.IsHit)
	            {
	                mom.HitMom();
	            }
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granpasoffa")
	        {
	            var gp = hit.collider.GetComponent<AI_Grandpa>();
	            if (EnemyControl != null && !EnemyControl.GrandpaKilled)
	            {
	                if (gp != null && !gp.CaughtPlayer)
	                {
	                    EnemyControl.GrandpaDeathSBed();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granpa")
	        {
	            var gp = hit.collider.GetComponent<AI_Grandpa>();
	            if (EnemyControl != null && !EnemyControl.GrandpaKilled)
	            {
	                if (gp != null && !gp.CaughtPlayer)
	                {
	                    EnemyControl.GrandpaDeathS();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "granny")
	        {
	            var gr = hit.collider.GetComponent<AI_Granny>();
	            if (EnemyControl != null && !EnemyControl.GrannyKilled)
	            {
	                if (gr != null && !gr.CaughtPlayer)
	                {
	                    EnemyControl.GrannyDeathS();
	                }
	            }
	            if (ImpactLord != null) Instantiate(ImpactLord, hit.point, hit.transform.rotation);
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        if (hitTag == "Santa")
	        {
	            if (LF != null && !LF.Hurt)
	            {
	                LF.KnockedOut();
	            }
	        }

	        if (hitName == "Screwdriver")
	        {
	            hit.collider.gameObject.layer = LayerMask.NameToLayer("Item");
	        }

	        if (hitName == "CanScrewDriver")
	        {
	            hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
	            if (ScrewdriverStanding != null) ScrewdriverStanding.layer = LayerMask.NameToLayer("Item");
	        }

	        if (hitTag == "mom")
	        {
	            if (EnemyControl != null && !EnemyControl.MomKilled)
	            {
	                EnemyControl.MomShoot();
	            }
	            if (BloodEffect != null) Instantiate(BloodEffect, hit.point, hit.transform.rotation);
	        }

	        var isd = hit.collider.gameObject.GetComponent<ItemSeedData>();
	        if (isd != null)
	        {
	            if (isd.itemName == "Gas")
	            {
	                if (ExplosionGas != null) Instantiate(ExplosionGas, hit.collider.transform.position, hit.collider.transform.rotation);
	                Destroy(hit.collider.gameObject);
	            }
	            else if (isd.itemName == "Melon")
	            {
	                if (MelonBlown != null)
	                {
	                    MelonBlown.transform.position = hit.collider.transform.position;
	                    MelonBlown.transform.rotation = hit.collider.transform.rotation;
	                    MelonBlown.SetActive(true);
	                }
	                Destroy(hit.collider.gameObject);
	            }
	        }

	        if (hitName == "RemoteControlRat")
	        {
	            var rr = hit.collider.gameObject.GetComponent<RemoteRat>();
	            if (rr != null) rr.Impact();
	        }

	        if (hitName == "SpiderWall")
	        {
	            var sw = hit.collider.gameObject.GetComponentInParent<SpiderWall>();
	            if (sw != null) sw.DeathInitiates();
	        }

	        var rbHit = hit.collider.GetComponent<Rigidbody>();
	        if (rbHit != null)
	        {
	            rbHit.AddForce(dir * ImpactS2, ForceMode.Impulse);
	        }
	    }

	    private void Update()
	    {
	        if (AmmoAmount == 0f)
	        {
	            if (AmmoUI1 != null) AmmoUI1.SetActive(false);
	            if (AmmoUI2 != null) AmmoUI2.SetActive(false);
	        }
	        else if (AmmoAmount == 1f)
	        {
	            if (AmmoUI1 != null) AmmoUI1.SetActive(true);
	            if (AmmoUI2 != null) AmmoUI2.SetActive(false);
	        }
	        else if (AmmoAmount == 2f)
	        {
	            if (AmmoUI1 != null) AmmoUI1.SetActive(true);
	            if (AmmoUI2 != null) AmmoUI2.SetActive(true);
	        }

	        if (!IsCharged)
	        {
	            if (H_TD != null) H_TD.SetActive(false);
	            if (UnchargedCrossbow != null) UnchargedCrossbow.SetActive(true);
	            if (ChargedCrossbow != null) ChargedCrossbow.SetActive(false);
	        }
	        else
	        {
	            if (UnchargedCrossbow != null) UnchargedCrossbow.SetActive(false);
	            if (ChargedCrossbow != null) ChargedCrossbow.SetActive(true);
	            if (H_TD != null) H_TD.SetActive(true);
	        }

	        FallingHolder fh = null;
	        if (Player != null) fh = Player.GetComponent<FallingHolder>();

	        if (H_CR != null && H_CR.activeSelf && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            if (IsCharged)
	            {
	                if (Input.GetKeyDown(ShootKey) && !Paused.IsPaused && (fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping) && CanDropDis)
	                {
	                    IsCharged = false;
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && ShootCross != null) audio.PlayOneShot(ShootCross);
	                    if (DartShoot != null && SpawnDart != null)
	                    {
	                        GameObject dart = Instantiate(DartShoot, SpawnDart.position, SpawnDart.rotation);
	                        if (dart != null)
	                        {
	                            var rb = dart.GetComponent<Rigidbody>();
	                            if (rb != null) rb.AddForce(dart.transform.forward * ShootSpeed, ForceMode.Impulse);
	                            if (DartHasEnergy)
	                            {
	                                var dartComp = dart.GetComponent<Dart>();
	                                if (dartComp != null) dartComp.EnergyFilled = true;
	                                DartHasEnergy = false;
	                            }
	                        }
	                    }
	                }
	                if (ShootCB1 != null) ShootCB1.SetActive(true);
	            }
	            else
	            {
	                if (ShootCB1 != null) ShootCB1.SetActive(false);
	                Input.GetKeyDown(ShootKey);
	            }
	        }
	        else
	        {
	            if (ShootCB1 != null) ShootCB1.SetActive(false);
	        }

	        if (H_SPRAY != null && H_SPRAY.activeSelf && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            if (EffectSpray == null || !EffectSpray.isPlaying)
	            {
	                if (SprayLeft > 0f)
	                {
	                    if (Input.GetKeyDown(ShootKey) && !Paused.IsPaused && (fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping) && CanDropDis)
	                    {
	                        SprayLeft -= 1f;
	                        if (EffectSpray != null) EffectSpray.Play();
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null) audio.Play();
	                    }
	                    if (SprayB != null) SprayB.SetActive(true);
	                }
	                else
	                {
	                    if (SprayB != null) SprayB.SetActive(false);
	                    if (Input.GetKeyDown(ShootKey))
	                    {
	                        ShowNeedText("It's empty");
	                    }
	                }
	            }
	            else
	            {
	                if (SprayB != null) SprayB.SetActive(false);
	            }
	        }
	        else
	        {
	            if (SprayB != null) SprayB.SetActive(false);
	        }

	        if (H_RC != null && H_RC.activeSelf && (HP == null || !HP.usedRemote) && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            if (IsCloseRemote)
	            {
	                if (Input.GetKeyDown(ShootKey) && !Paused.IsPaused && (fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping) && CanDropDis)
	                {
	                    if (HP != null) HP.OpenRemote();
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && ClickRemote != null) audio.PlayOneShot(ClickRemote);
	                }
	                if (UseB != null) UseB.SetActive(true);
	            }
	            else
	            {
	                if (UseB != null) UseB.SetActive(true);
	                if (Input.GetKeyDown(ShootKey))
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && ClickRemote != null) audio.PlayOneShot(ClickRemote);
	                    ShowNeedText("I need to get closer");
	                }
	            }
	        }
	        else
	        {
	            if (UseB != null) UseB.SetActive(false);
	        }

	        if (H_SG != null && H_SG.activeSelf && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            if (LoadedGun)
	            {
	                if (Input.GetKeyDown(ShootKey) && !Paused.IsPaused && (fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping))
	                {
	                    var anim = H_SG.GetComponentInChildren<Animation>();
	                    if (anim != null && !anim.isPlaying)
	                    {
	                        LoadedGun = false;
	                        anim.Stop();
	                        anim.CrossFade("Shoot");
	                        ShootGun1();
	                    }
	                }
	                if (ShootB1 != null) ShootB1.SetActive(true);
	            }
	            else
	            {
	                if (ShootB1 != null) ShootB1.SetActive(false);
	                if (Input.GetKeyDown(ShootKey))
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && EmptyGun != null) audio.PlayOneShot(EmptyGun);
	                }
	            }
	        }
	        else
	        {
	            if (ShootB1 != null) ShootB1.SetActive(false);
	        }

	        if (H_DSH != null && H_DSH.activeSelf && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            if (AmmoCounterUI != null) AmmoCounterUI.SetActive(true);
	            if (AmmoAmount > 0f)
	            {
	                if (Input.GetKeyDown(ShootKey) && !Paused.IsPaused && (fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping))
	                {
	                    var anim = H_DSH.GetComponent<Animation>();
	                    if (AmmoAmount == 1f)
	                    {
	                        if (anim != null && !anim.isPlaying)
	                        {
	                            AmmoAmount -= 1f;
	                            anim.Stop();
	                            anim.Play("ShootShootyEmpty");
	                            ShootGun2();
	                        }
	                    }
	                    else if (AmmoAmount == 2f)
	                    {
	                        if (anim != null && !anim.isPlaying)
	                        {
	                            AmmoAmount -= 1f;
	                            anim.Stop();
	                            anim.Play("ShootShooty");
	                            ShootGun2();
	                        }
	                    }
	                }
	                if (ShootB2 != null) ShootB2.SetActive(true);
	            }
	            else
	            {
	                if (ShootB2 != null) ShootB2.SetActive(false);
	                if (Input.GetKeyDown(ShootKey))
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && EmptyGun != null) audio.PlayOneShot(EmptyGun);
	                }
	            }
	        }
	        else
	        {
	            if (AmmoCounterUI != null) AmmoCounterUI.SetActive(false);
	            if (ShootB2 != null) ShootB2.SetActive(false);
	        }

	        if (Input.GetKeyDown(MainInteract))
	        {
	            if (Ring != null && Ring.activeSelf && !Paused.IsPaused)
	            {
	                if ((fh == null || (!fh.isFalling && !fh.isLanding)) && (WJ == null || !WJ.IsJumping) && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	                {
	                    buttonClicked = true;
	                }
	            }
	        }

	        if (Input.GetKeyDown(DropKey))
	        {
	            if (Drop1 != null && Drop1.activeSelf && !Paused.IsPaused)
	            {
	                if ((fh == null || !fh.isLanding) && (WJ == null || !WJ.IsJumping) && (PlayerStatus == null || !PlayerStatus.IsJumpscared) && CanDropDis)
	                {
	                    buttonClicked2 = true;
	                }
	            }
	        }

	        if (Paused.IsPaused) return;

	        bool enableHUD = (fh == null || !fh.isLanding) && (WJ == null || !WJ.IsJumping) && (PlayerStatus == null || !PlayerStatus.IsJumpscared);
	        if (Space != null) Space.enabled = enableHUD;
	        if (Shoot1 != null) Shoot1.enabled = enableHUD;
	        if (Shoot2 != null) Shoot2.enabled = enableHUD;
	        if (Shoot3 != null) Shoot3.enabled = enableHUD;
	        if (SprayShoot != null) SprayShoot.enabled = enableHUD;
	        if (Use != null) Use.enabled = enableHUD;
	        if (AmmoUI2 != null)
	        {
	            var img = AmmoUI2.GetComponent<Image>();
	            if (img != null) img.enabled = enableHUD;
	        }
	        if (AmmoUI1 != null)
	        {
	            var img = AmmoUI1.GetComponent<Image>();
	            if (img != null) img.enabled = enableHUD;
	        }

	        if (Crosshair != null)
	        {
	            bool doorActive = EButton_Door != null && EButton_Door.gameObject.activeSelf;
	            bool pickActive = EButton_Pick != null && EButton_Pick.gameObject.activeSelf;
	            bool winchActive = EButtonWinch != null && EButtonWinch.gameObject.activeSelf;
	            Crosshair.enabled = !(doorActive || pickActive || winchActive);
	        }

	        if (buttonClicked2 && !Paused.IsPaused && (PlayerStatus == null || !PlayerStatus.IsJumpscared))
	        {
	            CheckItemDropping();
	        }

	        Vector3 forward = transform.TransformDirection(Vector3.forward);

	        if (Physics.Raycast(transform.position, forward, out RaycastHit hitCheck, RaycastCheckItemDis, GunLayers))
	        {
	            CanDropDis = (hitCheck.collider != null && hitCheck.collider.gameObject.name == "HoleGrid");
	        }
	        else
	        {
	            CanDropDis = true;
	        }

	        if (Physics.Raycast(transform.position, forward, out RaycastHit hitGrid, 0.6f))
	        {
	            if (hitGrid.collider != null && hitGrid.collider.gameObject.name == "HoleGrid")
	            {
	                if (DropPointGridsPos != null && DropP != null) DropP.position = DropPointGridsPos.position;
	            }
	            else
	            {
	                if (DropPointNormalPos != null && DropP != null) DropP.position = DropPointNormalPos.position;
	            }
	        }
	        else
	        {
	            if (DropPointNormalPos != null && DropP != null) DropP.position = DropPointNormalPos.position;
	        }

	        Debug.DrawRay(transform.position, forward * RaycastDis, Color.white);

	        if (!Physics.Raycast(transform.position, forward, out RaycastHit hit, RaycastDis, LayersIncluded))
	        {
	            TurnOffItemTexts();
	            buttonClicked = false;
	            if (Ring != null) Ring.SetActive(false);
	            if (FuelTankButton != null) FuelTankButton.SetActive(false);
	            if (HP != null)
	            {
	                HP.FillingTank = false;
	                HP.RotatingWinch = false;
	            }
	            if (RotateWinchButton != null) RotateWinchButton.SetActive(false);
	            return;
	        }

	        if ((fh != null && (fh.isFalling || fh.isLanding)) || (WJ != null && WJ.IsJumping) || (PlayerStatus != null && PlayerStatus.IsJumpscared))
	        {
	            TurnOffItemTexts();
	            buttonClicked = false;
	            if (Ring != null) Ring.SetActive(false);
	            if (FuelTankButton != null) FuelTankButton.SetActive(false);
	            if (HP != null)
	            {
	                HP.FillingTank = false;
	                HP.RotatingWinch = false;
	            }
	            if (RotateWinchButton != null) RotateWinchButton.SetActive(false);
	            return;
	        }

	        string hitName = hit.collider.gameObject.name;
	        string hitTag = hit.collider.gameObject.tag;

	        if (hitName == "HatchSpiderCellarClosed")
	        {
	            if (HP != null && !HP.SpiderCellarDone)
	            {
	                var audio = GetComponent<AudioSource>();
	                if (audio == null || !audio.isPlaying)
	                {
	                    TurnOffItemTexts();
	                    if (Ring != null) Ring.SetActive(true);
	                    if (buttonClicked)
	                    {
	                        buttonClicked = false;
	                        if (H_SPIDERK == null || !H_SPIDERK.activeSelf)
	                        {
	                            if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                            ShowNeedText("I need a spider key");
	                        }
	                        else
	                        {
	                            H_SPIDERK.SetActive(false);
	                            if (Drop1 != null) Drop1.SetActive(false);
	                            HP.OpenHatchSpiderCellar();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "WheelCrankCollider")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_WC != null && H_WC.activeSelf)
	                {
	                    H_WC.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.WheelCrankRotate();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a wheel crank");
	                }
	            }
	        }
	        else if (hitName == "ironDoorLock")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_WS != null && H_WS.activeSelf)
	                {
	                    H_WS.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.WoodenStickPlaced();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need some sort of lever");
	                }
	            }
	        }
	        else if (hitName == "PadlockSpiderCellar")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_RPK == null || !H_RPK.activeSelf)
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                    ShowNeedText("I need some sort of padlock key");
	                }
	                else
	                {
	                    H_RPK.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UseRustyPadlock();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                }
	            }
	        }
	        else if (hitName == "PlaceWinchHandleColl")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_WH != null && H_WH.activeSelf)
	                {
	                    H_WH.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    var col = hit.collider.GetComponent<BoxCollider>();
	                    if (col != null) Destroy(col);
	                    if (HP != null && HP.WinchHandleThere != null) HP.WinchHandleThere.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a winch handle");
	                }
	            }
	        }
	        else if (hitName == "Cable")
	        {
	            if (HP != null && !HP.CutWire1)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if ((H_CP != null && H_CP.activeSelf) || (H_CC != null && H_CC.activeSelf))
	                    {
	                        HP.CuttingWire1();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("I need cutting pliers");
	                    }
	                }
	            }
	        }
	        else if (hitName == "Cable2")
	        {
	            if (HP != null && !HP.CutWire2)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if ((H_CP != null && H_CP.activeSelf) || (H_CC != null && H_CC.activeSelf))
	                    {
	                        HP.CuttingWire2();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("I need cutting pliers");
	                    }
	                }
	            }
	        }
	        else if (hitName == "CableFan")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if ((H_CP != null && H_CP.activeSelf) || (H_CC != null && H_CC.activeSelf))
	                {
	                    if (HP != null) HP.CutAtticWires();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need cutting pliers");
	                }
	            }
	        }
	        else if (hitName == "WeaponLockerDoor" && (HP == null || !HP.UsedWPKey))
	        {
	            var audio = GetComponent<AudioSource>();
	            if (audio == null || !audio.isPlaying)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_WK == null || !H_WK.activeSelf)
	                    {
	                        if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                        ShowNeedText("I need a weapon key");
	                    }
	                    else
	                    {
	                        if (HP != null) HP.UseWPKey();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                        if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                    }
	                }
	            }
	        }
	        else if (hitName == "BookPlace")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_BO != null && H_BO.activeSelf)
	                {
	                    H_BO.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.PutBook();
	                    hit.collider.gameObject.name = "InteractedAlready!";
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("Maybe I can put some sort of book here");
	                }
	            }
	        }
	        else if (hitName == "MeatPlate" && !MeatPlaced)
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_MEAT != null && H_MEAT.activeSelf)
	                {
	                    if (AS != null) AS.MeatIsThere();
	                    H_MEAT.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    MeatPlaced = true;
	                }
	                else
	                {
	                    ShowNeedText("An empty plate");
	                }
	            }
	        }
	        else if (hitName == "BatteryLockFrontDoor")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_BA != null && H_BA.activeSelf)
	                {
	                    H_BA.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UseBattery();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    if (HP != null) HP.NoBattery();
	                    ShowNeedText("I need a battery");
	                }
	            }
	        }
	        else if (hitName == "HangLock")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_PK != null && H_PK.activeSelf)
	                {
	                    H_PK.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UsePadlockKey();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                    ShowNeedText("I need a padlock key");
	                }
	            }
	        }
	        else if (hitName == "HangLockGarage")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_PK != null && H_PK.activeSelf)
	                {
	                    H_PK.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UsePadlockKey2();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                }
	                else
	                {
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                    ShowNeedText("I need a padlock key");
	                }
	            }
	        }
	        else if (hitName == "GarageportOpen")
	        {
	            if (HP != null && !HP.openedPort)
	            {
	                var audio = hit.collider.GetComponent<AudioSource>();
	                if (audio == null || !audio.isPlaying)
	                {
	                    TurnOffItemTexts();
	                    if (Ring != null) Ring.SetActive(true);
	                    if (buttonClicked)
	                    {
	                        buttonClicked = false;
	                        if (HP.usedPadlockForPort)
	                        {
	                            HP.OpenPort();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                        }
	                        else
	                        {
	                            if (audio != null) audio.Play();
	                            ShowNeedText("It's locked");
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "ElectricCoilDoor")
	        {
	            if (HP != null && !HP.usedElecKey)
	            {
	                var audio = GetComponent<AudioSource>();
	                if (audio == null || !audio.isPlaying)
	                {
	                    TurnOffItemTexts();
	                    if (Ring != null) Ring.SetActive(true);
	                    if (buttonClicked)
	                    {
	                        buttonClicked = false;
	                        if (H_EK != null && H_EK.activeSelf)
	                        {
	                            H_EK.SetActive(false);
	                            if (Drop1 != null) Drop1.SetActive(false);
	                            HP.UseElecKey();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                            if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                        }
	                        else
	                        {
	                            if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                            ShowNeedText("I need a cabinet key");
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "RechargingBulb")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_BATON != null && H_BATON.activeSelf)
	                {
	                    if (ChargePoints > 0f)
	                    {
	                        ChargePoints -= 1f;
	                        if (HP != null) HP.RechargedSites += 1f;
	                        var cb = hit.collider.GetComponent<ChargeBulb>();
	                        if (cb != null) cb.ZapIT();
	                        hit.collider.gameObject.name = "ChargedBulbYes";
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("I need to recharge the baton");
	                    }
	                }
	                else
	                {
	                    ShowNeedText("I need something electric");
	                }
	            }
	        }
	        else if (hitName == "ColliderChargeBaton")
	        {
	            if (HP != null && HP.usedElecKey)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_BATON != null && H_BATON.activeSelf)
	                    {
	                        if (ChargePoints < 2f)
	                        {
	                            ChargePoints = 2f;
	                            if (ElectricEffectBaton != null) ElectricEffectBaton.Play();
	                            var audio = GetComponent<AudioSource>();
	                            if (audio != null) audio.Play();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                        }
	                        else
	                        {
	                            ShowNeedText("It's recharged");
	                        }
	                    }
	                    else if ((H_CR != null && H_CR.activeSelf) || (H_TD != null && H_TD.activeSelf))
	                    {
	                        DartHasEnergy = true;
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("An electric coil");
	                    }
	                }
	            }
	        }
	        else if (hitName == "GrandpaHitBody")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_BATON != null && H_BATON.activeSelf && ChargePoints > 0f)
	                {
	                    ChargePoints -= 1f;
	                    if (ParticleEffectElectricSpawn != null)
	                    {
	                        Instantiate(ParticleEffectElectricSpawn, hit.point, hit.collider.transform.rotation);
	                    }
	                    if (EnemyControl != null) EnemyControl.GrandpaDeathSBed();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I could electrocute him");
	                }
	            }
	        }
	        else if (hitName == "BakluckaLocked")
	        {
	            var audio = GetComponent<AudioSource>();
	            if (audio == null || !audio.isPlaying)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_CK != null && H_CK.activeSelf)
	                    {
	                        H_CK.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        if (HP != null) HP.OpenTrunkCarKey();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                        if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                    }
	                    else
	                    {
	                        if (audio != null && LockedTrunk != null) audio.PlayOneShot(LockedTrunk);
	                        ShowNeedText("I need a car key");
	                    }
	                }
	            }
	        }
	        else if (hitName == "CutAreaTriggerText")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_ME != null && H_ME.activeSelf)
	                {
	                    H_ME.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.PlaceMelon();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("This is a dangerous cutting tool");
	                }
	            }
	        }
	        else if (hitName == "_Highlight-Plank")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_PL != null && H_PL.activeSelf)
	                {
	                    H_PL.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.PlacePlank();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("Maybe I can use a plank");
	                }
	            }
	        }
	        else if (hitName == "SpecialKeyLock")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SPK != null && H_SPK.activeSelf)
	                {
	                    H_SPK.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UseSpecialKey();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a special key");
	                }
	            }
	        }
	        else if (hitName == "CarBatteryCollider")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_CB != null && H_CB.activeSelf)
	                {
	                    H_CB.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.CarBatteryPlace();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a car battery");
	                }
	            }
	        }
	        else if (hitName == "EnginePartCollider")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_EP != null && H_EP.activeSelf)
	                {
	                    H_EP.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.EnginePlace();
	                    hit.collider.enabled = false;
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need an engine part");
	                }
	            }
	        }
	        else if (hitName == "SparkplugCable")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SP != null && H_SP.activeSelf)
	                {
	                    H_SP.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UseSparkplug();
	                    Destroy(hit.collider.gameObject);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a spark plug");
	                }
	            }
	        }
	        else if (hitName == "PlaceStabilizerFuseCollider")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_FUSE != null && H_FUSE.activeSelf)
	                {
	                    H_FUSE.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.PlaceFusePanel();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a fuse");
	                }
	            }
	        }
	        else if (hitName == "EngineScrewPoint")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_WR != null && H_WR.activeSelf)
	                {
	                    Destroy(hit.collider.gameObject);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a wrench");
	                }
	            }
	        }
	        else if (hitName == "CollPlaceData")
	        {
	            if (HP != null && !HP.PlacedUSB)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_DATA != null && H_DATA.activeSelf)
	                    {
	                        H_DATA.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        HP.PlaceUSB();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                    }
	                    else
	                    {
	                        ShowNeedText("I need some sort of data container");
	                    }
	                }
	            }
	        }
	        else if (hitName == "PictureOfGranny")
	        {
	            if (HP != null && HP.Days != null && !HP.Days.ExtraDay)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_PP1 != null && H_PP1.activeSelf)
	                    {
	                        H_PP1.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        HP.PlaceP1();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else if (H_PP2 != null && H_PP2.activeSelf)
	                    {
	                        H_PP2.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        HP.PlaceP2();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else if (H_PP3 != null && H_PP3.activeSelf)
	                    {
	                        H_PP3.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        HP.PlaceP3();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else if (H_PP4 != null && H_PP4.activeSelf)
	                    {
	                        H_PP4.SetActive(false);
	                        if (Drop1 != null) Drop1.SetActive(false);
	                        HP.PlaceP4();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("There are missing painting pieces");
	                    }
	                }
	            }
	        }
	        else if (hitName == "ButtonRoboSparking")
	        {
	            if (HP != null && !HP.IsRecharging)
	            {
	                var audio = hit.collider.GetComponent<AudioSource>();
	                if (audio == null || !audio.isPlaying)
	                {
	                    TurnOffItemTexts();
	                    if (Ring != null) Ring.SetActive(true);
	                    if (buttonClicked)
	                    {
	                        buttonClicked = false;
	                        var anim = hit.collider.GetComponent<Animation>();
	                        if (anim != null) anim.Play("PressButton");
	                        if (audio != null) audio.Play();
	                        if (HP != null && HP.PlacedUSB && HP.RechargedSites == 4f)
	                        {
	                            if (!HP.fuseInPlace)
	                            {
	                                ShowNeedText("Something is wrong. The Robot won't charge");
	                            }
	                            else
	                            {
	                                HP.StartRecharging();
	                                StopSetOfTexts();
	                                if (Ring != null) Ring.SetActive(false);
	                            }
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "LockedLucka")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                ShowNeedText("Maybe I can put some weight on the weight plate");
	            }
	        }
	        else if (hitName == "ColliderCogwheelPlacement")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_CW1 != null && H_CW1.activeSelf)
	                {
	                    H_CW1.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null && HP.Cogwheel1 != null) HP.Cogwheel1.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                }
	                else if (H_CW2 != null && H_CW2.activeSelf)
	                {
	                    H_CW2.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null && HP.Cogwheel2 != null) HP.Cogwheel2.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                }
	                else
	                {
	                    ShowNeedText("There are missing cogwheels");
	                }
	            }
	        }
	        else if (hitName == "ColliderShotgunPieces")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_PS1 != null && H_PS1.activeSelf)
	                {
	                    H_PS1.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null && HP.P1 != null) HP.P1.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else if (H_PS2 != null && H_PS2.activeSelf)
	                {
	                    H_PS2.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null && HP.P2 != null) HP.P2.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else if (H_PS3 != null && H_PS3.activeSelf)
	                {
	                    H_PS3.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null && HP.P3 != null) HP.P3.SetActive(true);
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("There are missing shotgun parts");
	                }
	            }
	        }
	        else if (hitName == "PlayHousePadlock")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_PLAYK != null && H_PLAYK.activeSelf)
	                {
	                    H_PLAYK.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UsePlayhouse();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a playhouse key");
	                }
	            }
	        }
	        else if (hitName == "DigitalPadlock")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_CO != null && H_CO.activeSelf)
	                {
	                    H_CO.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (HP != null) HP.UseCode();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && CodeVerify != null) audio.PlayOneShot(CodeVerify);
	                }
	                else
	                {
	                    ShowNeedText("I need a padlock code");
	                }
	            }
	        }
	        else if (hitName == "SwitchExtreme")
	        {
	            if (HP != null && !HP.OpenedExLock)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    HP.ExtremeUnlock();
	                    var anim = hit.collider.GetComponent<Animation>();
	                    if (anim != null) anim.Play("Spak");
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && DragSwitch != null) audio.PlayOneShot(DragSwitch);
	                }
	            }
	        }
	        else if (hitName == "MomPlaceSwitch")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                hit.collider.gameObject.name = "InteractedAlready!";
	                if (HP != null) HP.PullBookshelf();
	                StopSetOfTexts();
	                if (Ring != null) Ring.SetActive(false);
	            }
	        }
	        else if (hitName == "PlankFrontDoor")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_HA != null && H_HA.activeSelf)
	                {
	                    if (HP != null) HP.HitPlankDoor();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need a hammer");
	                }
	            }
	        }
	        else if (hitName == "PlankaOH")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_HA != null && H_HA.activeSelf)
	                {
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var pf = hit.collider.GetComponent<PlankFall>();
	                    if (pf != null) pf.BreakPlank();
	                }
	                else
	                {
	                    ShowNeedText("I need a hammer");
	                }
	            }
	        }
	        else if (hitName == "CameraBase")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_HA != null && H_HA.activeSelf)
	                {
	                    var cs = hit.collider.GetComponent<CameraSmack>();
	                    if (cs != null) cs.SmackCam();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	                else
	                {
	                    ShowNeedText("I need to break this camera");
	                }
	            }
	        }
	        else if (hitName == "PlankAttic")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_HA != null && H_HA.activeSelf)
	                {
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var pf = hit.collider.GetComponent<PlankFall>();
	                    if (pf != null) pf.BreakPlank();
	                }
	                else
	                {
	                    ShowNeedText("I need a hammer");
	                }
	            }
	        }
	        else if (hitName == "ExitDoorCollEscape")
	        {
	            var audio = GetComponent<AudioSource>();
	            if (audio == null || !audio.isPlaying)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_MK == null || !H_MK.activeSelf)
	                    {
	                        if (audio != null && AttemptOpenDoor != null) audio.PlayOneShot(AttemptOpenDoor);
	                        ShowNeedText("I need a master key");
	                    }
	                    else
	                    {
	                        if (HP != null && HP.IsDoorFree)
	                        {
	                            H_MK.SetActive(false);
	                            if (Drop1 != null) Drop1.SetActive(false);
	                            HP.MasterKeyUsed();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                            if (audio != null && UnlockDoor != null) audio.PlayOneShot(UnlockDoor);
	                            if (Escapes != null) Escapes.EscapeDoor();
	                        }
	                        else
	                        {
	                            if (audio != null && AttemptOpenDoor != null) audio.PlayOneShot(AttemptOpenDoor);
	                            ShowNeedText("I can't escape yet");
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "CollEnterBody")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (HP != null && HP.RechargedRobot)
	                {
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    if (Escapes != null) Escapes.EscapeRobo();
	                }
	                else
	                {
	                    ShowNeedText("Something is wrong. The Robot won't charge");
	                }
	            }
	        }
	        else if (hitName == "ChainCutCollider")
	        {
	            if (HP != null && !HP.UsedChainCutter && HP.IronDoors != null && !HP.IronDoors.isPlaying)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    if (H_CC != null && H_CC.activeSelf)
	                    {
	                        HP.CutChains();
	                        StopSetOfTexts();
	                        if (Ring != null) Ring.SetActive(false);
	                    }
	                    else
	                    {
	                        ShowNeedText("I need a chain cutter");
	                    }
	                }
	            }
	        }
	        else if (hitName == "SafeDoor")
	        {
	            if (HP != null && !HP.UsedSafeKey)
	            {
	                var audio = GetComponent<AudioSource>();
	                if (audio == null || !audio.isPlaying)
	                {
	                    TurnOffItemTexts();
	                    if (Ring != null) Ring.SetActive(true);
	                    if (buttonClicked)
	                    {
	                        buttonClicked = false;
	                        if (H_SK != null && H_SK.activeSelf)
	                        {
	                            H_SK.SetActive(false);
	                            if (Drop1 != null) Drop1.SetActive(false);
	                            HP.OpenSafe();
	                            StopSetOfTexts();
	                            if (Ring != null) Ring.SetActive(false);
	                            if (audio != null && UsedKey != null) audio.PlayOneShot(UsedKey);
	                        }
	                        else
	                        {
	                            if (audio != null && LockedSafe != null) audio.PlayOneShot(LockedSafe);
	                            ShowNeedText("I need a safe key");
	                        }
	                    }
	                }
	            }
	        }
	        else if (hitName == "LockActive")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                ShowNeedText("I need to find a switch");
	            }
	        }
	        else if (hitName == "1_IronDoor_L" || hitName == "1_IronDoor_R")
	        {
	            if (HP != null && HP.IronDoors != null && !HP.IronDoors.isPlaying)
	            {
	                TurnOffItemTexts();
	                if (Ring != null) Ring.SetActive(true);
	                if (buttonClicked)
	                {
	                    buttonClicked = false;
	                    HP.OpenIronDoors();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                }
	            }
	        }
	        else if (hitName == "ScrewsSpiderCellar")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SD != null && H_SD.activeSelf)
	                {
	                    hit.collider.gameObject.name = "Untagged";
	                    var anim = hit.collider.GetComponent<Animation>();
	                    if (anim != null) anim.Play("screwAnim");
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && Screw != null) audio.PlayOneShot(Screw);
	                }
	                else
	                {
	                    ShowNeedText("I need a screwdriver");
	                }
	            }
	        }
	        else if (hitName == "ScrewOldHouse")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SD != null && H_SD.activeSelf)
	                {
	                    hit.collider.gameObject.name = "ScrewedIt";
	                    var anim = hit.collider.GetComponent<Animation>();
	                    if (anim != null) anim.Play("Screw1Open");
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && Screw != null) audio.PlayOneShot(Screw);
	                }
	                else
	                {
	                    ShowNeedText("I need a screwdriver");
	                }
	            }
	        }
	        else if (hitName == "ScrewOldHouse2")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SD != null && H_SD.activeSelf)
	                {
	                    hit.collider.gameObject.name = "ScrewedIt";
	                    var anim = hit.collider.GetComponent<Animation>();
	                    if (anim != null) anim.Play("Screw2Open");
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && Screw != null) audio.PlayOneShot(Screw);
	                }
	                else
	                {
	                    ShowNeedText("I need a screwdriver");
	                }
	            }
	        }
	        else if (hitName == "SkruvSpakBox")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SD != null && H_SD.activeSelf)
	                {
	                    hit.collider.gameObject.name = "Untagged";
	                    var anim = hit.collider.GetComponent<Animation>();
	                    if (anim != null) anim.Play("ELscrew");
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && Screw != null) audio.PlayOneShot(Screw);
	                }
	                else
	                {
	                    ShowNeedText("I need a screwdriver");
	                }
	            }
	        }
	        else if (hitName == "Crossbow" || hitName == "CrossbowGrannyWPActive")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Crossbow";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_CR != null) H_CR.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                if (Ring != null) Ring.SetActive(false);
	                Destroy(hit.collider.gameObject);
	                var audio = GetComponent<AudioSource>();
	                if (audio != null && PickCross != null) audio.PlayOneShot(PickCross);
	            }
	        }
	        else if (hitName == "PepperSprayGrannyWPActive")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Pepper spray";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_SPRAY != null) H_SPRAY.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                if (Ring != null) Ring.SetActive(false);
	                Destroy(hit.collider.gameObject);
	                var audio = GetComponent<AudioSource>();
	                if (audio != null && PickUpSoft != null) audio.PlayOneShot(PickUpSoft);
	            }
	        }
	        else if (hitName == "FreezeTrap")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Freeze trap";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_FT != null) H_FT.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                if (Ring != null) Ring.SetActive(false);
	                Destroy(hit.collider.gameObject);
	            }
	        }
	        else if (hitName == "PoisonTrap")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Poison trap";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_PTRAP != null) H_PTRAP.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                if (Ring != null) Ring.SetActive(false);
	                Destroy(hit.collider.gameObject);
	            }
	        }
	        else if (hit.collider.GetComponent<ItemSeedData>() != null)
	        {
	            var seed = hit.collider.GetComponent<ItemSeedData>();
	            TurnOffItemTexts();
	            string itemName = seed.itemName;
	            if (Inventory != null)
	            {
	                var def = Inventory.GetItemDefByName(itemName);
	                if (def != null && T_HighlightItem != null)
	                {
	                    T_HighlightItem.SetActive(true);
	                    var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                    if (tmp != null) tmp.text = def.textHighlighted;
	                }
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                if (Inventory != null)
	                {
	                    Inventory.PickupItem(itemName);
	                }
	                if (itemName == "RegShotgun") PickShotgun();
	                Destroy(hit.collider.gameObject);
	                TurnOffItemTexts();
	                if (Drop1 != null) Drop1.SetActive(true);
	                if (Ring != null) Ring.SetActive(false);
	            }
	        }
	        else if (hitName == "RegShotgun")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Shotgun";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_SG != null) H_SG.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                PickShotgun();
	                if (Ring != null) Ring.SetActive(false);
	                Destroy(hit.collider.gameObject);
	            }
	        }
	        else if (hitName == "ShotgunParentG" || hitName == "Shotgun2" || hitTag == "Shotgun2" || hitName == "ShotgunBed" || hitTag == "ShotgunParentG" || hitTag == "ShotgunBed")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Double barrel shotgun";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                CheckItemDropping();
	                TurnOffItemTexts();
	                if (H_DSH != null) H_DSH.SetActive(true);
	                if (Drop1 != null) Drop1.SetActive(true);
	                PickShooty();
	                if (Ring != null) Ring.SetActive(false);
	                if (WC != null) WC.RevSH2();
	                Destroy(hit.collider.gameObject);
	            }
	        }
	        else if (hitName == "ammo" || hitTag == "ammo")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Shotgun ammo";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_SG != null && H_SG.activeSelf)
	                {
	                    if (!LoadedGun)
	                    {
	                        LoadedGun = true;
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null && ReloadGun != null) audio.PlayOneShot(ReloadGun);
	                        var anim = H_SG.GetComponentInChildren<Animation>();
	                        if (anim != null) anim.Play("Load");
	                        Destroy(hit.collider.gameObject);
	                    }
	                    else
	                    {
	                        ShowNeedText("It's already loaded");
	                    }
	                }
	                else if (H_DSH != null && H_DSH.activeSelf)
	                {
	                    if (AmmoAmount == 0f)
	                    {
	                        AmmoAmount = 2f;
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null && ReloadGun != null) audio.PlayOneShot(ReloadGun);
	                        var anim = H_DSH.GetComponent<Animation>();
	                        if (anim != null) anim.Play("Load_0");
	                        Destroy(hit.collider.gameObject);
	                    }
	                    else if (AmmoAmount == 1f)
	                    {
	                        AmmoAmount = 2f;
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null && ReloadGun != null) audio.PlayOneShot(ReloadGun);
	                        var anim = H_DSH.GetComponent<Animation>();
	                        if (anim != null) anim.Play("Reload");
	                        Destroy(hit.collider.gameObject);
	                    }
	                    else
	                    {
	                        ShowNeedText("It's already loaded");
	                    }
	                }
	                else
	                {
	                    ShowNeedText("I need a shotgun");
	                }
	            }
	        }
	        else if (hitTag == "arrow")
	        {
	            TurnOffItemTexts();
	            if (T_HighlightItem != null)
	            {
	                T_HighlightItem.SetActive(true);
	                var tmp = T_HighlightItem.GetComponent<TMP_Text>();
	                if (tmp != null) tmp.text = "Tranquilizer dart";
	            }
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_CR != null && H_CR.activeSelf)
	                {
	                    if (!IsCharged)
	                    {
	                        IsCharged = true;
	                        var audio = GetComponent<AudioSource>();
	                        if (audio != null && LoadCross != null) audio.PlayOneShot(LoadCross);
	                        Destroy(hit.collider.gameObject);
	                    }
	                    else
	                    {
	                        ShowNeedText("It's already loaded");
	                    }
	                }
	                else
	                {
	                    ShowNeedText("I need a crossbow");
	                }
	            }
	        }
	        else if (hitName == "WinchHandleRotating")
	        {
	            if (HP != null && !HP.IsUpFully)
	            {
	                TurnOffItemTexts();
	                if (RotateWinchButton != null) RotateWinchButton.SetActive(true);
	                HP.RotatingWinch = Input.GetKey(MainInteract);
	            }
	        }
	        else if (hitName == "FuelTankCollider")
	        {
	            if (HP != null && !HP.TankFuelled)
	            {
	                TurnOffItemTexts();
	                if (!HP.OpenedTank)
	                {
	                    if (HP.FuelTank != null) HP.FuelTank.Play("tanklockOpen");
	                    HP.OpenedTank = true;
	                }
	                if (H_GAS != null && H_GAS.activeSelf)
	                {
	                    if (FuelTankButton != null) FuelTankButton.SetActive(true);
	                    if (Ring != null) Ring.SetActive(false);
	                    HP.FillingTank = Input.GetKey(MainInteract);
	                }
	                else
	                {
	                    if (Input.GetKeyDown(MainInteract))
	                    {
	                        ShowNeedText("I need a gasoline can");
	                    }
	                    if (Ring != null) Ring.SetActive(true);
	                    if (FuelTankButton != null) FuelTankButton.SetActive(false);
	                    HP.FillingTank = false;
	                }
	            }
	        }
	        else if (hitName == "BirdPlate")
	        {
	            TurnOffItemTexts();
	            if (Ring != null) Ring.SetActive(true);
	            if (buttonClicked)
	            {
	                buttonClicked = false;
	                if (H_BS != null && H_BS.activeSelf)
	                {
	                    H_BS.SetActive(false);
	                    if (Drop1 != null) Drop1.SetActive(false);
	                    if (CM != null) CM.FillPlate();
	                    StopSetOfTexts();
	                    if (Ring != null) Ring.SetActive(false);
	                    var audio = GetComponent<AudioSource>();
	                    if (audio != null && FillSeedy != null) audio.PlayOneShot(FillSeedy);
	                }
	                else
	                {
	                    ShowNeedText("An empty bowl");
	                }
	            }
	        }
	        else
	        {
	            TurnOffItemTexts();
	            buttonClicked = false;
	            if (FuelTankButton != null) FuelTankButton.SetActive(false);
	            if (Ring != null) Ring.SetActive(false);
	            if (HP != null)
	            {
	                HP.FillingTank = false;
	                HP.RotatingWinch = false;
	            }
	            if (RotateWinchButton != null) RotateWinchButton.SetActive(false);
	        }

	        if (H_ME != null && H_ME.activeSelf && PlayerPrefs.GetFloat("Melon_PickUpsCount") != 4f && !TookMelon)
	        {
	            TookMelon = true;
	            ShowNeedText("There's something inside this watermelon");
	            PlayerPrefs.SetFloat("Melon_PickUpsCount", PlayerPrefs.GetFloat("Melon_PickUpsCount") + 1f);
	            PlayerPrefs.Save();
	        }
	        if (((H_PP1 != null && H_PP1.activeSelf) || (H_PP2 != null && H_PP2.activeSelf) || (H_PP3 != null && H_PP3.activeSelf) || (H_PP4 != null && H_PP4.activeSelf)) && PlayerPrefs.GetFloat("Pics_PickUpsCount") != 3f && !PiecePickedUp)
	        {
	            PiecePickedUp = true;
	            ShowNeedText("Looks like a piece of a puzzle. Maybe I can place it somewhere");
	            PlayerPrefs.SetFloat("Pics_PickUpsCount", PlayerPrefs.GetFloat("Pics_PickUpsCount") + 1f);
	            PlayerPrefs.Save();
	        }
	        if (H_BO != null && H_BO.activeSelf && PlayerPrefs.GetFloat("Book_PickUpsCount") != 5f && !BookPickedUp)
	        {
	            BookPickedUp = true;
	            ShowNeedText("This is no ordinary book. It must have a use");
	            PlayerPrefs.SetFloat("Book_PickUpsCount", PlayerPrefs.GetFloat("Book_PickUpsCount") + 1f);
	            PlayerPrefs.Save();
	        }
	    }
}
