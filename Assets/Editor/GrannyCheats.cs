using UnityEditor;
using UnityEngine;

// Editor-only test helper: unlocks every item colour skin in the shop and sets the coin balance.
// Writes the same PlayerPrefs keys the game uses (ToggleOn_CertainColor checks "<Colour>_<Item>" == 1,
// MainMenu reads the float "Coins").
public static class GrannyCheats
{
    static readonly string[] Colours = { "Blue", "Brown", "Green", "Grey", "Orange", "Pink", "Purple", "Rainbow", "Red", "White", "Yellow" };
    static readonly string[] Items = { "CarKey", "ChainCutter", "Pliers", "Screwdriver", "WPKey" };

    [MenuItem("Granny/Cheats/Unlock All Skins + 100 Coins")]
    static void UnlockAllSkinsAndCoins()
    {
        int count = 0;
        foreach (var colour in Colours)
            foreach (var item in Items) { PlayerPrefs.SetInt(colour + "_" + item, 1); count++; }
        PlayerPrefs.SetFloat("Coins", 100f);
        PlayerPrefs.Save();
        Debug.Log($"[GrannyCheats] Unlocked {count} item skins and set coins to 100.");
    }
}
