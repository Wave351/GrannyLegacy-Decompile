using UnityEngine;

// Added for the decompile: the menus and HUD are laid out for 16:9 and stretch at other shapes, so when the
// (now resizable) window is resized it snaps back to 16:9 once the user lets go. Fullscreen is left alone.
public class WindowAspectLock : MonoBehaviour
{
	const float Aspect = 16f / 9f;
	int lastW, lastH;
	float settle;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static void Install()
	{
		if (Application.isEditor) return;                                    // the Game view has its own aspect setting
		var go = new GameObject("WindowAspectLock");
		DontDestroyOnLoad(go);
		go.AddComponent<WindowAspectLock>();
	}

	void Update()
	{
		if (Screen.fullScreenMode != FullScreenMode.Windowed) { lastW = lastH = 0; return; }
		int w = Screen.width, h = Screen.height;
		if (w != lastW || h != lastH) { lastW = w; lastH = h; settle = .35f; return; }   // still being dragged
		if (settle <= 0) return;
		settle -= Time.unscaledDeltaTime;
		if (settle > 0) return;
		if (Mathf.Abs((float)w / h - Aspect) < .01f) return;
		// Keep whichever edge the user changed most, and fit the other one to 16:9.
		int newW = w, newH = Mathf.RoundToInt(w / Aspect);
		if (newH > Display.main.systemHeight - 80) { newH = h; newW = Mathf.RoundToInt(h * Aspect); }
		Screen.SetResolution(newW, newH, FullScreenMode.Windowed);
		lastW = newW; lastH = newH;
	}
}
