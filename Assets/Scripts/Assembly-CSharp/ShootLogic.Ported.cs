using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ShootLogic.c (Granny Legacy, IL2CPP x64).
public partial class ShootLogic
{
	private void Update()
	{
	    var ai = EC && EC.Grandpa ? EC.Grandpa.GetComponent<AI_Grandpa>() : null;
	    if (ai && ai.IsShooting && SpineGrandpa && Player)
	    {
	        Vector3 direction = (Player.position - SpineGrandpa.position).normalized;
	        if (direction.sqrMagnitude > .000001f) SpineGrandpa.rotation = Quaternion.Slerp(SpineGrandpa.rotation, Quaternion.LookRotation(direction, Vector3.up), 5 * Time.deltaTime);
	    }
	    if (!Shooting) { Distance = 0; return; }
	    if (!Rotater || !Target) return;
	    Distance += ShootSpeed * Time.deltaTime; Rotater.LookAt(Target);
	    RaycastHit hit;
	    if (!Physics.Raycast(Rotater.position, Rotater.forward, out hit, Distance, LayersShoot) || (ai && ai.IsDying)) return;
	    var target = hit.collider.gameObject;
	    var falling = Player ? Player.GetComponent<FallingHolder>() : null;
	    if (target.CompareTag("Player") && !(WJ && WJ.IsJumping) && !(falling && (falling.isFalling || falling.isLanding)) && CanPlayerGetShoot)
	    {
	        Impact(Impactus, hit.point); Impact(BloodEffect, hit.point); ImpactSound(hit.point);
	        Shooting = false; if (Status) Status.NormalDeath(); return;
	    }
	    if (target.CompareTag("granny") && !(Status && Status.IsJumpscared))
	    {
	        Impact(BloodEffect, hit.point); ImpactSound(hit.point); Shooting = false;
	        if (EC) EC.GrannyDeathS(); return;
	    }
	    if (target.name == "RobotProtect")
	    {
	        var robot = hit.collider.GetComponent<AI_RobotEnemy>(); if (robot) robot.KillRobot();
	        Impact(SpawnImpact, hit.point);
	    }
	    else if (target.name == "Cowleg")
	    {
	        var meat = hit.collider.GetComponent<CowlegRuin>(); if (meat) meat.RuinMeat();
	        Impact(SpawnImpact, hit.point);
	    }
	    else
	    {
	        var item = target.GetComponent<ItemSeedData>();
	        if (item && item.itemName == "Gas")
	        { if (ExplosionGas) Instantiate(ExplosionGas, target.transform.position, target.transform.rotation); Destroy(target); Shooting = false; return; }
	        if (item && item.itemName == "Melon")
	        { if (MelonBlown) { MelonBlown.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation); MelonBlown.SetActive(true); } Destroy(target); Shooting = false; return; }
	        Impact(SpawnImpact, hit.point);
	        var body = hit.collider.GetComponent<Rigidbody>(); if (body) body.AddForce(Rotater.forward * 20, ForceMode.Impulse);
	    }
	    ImpactSound(hit.point); Shooting = false;
	}
	private void Impact(GameObject effect, Vector3 position) { if (effect) Instantiate(effect, position, Quaternion.identity); }
	private void ImpactSound(Vector3 position) { if (ImpactS) AudioSource.PlayClipAtPoint(ImpactS, position); }
	public void Shoot()
	{
	    Shooting = true; if (Shot) Shot.Play();
	    if (Smoke && Spawner) Instantiate(Smoke, Spawner.position, Spawner.rotation);
	}
}
