using System.IO;
using UnityEditor;
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
}
