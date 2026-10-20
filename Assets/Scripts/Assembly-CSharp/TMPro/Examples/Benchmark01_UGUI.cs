using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro.Examples
{
	public class Benchmark01_UGUI : MonoBehaviour
	{
		public int BenchmarkType;

		public Canvas canvas;

		public TMP_FontAsset TMProFont;

		public Font TextMeshFont;

		private TextMeshProUGUI m_textMeshPro;

		private Text m_textMesh;

		private const string label01 = "The <#0050FF>count is: </color>";

		private const string label02 = "The <color=#0050FF>count is: </color>";

		private Material m_material01;

		private Material m_material02;

		private IEnumerator Start()
		{
			return null;
		}
	}
}
