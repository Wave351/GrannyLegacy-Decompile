# Granny: Legacy — Decompile

A Unity project of **Granny: Legacy**, rebuilt from the shipped game.

The released game was compiled with IL2CPP, so its C# scripts weren't in the files. They were rebuilt by hand from Ghidra pseudocode of `GameAssembly.dll` and the Il2CppDumper metadata, using the assets AssetRipper exported. Every script used in the game's scenes now has a working implementation. The project compiles with 0 errors and builds as a playable Windows game.

> This is an unofficial fan project for study and preservation. It isn't affiliated with the original developers. All game content belongs to its owners.

## What's in this repo

| Path | Contents |
| --- | --- |
| `Assets/1_Scenes` | `Menu`, `Scene` (the house) and `EndScene` |
| `Assets/Scripts/Assembly-CSharp` | Game scripts. `Foo.cs` keeps the original serialized fields and GUID; `Foo.Ported.cs` holds the restored logic as a `partial` class |
| `Assets/Editor` | `BuildGame.cs` (Windows build) and `FixDynamicFonts.cs` (rebuilds TextMeshPro font atlases lost in the export) |
| `ProjectSettings`, `Packages` | Unity settings and package list |

**Large binary assets aren't in git:** textures, audio, meshes, video, cubemaps and sprites (see `.gitignore`). They're in the asset pack on the [Releases](../../releases) page.

## Setup

1. Install **Unity 6000.5.5f1** exactly. Use Unity Hub → Installs → Install Editor → Archive if it isn't listed.
2. Clone this repo, or download it as a zip.
3. Download `GrannyLegacyDecompile-v1.0.1-Assets.zip` from the [latest release](../../releases/latest). Extract it **into the project folder** (the one with `Assets` and `ProjectSettings`), merging with the existing `Assets` folder.
4. Open the folder in Unity 6000.5.5f1. The first import takes a while.
5. Open `Assets/1_Scenes/Menu.unity` and press Play.

Don't use your own AssetRipper export instead of the asset pack. A fresh export gives the files different GUIDs, so scenes and materials can't find them: the map turns invisible and everything looks white or grey.

### Troubleshooting

- **Hundreds of `CS0121: The call is ambiguous ... TMPro.TMP_ExtensionMethods` errors in `Library/PackageCache/com.unity.ugui...`:** the project was opened in a different Unity version. Close Unity, delete the `Library` folder, and reopen the project in **6000.5.5f1**.
- **White or grey UI, missing map:** the asset pack isn't extracted, or was extracted into the wrong folder. You should end up with `Assets/Texture2D`, `Assets/Mesh` and `Assets/AudioClip` folders full of files.

## Building

You need **Unity 6000.5.5f1**.

- **In the editor:** *Granny → Build Windows Game*.
- **From the command line:**
  ```
  Unity.exe -batchmode -quit -projectPath <this folder> -executeMethod BuildGame.BuildWindows -logFile build.log
  ```

The game is built to `../../Build/GrannyLegacyDecompile/Granny Legacy Decompile.exe`.

## Differences from the original

- **F11** or **Alt+Enter** switches between fullscreen and a 1280×720 window. A dragged window snaps back to 16:9 so the menus don't stretch.
- The Unity splash logo is removed, and the exe uses the original game's icon.
- Some third-party plugins (ootii motion controller, Bakery, Steamworks, Game Jolt) only came through as empty stubs:
  - Enemy head-look uses a fallback script (`BoneLookFallback.cs`).
  - Steam and Game Jolt features don't do anything.
- AssetRipper can't recover compiled shaders, so several were rewritten:
  - Bakery/Standard, enemy glow, spectral, skybox, lava and water, and ProBuilder vertex colour.
  - Lighting can look slightly different from the original.

## Status

Every game script is restored, including ones the current scenes don't use: CandyBar, OpenPresent, Skull, Traveler, the AQUAS water scripts and the TextMesh Pro chat, dropdown and env-map samples. Only TextMesh Pro's own benchmark and example scripts are still empty. The game never uses them.

Large parts of the game haven't been played through end to end. Bug reports in the Issues tab are welcome.

## Branches

- `master`: current work.
- `playtest-fixes`: an earlier round of fixes from play-testing.
