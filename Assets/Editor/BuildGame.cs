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
		FixDynamicFonts.Run();                                             // dynamic TMP fonts lost their atlas in the export
		AssetDatabase.SaveAssets();
	}
}
