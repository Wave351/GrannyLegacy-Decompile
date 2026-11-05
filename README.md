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

**Large binary assets aren't included:** textures, audio, meshes, video, cubemaps and sprites (see `.gitignore`). To open or build the project, re-export them from your own copy of the game with AssetRipper and copy them into `Assets/`.

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

Scripts that the scenes don't use are still empty stubs: CandyBar, ChatController, DropdownSample, EnvMapAnimator, OpenPresent, Skull and Traveler.

Large parts of the game haven't been played through end to end. Bug reports in the Issues tab are welcome.

## Branches

- `master`: current work.
- `playtest-fixes`: an earlier round of fixes from play-testing.
