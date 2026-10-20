using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
	public class TextMeshProFloatingText : MonoBehaviour
	{
		public Font TheFont;

		private GameObject m_floatingText;

		private TextMeshPro m_textMeshPro;

		private TextMesh m_textMesh;

		private Transform m_transform;

		private Transform m_floatingText_Transform;

		private Transform m_cameraTransform;

		private Vector3 lastPOS;

		private Quaternion lastRotation;

		public int SpawnType;

		public bool IsTextObjectScaleStatic;

		private static WaitForEndOfFrame k_WaitForEndOfFrame;

		private static WaitForSeconds[] k_WaitForSecondsRandom;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public IEnumerator DisplayTextMeshProFloatingText()
		{
			return null;
		}

		public IEnumerator DisplayTextMeshFloatingText()
		{
			return null;
		}
	}
}
