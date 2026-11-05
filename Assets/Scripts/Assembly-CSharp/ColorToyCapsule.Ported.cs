using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ported from Annotated-Pseudocode/ColorToyCapsule.c (Granny Legacy, IL2CPP x64).
public partial class ColorToyCapsule
{
	static readonly string[] CapsuleItems = { "CarKey", "WPKey", "Screwdriver", "Pliers", "ChainCutter" };
	static Tier MakeTier(string name, float chance) { return new Tier { name = name, chance = chance, items = (string[])CapsuleItems.Clone() }; }
	// Colour tiers and their base odds (percent weights) for the menu's toy-capsule machine.
	private void Start()
	{
	    tiers = new[]
	    {
	        MakeTier("Rainbow", .388f), MakeTier("Pink", 2.326f), MakeTier("Purple", 5.426f), MakeTier("Red", 6.977f),
	        MakeTier("Yellow", 6.977f), MakeTier("Orange", 8.527f), MakeTier("Green", 10.853f), MakeTier("Blue", 13.953f),
	        MakeTier("Grey", 18.605f), MakeTier("Brown", 25.969f),
	    };
	    UpdateRarityUI();
	}
	// "- Colour (x.x%)" for every tier, using the odds after fully collected tiers are reduced to a tenth.
	public void UpdateRarityUI()
	{
	    float total = ActiveChance();
	    string text = "Color Rarity :\n";
	    foreach (var tier in tiers)
	        text += string.Format("- {0} ({1:F1}%)\n", tier.name, AdjustedChance(tier) / total * 100f);
	    if (ColorRarityText) ColorRarityText.text = text;
	}
	// Costs half a coin: hides the menu buttons, plays the capsule animation and rolls a skin.
	public void AttemptInsertCoin()
	{
	    if (AnimatedToyCapsule.isPlaying) return;
	    float coins = PlayerPrefs.GetFloat("Coins", 0f);
	    if (coins >= .5f)
	    {
	        PlayerPrefs.SetFloat("Coins", coins - .5f);
	        InvB.SetActive(false); BackB.SetActive(false); InsertCoin.SetActive(false);
	        AnimatedToyCapsule.Play("TakeOutBall");
	        UnlockCertainSkin();
	        StartCoroutine(TimerToyCapsule());
	    }
	    else if (TextNotEnough)
	    {
	        var text = TextNotEnough.GetComponent<TextManager>();
	        if (text) { text.Add(); TextNotEnough.SetActive(true); }
	    }
	}
	public bool IsTierFullyUnlocked(Tier tier)
	{
	    foreach (var item in tier.items) if (PlayerPrefs.GetInt(tier.name + "_" + item, 0) == 0) return false;
	    return true;
	}
	public float AdjustedChance(Tier tier) { return IsTierFullyUnlocked(tier) ? tier.chance * .1f : tier.chance; }
	public float ActiveChance()
	{
	    float total = 0;
	    foreach (var tier in tiers) total += AdjustedChance(tier);
	    return total;
	}
	// Weighted roll over the tiers, then a random item of that colour. Duplicates refund a quarter coin later.
	public void UnlockCertainSkin()
	{
	    float roll = UnityEngine.Random.Range(0f, ActiveChance());
	    float sum = 0;
	    foreach (var tier in tiers)
	    {
	        sum += AdjustedChance(tier);
	        if (roll > sum) continue;
	        string item = tier.items[UnityEngine.Random.Range(0, tier.items.Length)];
	        string key = tier.name + "_" + item;
	        ActivateItem(item, tier.name);
	        string label = item == "WPKey" ? " Weapon Key <" : item == "ChainCutter" ? " Chain Cutter <" : item == "CarKey" ? " Car Key <" : null;
	        ItemText.text = label != null ? "> " + tier.name + label : "> " + tier.name + " " + item + " <";
	        if (PlayerPrefs.GetInt(key, 0) == 1) { GiveCoinBack = true; return; }
	        GiveCoinBack = false;
	        PlayerPrefs.SetInt(key, 1);
	        PlayerPrefs.Save();
	        gamejoltMenuScript.SkinToCloud(key);
	        return;
	    }
	}
	public void ActivateItem(string itemName, string Color)
	{
	    GameObject item = itemName == "CarKey" ? CarKey : itemName == "WPKey" ? WeaponKey : itemName == "Screwdriver" ? Screwdriver
	                    : itemName == "Pliers" ? Pliers : ChainCutter;
	    string type = itemName == "CarKey" || itemName == "WPKey" || itemName == "Screwdriver" || itemName == "Pliers" ? itemName : "ChainCutter";
	    item.SetActive(true);
	    var texture = GetTextureByColor(Color, type);
	    var renderer = item.GetComponent<Renderer>();
	    if (renderer) renderer.material.mainTexture = texture;
	}
	public Texture GetTextureByColor(string color, string Type)
	{
	    Texture2D[] set =
	        Type == "CarKey" ? new[] { CarKey_Rainbow, CarKey_Pink, CarKey_Purple, CarKey_Red, CarKey_Yellow, CarKey_Orange, CarKey_Green, CarKey_Blue, CarKey_Gray, CarKey_Brown } :
	        Type == "WPKey" ? new[] { WPKey_Rainbow, WPKey_Pink, WPKey_Purple, WPKey_Red, WPKey_Yellow, WPKey_Orange, WPKey_Green, WPKey_Blue, WPKey_Gray, WPKey_Brown } :
	        Type == "Screwdriver" ? new[] { Screwdriver_Rainbow, Screwdriver_Pink, Screwdriver_Purple, Screwdriver_Red, Screwdriver_Yellow, Screwdriver_Orange, Screwdriver_Green, Screwdriver_Blue, Screwdriver_Gray, Screwdriver_Brown } :
	        Type == "Pliers" ? new[] { Pliers_Rainbow, Pliers_Pink, Pliers_Purple, Pliers_Red, Pliers_Yellow, Pliers_Orange, Pliers_Green, Pliers_Blue, Pliers_Gray, Pliers_Brown } :
	        Type == "ChainCutter" ? new[] { ChainCutter_Rainbow, ChainCutter_Pink, ChainCutter_Purple, ChainCutter_Red, ChainCutter_Yellow, ChainCutter_Orange, ChainCutter_Green, ChainCutter_Blue, ChainCutter_Gray, ChainCutter_Brown } :
	        null;
	    if (set == null) return null;
	    int i = System.Array.IndexOf(new[] { "Rainbow", "Pink", "Purple", "Red", "Yellow", "Orange", "Green", "Blue", "Grey", "Brown" }, color);
	    return i >= 0 ? set[i] : null;
	}
	// After the 7 s capsule animation: restore the menu buttons, hide the prize and refund duplicates.
	private IEnumerator TimerToyCapsule()
	{
	    yield return new WaitForSeconds(7f);
	    InvB.SetActive(true); BackB.SetActive(true); InsertCoin.SetActive(true);
	    CarKey.SetActive(false); Pliers.SetActive(false); Screwdriver.SetActive(false); WeaponKey.SetActive(false); ChainCutter.SetActive(false);
	    ItemText.gameObject.SetActive(false);
	    UpdateRarityUI();
	    if (GiveCoinBack) PlayerPrefs.SetFloat("Coins", PlayerPrefs.GetFloat("Coins", 0f) + .25f);
	}
}
