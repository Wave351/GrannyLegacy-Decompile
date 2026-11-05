using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Places grates, locks and blocks on the whole map for the Extra Difficulties mod, and the purple key's spots.
// Granny > Extra Difficulty Placer. Add items, drag them into place, then Export. A placement shows on its difficulty and
// every harder one. Drag an item to another difficulty's group in the Hierarchy to change its difficulty.
// The mod's Placements.txt is the saved copy, so nothing here needs saving in the scene.
public class ExtraDifficultyPlacer : EditorWindow
{
    private const string SceneFile = "Assets/1_Scenes/Scene.unity";
    private const string RootName = "== Extra Difficulty Placements ==";
    private static readonly string[] TierNames = { "Heavenly", "Daemon", "Daemon+" };
    private static readonly string[] Kinds = { "Grate", "Lock", "Block", "Vase" };
    private const int VasesWanted = 4;
    // Creaking floors: each difficulty has its own set, used on that difficulty only. Until a set is exported, the game's
    // floors as the mod widens them (CreakFloorsDefault.txt) are what each starts from.
    private static readonly Color CreakColour = new Color(1f, .55f, .1f, .35f);
    private static string CreakDefaultFile => Path.Combine(ModFolder, "CreakFloorsDefault.txt");
    private static string CreakGroupName(int tier) => "Creak Floors - " + TierNames[tier];
    private static readonly Color VaseYellow = new Color(1f, .85f, .2f);
    private int _tier;

    // The purple key (Daemon and Daemon+) appears at one of these spots, picked at random each run.
    private const string PurpleGroupName = "Purple Key Spots";
    private const int PurpleWanted = 16;
    private static readonly Color Purple = new Color(.55f, .15f, .95f);
    // The mod's built-in spots, used until spots are exported from here.
    private static readonly Vector3[] DefaultPurpleSpots =
    {
        new Vector3(5.62f, 35.50f, 9.07f), new Vector3(23.11f, 13.63f, -18.99f), new Vector3(3.73f, 17.72f, 3.60f), new Vector3(-5.84f, 15.51f, -18.16f),
        new Vector3(25.56f, 3.80f, -0.97f), new Vector3(7.12f, 7.79f, 0.76f), new Vector3(-16.12f, 30.38f, 7.22f), new Vector3(-13.09f, 23.17f, 0.01f),
        new Vector3(12.81f, 13.60f, 0.18f), new Vector3(5.86f, 23.28f, 11.29f), new Vector3(-10.22f, 14.69f, -29.50f), new Vector3(30.97f, 5.66f, -23.18f),
        new Vector3(9.59f, 21.29f, 12.32f), new Vector3(-19.20f, 15.25f, 1.56f), new Vector3(12.52f, 26.96f, 3.87f), new Vector3(8.11f, 12.61f, -34.85f),
    };

    // Project folder: GrannyLegacy-decompile/UnityProject/ExportedProject
    private static string ModFolder => Path.GetFullPath(Path.Combine(Application.dataPath, "../../../../GrannyLegacyExtraDifficulties"));
    private static string SaveFile => Path.Combine(ModFolder, "Placements.txt");

    [MenuItem("Granny/Extra Difficulty Placer")]
    private static void Open() => GetWindow<ExtraDifficultyPlacer>("Difficulty Placer");

    private void OnGUI()
    {
        EditorGUILayout.Space();
        if (GUILayout.Button("Open map and load my placements", GUILayout.Height(30))) OpenMap();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Difficulty for new items", EditorStyles.boldLabel);
        int was = _tier;
        _tier = GUILayout.Toolbar(_tier, TierNames);
        if (_tier != was) ShowCreak(_tier);
        EditorGUILayout.HelpBox(_tier == 0 ? "Shows on Heavenly, Daemon and Daemon+." : _tier == 1 ? "Shows on Daemon and Daemon+." : "Shows on Daemon+ only.", MessageType.None);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Add at the Scene view's centre, then drag into place", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        foreach (string kind in Kinds)
            if (GUILayout.Button(kind, GUILayout.Height(40))) Add(kind, _tier);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox("W moves, E rotates, R scales. Ctrl+D copies an item. Delete removes it.", MessageType.None);
        int vases = 0;
        for (int i = 0; i < TierNames.Length; i++)
        {
            Transform group = Group(i, false);
            if (group != null) for (int c = 0; c < group.childCount; c++) if (group.GetChild(c).name.StartsWith("Vase", StringComparison.Ordinal)) vases++;
        }
        EditorGUILayout.HelpBox($"Yellow vases: {vases} of {VasesWanted}. A real vase in the game, yellow; it can't be scaled. Use 'Drop selected onto the surface below' to set one down.",
            vases == VasesWanted ? MessageType.None : MessageType.Warning);

        EditorGUILayout.Space();
        Transform creak = CreakGroup(_tier, false);
        EditorGUILayout.LabelField($"Creaking floors on {TierNames[_tier]}: {(creak != null ? creak.childCount : 0)}", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add creaking floor", GUILayout.Height(30))) AddCreak(_tier);
        if (GUILayout.Button("Copy to the other difficulties", GUILayout.Height(30))) CopyCreak(_tier);
        if (GUILayout.Button("Reset to the game's", GUILayout.Height(30))) ResetCreak(_tier);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox($"The orange boxes: stepping into one creaks. Only {TierNames[_tier]}'s are shown; pick another difficulty above to edit its own. W moves, E turns, R sizes.", MessageType.None);

        EditorGUILayout.Space();
        Transform purple = PurpleGroup(false);
        int spots = purple != null ? purple.childCount : 0;
        EditorGUILayout.LabelField($"Purple key spots: {spots} of {PurpleWanted}", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add purple key spot", GUILayout.Height(30))) AddPurple();
        if (GUILayout.Button("Drop selected onto the surface below", GUILayout.Height(30))) DropSelected();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox(spots == PurpleWanted
            ? "Each run on Daemon and Daemon+, the purple key lies at one of these, picked at random, turned as you turn it here."
            : $"There should be {PurpleWanted} spots: there are {spots}. The key still uses whatever spots there are.", spots == PurpleWanted ? MessageType.None : MessageType.Warning);
        // Which game versions each spot is used on: older versions close off some areas.
        var picked = new List<GameObject>();
        if (purple != null) foreach (GameObject g in Selection.gameObjects) if (g.transform.parent == purple) picked.Add(g);
        if (picked.Count > 0)
        {
            (int lo, int hi) = SpotVersions(picked[0]);
            EditorGUILayout.LabelField($"Selected spot{(picked.Count > 1 ? "s" : "")}: used on game versions 1.{lo} to 1.{hi}");
            float flo = lo, fhi = hi;
            EditorGUILayout.MinMaxSlider(ref flo, ref fhi, 0f, 8f);
            int nlo = Mathf.RoundToInt(flo), nhi = Mathf.RoundToInt(fhi);
            if (nlo != lo || nhi != hi)
                foreach (GameObject g in picked) { Undo.RecordObject(g, "Purple key spot versions"); g.name = SpotName(nlo, nhi); }
        }
        else EditorGUILayout.HelpBox("Select purple key spots to set which game versions they're used on (an area closed off on older versions shouldn't get the key there).", MessageType.None);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Show on the map", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < TierNames.Length; i++)
        {
            Transform group = Group(i, false);
            if (group == null) continue;
            bool on = group.gameObject.activeSelf;
            bool want = GUILayout.Toggle(on, $"{TierNames[i]} ({group.childCount})");
            if (want != on) group.gameObject.SetActive(want);
        }
        if (purple != null)
        {
            bool on = purple.gameObject.activeSelf;
            bool want = GUILayout.Toggle(on, $"Purple key ({purple.childCount})");
            if (want != on) purple.gameObject.SetActive(want);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        if (GUILayout.Button("Export", GUILayout.Height(30))) Export();
        if (GUILayout.Button("Export, build and install into the game", GUILayout.Height(30))) { Export(); BuildAndInstall(); }
        EditorGUILayout.HelpBox("Saves to " + SaveFile + ". The game must be closed to install.", MessageType.None);
    }

    private static void OpenMap()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
        EditorSceneManager.OpenScene(SceneFile);
        Load();
    }

    private static Transform Root(bool create)
    {
        GameObject root = GameObject.Find(RootName);
        if (root == null && create) root = new GameObject(RootName);
        return root != null ? root.transform : null;
    }

    private static Transform Group(int tier, bool create)
    {
        Transform root = Root(create);
        if (root == null) return null;
        Transform group = root.Find(TierNames[tier]);
        if (group == null && create)
        {
            group = new GameObject(TierNames[tier]).transform;
            group.SetParent(root, false);
        }
        return group;
    }

    private static Transform CreakGroup(int tier, bool create)
    {
        Transform root = Root(create);
        if (root == null) return null;
        Transform group = root.Find(CreakGroupName(tier));
        if (group == null && create)
        {
            group = new GameObject(CreakGroupName(tier)).transform;
            group.SetParent(root, false);
        }
        return group;
    }

    private static void ShowCreak(int tier)
    {
        for (int i = 0; i < TierNames.Length; i++)
        {
            Transform group = CreakGroup(i, false);
            if (group != null) group.gameObject.SetActive(i == tier);
        }
    }

    // A see-through orange box: the creaking floor's trigger, at its size.
    private static GameObject CreateCreak(int tier, Vector3 position, Quaternion rotation, Vector3 size)
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Creak";
        DestroyImmediate(floor.GetComponent<Collider>());
        floor.transform.SetParent(CreakGroup(tier, true), false);
        floor.transform.SetPositionAndRotation(position, rotation);
        floor.transform.localScale = size;
        Shader see = Shader.Find("Sprites/Default") ?? Shader.Find("Unlit/Color");
        floor.GetComponent<Renderer>().sharedMaterial = new Material(see) { color = CreakColour };
        return floor;
    }

    private static void AddCreak(int tier)
    {
        SceneView view = SceneView.lastActiveSceneView;
        GameObject floor = CreateCreak(tier, view != null ? view.pivot : Vector3.zero, Quaternion.identity, new Vector3(3f, .7f, 3f));
        ShowCreak(tier);
        Undo.RegisterCreatedObjectUndo(floor, "Add creaking floor");
        Selection.activeGameObject = floor;
        if (view != null) view.FrameSelected();
    }

    private static void CopyCreak(int tier)
    {
        Transform from = CreakGroup(tier, false);
        if (from == null) return;
        for (int i = 0; i < TierNames.Length; i++)
        {
            if (i == tier) continue;
            Transform to = CreakGroup(i, true);
            for (int c = to.childCount - 1; c >= 0; c--) Undo.DestroyObjectImmediate(to.GetChild(c).gameObject);
            for (int c = 0; c < from.childCount; c++)
            {
                Transform t = from.GetChild(c);
                Undo.RegisterCreatedObjectUndo(CreateCreak(i, t.position, t.rotation, t.lossyScale), "Copy creaking floors");
            }
        }
        ShowCreak(tier);
        UnityEngine.Debug.Log($"Placer: copied {TierNames[tier]}'s {from.childCount} creaking floors to the other difficulties.");
    }

    private static void ResetCreak(int tier)
    {
        Transform group = CreakGroup(tier, true);
        for (int c = group.childCount - 1; c >= 0; c--) Undo.DestroyObjectImmediate(group.GetChild(c).gameObject);
        int count = SeedCreak(tier);
        ShowCreak(tier);
        UnityEngine.Debug.Log($"Placer: {TierNames[tier]} set back to the game's {count} creaking floors.");
    }

    private static int SeedCreak(int tier)
    {
        if (!File.Exists(CreakDefaultFile)) { UnityEngine.Debug.LogWarning("Placer: no " + CreakDefaultFile); return 0; }
        int count = 0;
        foreach (string raw in File.ReadAllLines(CreakDefaultFile))
        {
            string line = raw.Trim();
            if (line.Length == 0 || line[0] == '#') continue;
            string[] v = line.Split(' ');
            if (v.Length != 10) continue;
            float F(int i) => float.Parse(v[i], CultureInfo.InvariantCulture);
            CreateCreak(tier, new Vector3(F(0), F(1), F(2)), new Quaternion(F(3), F(4), F(5), F(6)), new Vector3(F(7), F(8), F(9)));
            count++;
        }
        return count;
    }

    private static Transform PurpleGroup(bool create)
    {
        Transform root = Root(create);
        if (root == null) return null;
        Transform group = root.Find(PurpleGroupName);
        if (group == null && create)
        {
            group = new GameObject(PurpleGroupName).transform;
            group.SetParent(root, false);
        }
        return group;
    }

    // A copy of one of the game's padlock keys, at its real size, tinted purple.
    private void OnSelectionChange() => Repaint();

    // A spot's game versions live in its name, "PurpleKey 1.0-1.8" (0 = 1.0 ... 8 = 1.8); a plain "PurpleKey" is all of them.
    private static string SpotName(int lo, int hi) => lo == 0 && hi == 8 ? "PurpleKey" : $"PurpleKey 1.{lo}-1.{hi}";

    private static (int, int) SpotVersions(GameObject spot)
    {
        var m = System.Text.RegularExpressions.Regex.Match(spot.name, @"1\.(\d)-1\.(\d)");
        return m.Success ? (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)) : (0, 8);
    }

    private static GameObject CreatePurple(Vector3 position, Quaternion rotation)
    {
        Transform source = null;
        foreach (Transform t in Resources.FindObjectsOfTypeAll<Transform>())
        {
            if (t.name != "PadlockKey" || !t.gameObject.scene.IsValid()) continue;
            MeshFilter mesh = t.GetComponent<MeshFilter>();
            Vector3 size = t.lossyScale;
            if (mesh != null && size.x > .0001f && size.y > .0001f && size.z > .0001f) { source = t; break; }
        }
        if (source == null) { UnityEngine.Debug.LogError("Placer: the game's padlock key wasn't found. Use 'Open map and load my placements' first."); return null; }
        GameObject item = Instantiate(source.gameObject, PurpleGroup(true));
        item.name = "PurpleKey";
        item.SetActive(true);
        foreach (MonoBehaviour script in item.GetComponentsInChildren<MonoBehaviour>(true)) DestroyImmediate(script);
        foreach (Rigidbody body in item.GetComponentsInChildren<Rigidbody>(true)) DestroyImmediate(body);
        Vector3 world = source.lossyScale, under = item.transform.parent.lossyScale;
        item.transform.localScale = new Vector3(world.x / under.x, world.y / under.y, world.z / under.z);
        item.transform.SetPositionAndRotation(position, rotation == default ? source.rotation : rotation);
        foreach (Renderer look in item.GetComponentsInChildren<Renderer>(true))
        {
            var tinted = new Material(look.sharedMaterial) { color = Purple };
            look.sharedMaterial = tinted;
        }
        return item;
    }

    private static void AddPurple()
    {
        SceneView view = SceneView.lastActiveSceneView;
        GameObject item = CreatePurple(view != null ? view.pivot : Vector3.zero, default);
        if (item == null) return;
        Undo.RegisterCreatedObjectUndo(item, "Add purple key spot");
        Selection.activeGameObject = item;
        if (view != null) view.FrameSelected();
    }

    // Sets each selected key down on whatever is under it, so it lies there instead of floating.
    private static void DropSelected()
    {
        Physics.SyncTransforms();
        int dropped = 0;
        foreach (GameObject item in Selection.gameObjects)
        {
            Collider own = item.GetComponent<Collider>();
            bool was = own != null && own.enabled;
            if (own != null) own.enabled = false;
            Renderer look = item.GetComponent<Renderer>();
            float below = look != null ? item.transform.position.y - look.bounds.min.y : 0f;
            if (Physics.Raycast(item.transform.position + Vector3.up * .3f, Vector3.down, out RaycastHit hit, 10f, ~0, QueryTriggerInteraction.Ignore))
            {
                Undo.RecordObject(item.transform, "Drop purple key spot");
                item.transform.position = hit.point + Vector3.up * (below + .005f);
                dropped++;
            }
            if (own != null) own.enabled = was;
        }
        UnityEngine.Debug.Log($"Placer: dropped {dropped} of {Selection.gameObjects.Length} selected onto the surface below.");
    }

    private static GameObject Source(string kind)
    {
        string path = kind == "Grate" ? "Difficulty Objects/Extreme+/HoleGrid"
            : kind == "Block" ? "Difficulty Objects/Hollow+/BlockedHidingSpots/BlockageHidingSpot (1)"
            : kind == "Vase" ? "Objects/RegularVase"
            : "Granny's House/Granny_SomeOthers/DoorLock/HangLock";
        string name = path.Substring(path.LastIndexOf('/') + 1);
        foreach (Transform t in Resources.FindObjectsOfTypeAll<Transform>())
            if (t.name == name && t.gameObject.scene.IsValid() && FullPath(t) == path) return t.gameObject;
        return null;
    }

    private static string FullPath(Transform t)
    {
        string path = t.name;
        for (t = t.parent; t != null; t = t.parent) path = t.name + "/" + path;
        return path;
    }

    private static GameObject Create(string kind, int tier)
    {
        GameObject source = Source(kind);
        if (source == null) { UnityEngine.Debug.LogError($"Placer: {kind} source not found. Use 'Open map and load my placements' first."); return null; }
        GameObject item = Instantiate(source, Group(tier, true));
        item.name = kind;
        item.SetActive(true);
        // Only the look and shape are kept. The mod removes the same things in-game.
        foreach (MonoBehaviour script in item.GetComponentsInChildren<MonoBehaviour>(true)) DestroyImmediate(script);
        foreach (Rigidbody body in item.GetComponentsInChildren<Rigidbody>(true)) DestroyImmediate(body);
        if (kind == "Vase")
        {
            // The game's own size, whatever the group's; tinted as it will look.
            Vector3 world = source.transform.lossyScale, under = item.transform.parent.lossyScale;
            item.transform.localScale = new Vector3(world.x / under.x, world.y / under.y, world.z / under.z);
            foreach (Renderer look in item.GetComponentsInChildren<Renderer>(true))
                look.sharedMaterial = new Material(look.sharedMaterial) { color = VaseYellow };
        }
        return item;
    }

    private static void Add(string kind, int tier)
    {
        GameObject item = Create(kind, tier);
        if (item == null) return;
        SceneView view = SceneView.lastActiveSceneView;
        if (view != null) item.transform.position = view.pivot;
        Undo.RegisterCreatedObjectUndo(item, "Add " + kind);
        Selection.activeGameObject = item;
        if (view != null) view.FrameSelected();
    }

    private static string Text(float value) => value.ToString("0.#####", CultureInfo.InvariantCulture);

    private static void Export()
    {
        var lines = new StringBuilder("# tier kind px py pz rx ry rz rw sx sy sz. Written by the Extra Difficulty Placer in Unity.\n");
        int count = 0;
        for (int i = 0; i < TierNames.Length; i++)
        {
            Transform group = Group(i, false);
            if (group == null) continue;
            for (int c = 0; c < group.childCount; c++)
            {
                Transform t = group.GetChild(c);
                string kind = Array.Find(Kinds, k => t.name.StartsWith(k, StringComparison.Ordinal));
                if (kind == null) { UnityEngine.Debug.LogWarning($"Placer: skipped '{t.name}': names must start with Grate, Lock, Block or Vase."); continue; }
                Vector3 p = t.position, s = t.lossyScale;
                Quaternion r = t.rotation;
                lines.Append(i + 1).Append(' ').Append(kind);
                foreach (float value in new[] { p.x, p.y, p.z, r.x, r.y, r.z, r.w, s.x, s.y, s.z }) lines.Append(' ').Append(Text(value));
                lines.Append('\n');
                count++;
            }
        }
        // Creaking floors, each for exactly its own difficulty; scale is the box's size.
        int floors = 0;
        for (int i = 0; i < TierNames.Length; i++)
        {
            Transform group = CreakGroup(i, false);
            if (group == null) continue;
            for (int c = 0; c < group.childCount; c++)
            {
                Transform t = group.GetChild(c);
                Vector3 p = t.position, size = t.lossyScale;
                Quaternion r = t.rotation;
                lines.Append(i + 1).Append(" Creak");
                foreach (float value in new[] { p.x, p.y, p.z, r.x, r.y, r.z, r.w, Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z) }) lines.Append(' ').Append(Text(value));
                lines.Append('\n');
                floors++;
            }
        }

        // The purple key's spots: tier 2 (Daemon) as it starts there; only position and rotation are used.
        Transform purple = PurpleGroup(false);
        int spots = 0;
        if (purple != null)
            for (int c = 0; c < purple.childCount; c++)
            {
                Transform t = purple.GetChild(c);
                Vector3 p = t.position;
                Quaternion r = t.rotation;
                // The last three fields: first and last game version it's used on, and 2 to mark them as versions.
                (int lo, int hi) = SpotVersions(t.gameObject);
                lines.Append("2 PurpleKey");
                foreach (float value in new[] { p.x, p.y, p.z, r.x, r.y, r.z, r.w, lo, hi, 2f }) lines.Append(' ').Append(Text(value));
                lines.Append('\n');
                spots++;
            }
        File.WriteAllText(SaveFile, lines.ToString());
        UnityEngine.Debug.Log($"Placer: exported {count} placements, {floors} creaking floors and {spots} purple key spots to {SaveFile}");
        if (spots != PurpleWanted) UnityEngine.Debug.LogWarning($"Placer: there are {spots} purple key spots, not {PurpleWanted}.");
    }

    private static void Load()
    {
        Transform old = Root(false);
        if (old != null) DestroyImmediate(old.gameObject);
        int count = 0, spots = 0;
        var creakLoaded = new bool[TierNames.Length];
        foreach (string raw in File.Exists(SaveFile) ? File.ReadAllLines(SaveFile) : new string[0])
        {
            string line = raw.Trim();
            if (line.Length == 0 || line[0] == '#') continue;
            string[] parts = line.Split(' ');
            if (parts.Length != 12) continue;
            float P(int i) => float.Parse(parts[i], CultureInfo.InvariantCulture);
            if (parts[1] == "Creak")
            {
                int tier = int.Parse(parts[0], CultureInfo.InvariantCulture) - 1;
                CreateCreak(tier, new Vector3(P(2), P(3), P(4)), new Quaternion(P(5), P(6), P(7), P(8)), new Vector3(P(9), P(10), P(11)));
                creakLoaded[tier] = true;
                continue;
            }
            if (parts[1] == "PurpleKey")
            {
                GameObject spot = CreatePurple(new Vector3(P(2), P(3), P(4)), new Quaternion(P(5), P(6), P(7), P(8)));
                if (spot != null)
                {
                    if (P(11) == 2f) spot.name = SpotName(Mathf.RoundToInt(P(9)), Mathf.RoundToInt(P(10))); // Older files: all versions.
                    spots++;
                }
                continue;
            }
            GameObject item = Create(parts[1], int.Parse(parts[0], CultureInfo.InvariantCulture) - 1);
            if (item == null) continue;
            float F(int i) => float.Parse(parts[i], CultureInfo.InvariantCulture);
            item.transform.SetPositionAndRotation(new Vector3(F(2), F(3), F(4)), new Quaternion(F(5), F(6), F(7), F(8)));
            item.transform.localScale = new Vector3(F(9), F(10), F(11));
            count++;
        }
        // Difficulties with no floors saved start from the game's.
        for (int i = 0; i < TierNames.Length; i++) if (!creakLoaded[i]) SeedCreak(i);
        ShowCreak(0);
        // None saved yet: the mod's built-in spots, to move from.
        if (spots == 0)
            foreach (Vector3 spot in DefaultPurpleSpots)
                if (CreatePurple(spot, default) != null) spots++;
        UnityEngine.Debug.Log($"Placer: loaded {count} placements and {spots} purple key spots.");
    }

    private static void BuildAndInstall()
    {
        string script = Path.Combine(ModFolder, "build.ps1");
        var start = new ProcessStartInfo("powershell", $"-ExecutionPolicy Bypass -File \"{script}\" -Install")
        { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true };
        using Process process = Process.Start(start);
        string output = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode == 0) UnityEngine.Debug.Log("Placer: installed.\n" + output);
        else UnityEngine.Debug.LogError("Placer: build or install failed. Is the game closed?\n" + output);
    }

    // For checking the tool from the command line: -executeMethod ExtraDifficultyPlacer.SelfTest
    public static void SelfTest()
    {
        EditorSceneManager.OpenScene(SceneFile);
        foreach (string kind in Kinds)
        {
            GameObject source = Source(kind);
            var renderer = source != null ? source.GetComponent<Renderer>() : null;
            UnityEngine.Debug.Log($"PLACERTEST {kind}: source={(source != null ? FullPath(source.transform) : "MISSING")} mesh={renderer != null} size={(renderer != null ? renderer.bounds.size.ToString() : "-")}");
            GameObject item = Create(kind, Array.IndexOf(Kinds, kind) % TierNames.Length);
            UnityEngine.Debug.Log($"PLACERTEST {kind}: created={item != null} scripts left={(item != null ? item.GetComponentsInChildren<MonoBehaviour>(true).Length : -1)}{(item != null && kind == "Vase" ? $" size={item.GetComponent<Renderer>().bounds.size} colour={item.GetComponent<Renderer>().sharedMaterial.color}" : "")}");
        }
        UnityEngine.Debug.Log($"PLACERTEST groups: {Group(0, false)?.childCount} {Group(1, false)?.childCount} {Group(2, false)?.childCount}");
        Load();
        Transform purple = PurpleGroup(false);
        UnityEngine.Debug.Log($"PLACERTEST creak floors: {CreakGroup(0, false)?.childCount} {CreakGroup(1, false)?.childCount} {CreakGroup(2, false)?.childCount}; first size={(CreakGroup(0, false) != null && CreakGroup(0, false).childCount > 0 ? CreakGroup(0, false).GetChild(0).lossyScale.ToString() : "-")}");
        UnityEngine.Debug.Log($"PLACERTEST purple spots loaded={(purple != null ? purple.childCount : 0)} first={(purple != null && purple.childCount > 0 ? purple.GetChild(0).position + " scale " + purple.GetChild(0).lossyScale : "-")}");
        if (purple != null && purple.childCount > 0)
        {
            Selection.activeGameObject = purple.GetChild(0).gameObject;
            Vector3 before = purple.GetChild(0).position;
            purple.GetChild(0).position += Vector3.up * .5f;
            DropSelected();
            UnityEngine.Debug.Log($"PLACERTEST drop: from {before} raised then dropped to {purple.GetChild(0).position}");
            purple.GetChild(0).position = before;
        }
        UnityEngine.Debug.Log("PLACERTEST done");
    }
}
