using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Added for the decompile (not in the original game): a splash-style intro shown once per launch before the main menu.
// The menu is switched off before any of its Start/Update/music runs and is loaded fresh once the intro ends, so the
// game is not running underneath. In the editor it first replays the original splash logos (Unity only shows those
// in built players); every card, including the decompile credit, uses the same black backdrop and fade timing.
public class DecompileIntro : MonoBehaviour
{
	static bool shown;
	const float CardTime = 2f, Fade = .4f, CreditTime = 3f;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void Install()
	{
		var menu = SceneManager.GetActiveScene();
		if (shown || menu.name != "Menu") return;
		shown = true;
		// Runs after the menu's Awake/OnEnable but before any Start: shut the menu down so nothing plays behind us.
		foreach (var root in menu.GetRootGameObjects()) root.SetActive(false);
		var intro = SceneManager.CreateScene("DecompileIntro");
		SceneManager.SetActiveScene(intro);
		var go = new GameObject("DecompileIntro");
		SceneManager.MoveGameObjectToScene(go, intro);
		go.AddComponent<DecompileIntro>();
		SceneManager.UnloadSceneAsync(menu);
	}

	Image logo;
	CanvasGroup credit;
	bool skipped;

	void Awake()
	{
		// Own camera so the screen is plain black while the menu is unloaded.
		var cam = new GameObject("IntroCamera").AddComponent<Camera>();
		cam.transform.SetParent(transform, false);
		cam.clearFlags = CameraClearFlags.SolidColor;
		cam.backgroundColor = Color.black;
		cam.cullingMask = 0;
		gameObject.AddComponent<AudioListener>();

		var canvas = gameObject.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 32000;
		var scaler = gameObject.AddComponent<CanvasScaler>();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.referenceResolution = new Vector2(1920, 1080);
		scaler.matchWidthOrHeight = .5f;

		var bg = Make<Image>("Backdrop", transform);
		bg.color = Color.black;
		Stretch(bg.rectTransform);

		logo = Make<Image>("SplashLogo", transform);
		logo.preserveAspect = true;
		logo.rectTransform.sizeDelta = new Vector2(1100, 700);
		logo.color = new Color(1, 1, 1, 0);

		// The decompile credit, laid out like one of the splash logos: a single centred card.
		var card = new GameObject("CreditCard", typeof(RectTransform));
		card.transform.SetParent(transform, false);
		credit = card.AddComponent<CanvasGroup>();
		credit.alpha = 0;
		var c = card.transform;

		var cat = Resources.Load<Texture2D>("DecompileIntro/cat");
		if (cat)
		{
			var img = Make<RawImage>("Cat", c);
			img.texture = cat;
			float h = 430f, w = h * cat.width / Mathf.Max(1, cat.height);
			img.rectTransform.sizeDelta = new Vector2(w, h);
			img.rectTransform.anchoredPosition = new Vector2(0, 95);
		}

		var font = Resources.Load<Font>("DecompileIntro/IntroFont");
		if (!font) font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		var text = Make<Text>("Credit", c);
		text.font = font;
		text.text = "Decompile made by Claude";
		text.fontSize = 72;
		text.alignment = TextAnchor.MiddleLeft;
		text.color = new Color(.851f, .467f, .341f);
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		var outline = text.gameObject.AddComponent<UnityEngine.UI.Outline>();               // white rim like the OmGi card's lettering
		outline.effectColor = Color.white;
		outline.effectDistance = new Vector2(3, -3);
		float textWidth = text.preferredWidth, iconSize = 110, gap = 28;
		float total = iconSize + gap + textWidth;
		text.rectTransform.sizeDelta = new Vector2(textWidth + 10, 110);
		text.rectTransform.anchoredPosition = new Vector2(-total / 2 + iconSize + gap + textWidth / 2, -230);

		var spark = Make<RawImage>("ClaudeIcon", c);
		spark.texture = SparkTexture(256);
		spark.rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
		spark.rectTransform.anchoredPosition = new Vector2(-total / 2 + iconSize / 2, -230);
	}

	bool SkipPressed(float elapsed)
	{
		if (!skipped && elapsed > .3f && (Input.anyKeyDown || Input.GetMouseButtonDown(0))) skipped = true;
		return skipped;
	}

	static float CardAlpha(float t, float length) { return Mathf.Clamp01(Mathf.Min(t / Fade, (length - t) / Fade)); }

	static Sprite[] OriginalLogos()
	{
#if UNITY_EDITOR
		// Same sprites and order as PlayerSettings.m_SplashScreenLogos (2 s each in the original, minus its Unity card).
		string[] paths = { "Assets/Sprite/DVloperskylt3.asset", "Assets/Sprite/OmGiCredit_GrannyProjects.asset" };  // "Made with Unity" card removed on request
		var list = new System.Collections.Generic.List<Sprite>();
		foreach (var p in paths) { var sp = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(p); if (sp) list.Add(sp); }
		return list.ToArray();
#else
		return new Sprite[0];                                                // builds already show them via Unity's splash
#endif
	}

	IEnumerator Start()
	{
		foreach (var sprite in OriginalLogos())
		{
			logo.sprite = sprite;
			for (float t = 0; t < CardTime && !SkipPressed(t); t += Time.unscaledDeltaTime)
			{
				logo.color = new Color(1, 1, 1, CardAlpha(t, CardTime));
				yield return null;
			}
			logo.color = new Color(1, 1, 1, 0);
			if (skipped) break;
		}
		skipped = false;
		for (float t = 0; t < CreditTime && !SkipPressed(t); t += Time.unscaledDeltaTime)
		{
			credit.alpha = CardAlpha(t, CreditTime);
			yield return null;
		}
		SceneManager.LoadScene("Menu");                                       // fresh menu; the intro scene goes with it
	}

	T Make<T>(string name, Transform parent) where T : Graphic
	{
		var go = new GameObject(name, typeof(RectTransform));
		go.transform.SetParent(parent, false);
		var g = go.AddComponent<T>();
		g.raycastTarget = false;
		return g;
	}

	static void Stretch(RectTransform rt)
	{
		rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = rt.offsetMax = Vector2.zero;
	}

	// Claude-style spark: a ring of rounded rays in Claude orange, drawn into a texture at runtime.
	static Texture2D SparkTexture(int size)
	{
		var tex = new Texture2D(size, size, TextureFormat.RGBA32, false) { wrapMode = TextureWrapMode.Clamp };
		var orange = new Color(.851f, .467f, .341f);
		const int rays = 12;
		var px = new Color[size * size];
		for (int y = 0; y < size; y++)
			for (int x = 0; x < size; x++)
			{
				float u = (x + .5f) / size * 2 - 1, v = (y + .5f) / size * 2 - 1;
				float r = Mathf.Sqrt(u * u + v * v), a = Mathf.Atan2(v, u);
				float petal = Mathf.Pow(Mathf.Abs(Mathf.Cos(a * rays * .5f)), 6f);
				float alpha = Mathf.Clamp01((.2f + .74f * petal - r) * size * .25f);
				px[y * size + x] = new Color(orange.r, orange.g, orange.b, alpha);
			}
		tex.SetPixels(px);
		tex.Apply();
		return tex;
	}
}
