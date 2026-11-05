using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/AQUAS_Lite_CamNav.c (Granny Legacy, IL2CPP x64).
namespace AQUAS_Lite
{
	public partial class AQUAS_Lite_CamNav
	{
		public AQUAS_Lite_CamNav() { freeLookEnabled = true; showCursor = true; lookSpeed = 5f; moveSpeed = 5f; sprintSpeed = 50f; }
		// AQUAS demo fly camera: mouse look, WASD to move, Q/E up/down, Left Shift to sprint.
		private void Start()
		{
		    m_yaw = transform.rotation.eulerAngles.y;
		    m_pitch = transform.rotation.eulerAngles.x;
		    Cursor.visible = showCursor;
		}
		private void Update()
		{
		    if (!freeLookEnabled) return;
		    m_yaw = (m_yaw + lookSpeed * Input.GetAxis("Mouse X")) % 360f;
		    m_pitch = (m_pitch - lookSpeed * Input.GetAxis("Mouse Y")) % 360f;
		    transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up) * Quaternion.AngleAxis(m_pitch, Vector3.right);
		    float speed = Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed);
		    float forward = Input.GetAxis("Vertical") * speed;
		    float strafe = Input.GetAxis("Horizontal") * speed;
		    float lift = ((Input.GetKey(KeyCode.Q) ? 1f : 0f) - (Input.GetKey(KeyCode.E) ? 1f : 0f)) * speed;
		    transform.position += transform.forward * forward + transform.right * strafe + Vector3.up * lift;
		}
	}
}
