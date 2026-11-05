using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Command-line / menu build of the decompile as a Windows game:
//   Unity.exe -batchmode -quit -projectPath <project> -executeMethod BuildGame.BuildWindows -logFile <log>
public static class BuildGame
{
	public const string OutputDir = "../../Build/GrannyLegacyDecompile";

	[MenuItem("Granny/Build Windows Game")]
	public static void BuildWindows()
	{
		ApplyPlayerSettings();
		var scenes = new[] { "Assets/1_Scenes/Menu.unity", "Assets/1_Scenes/Scene.unity", "Assets/1_Scenes/EndScene.unity" };
		Directory.CreateDirectory(OutputDir);
		var options = new BuildPlayerOptions
		{
			scenes = scenes,
			locationPathName = Path.Combine(OutputDir, "Granny Legacy Decompile.exe"),
			target = BuildTarget.StandaloneWindows64,
			options = BuildOptions.None,
		};
		BuildReport report = BuildPipeline.BuildPlayer(options);
		Debug.Log($"[BuildGame] result={report.summary.result} errors={report.summary.totalErrors} size={report.summary.totalSize} time={report.summary.totalTime}");
		if (Application.isBatchMode) EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
	}

	// Window, icon and splash settings for the decompile build (also saved into ProjectSettings).
	public static void ApplyPlayerSettings()
	{
		PlayerSettings.resizableWindow = true;
		PlayerSettings.allowFullscreenSwitch = false;                      // WindowAspectLock handles Alt+Enter / F11 itself
		PlayerSettings.SplashScreen.showUnityLogo = false;                 // keep only the original D.Vloper / OmGi logos
		var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Texture2D/AppIcon.png");  // taken from the original exe
		if (icon) PlayerSettings.SetIcons(NamedBuildTarget.Unknown, new[] { icon }, IconKind.Any);
		else Debug.LogWarning("[BuildGame] AppIcon.png not found; exe keeps the default icon");
		FixCursorTexture();
		FixDynamicFonts.Run();                                             // dynamic TMP fonts lost their atlas in the export
		AssetDatabase.SaveAssets();
	}

	// The game's custom mouse pointer (Player Settings > Default Cursor) was exported as a plain compressed texture,
	// which Unity rejects as a cursor ("must be RGBA32, readable ... no mip chain"), leaving the OS arrow.
	[InitializeOnLoadMethod]
	static void FixCursorOnLoad() => EditorApplication.delayCall += FixCursorTexture;   // also for Play in the editor

	[MenuItem("Granny/Fix Cursor Texture")]
	public static void FixCursorTexture()
	{
		var cursor = PlayerSettings.defaultCursor;
		if (!cursor) { Debug.LogWarning("[BuildGame] no default cursor set"); return; }
		var importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(cursor)) as TextureImporter;
		if (importer == null || importer.textureType == TextureImporterType.Cursor) return;
		importer.textureType = TextureImporterType.Cursor;
		importer.isReadable = true;
		importer.mipmapEnabled = false;
		importer.alphaIsTransparency = true;
		importer.textureCompression = TextureImporterCompression.Uncompressed;
		importer.npotScale = TextureImporterNPOTScale.None;
		foreach (var platform in new[] { "Standalone", "Android", "iPhone", "WebGL" })
		{
			var ps = importer.GetPlatformTextureSettings(platform);
			if (!ps.overridden) continue;
			ps.overridden = false;
			importer.SetPlatformTextureSettings(ps);
		}
		importer.SaveAndReimport();
		Debug.Log("[BuildGame] cursor texture set to Cursor type: " + importer.assetPath);
	}
}
