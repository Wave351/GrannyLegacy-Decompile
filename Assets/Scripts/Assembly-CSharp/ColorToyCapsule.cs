using System.Collections;
using TMPro;
using UnityEngine;

public class ColorToyCapsule : MonoBehaviour
{
	public struct Tier
	{
		public string name;

		public float chance;

		public string[] items;
	}

	public GameObject TextNotEnough;

	public Animation AnimatedToyCapsule;

	public GameObject InsertCoin;

	public GameObject BackB;

	public GameObject InvB;

	public GameObject Pliers;

	public GameObject Screwdriver;

	public GameObject CarKey;

	public GameObject ChainCutter;

	public GameObject WeaponKey;

	public TMP_Text ItemText;

	public Texture2D CarKey_Rainbow;

	public Texture2D CarKey_Pink;

	public Texture2D CarKey_Purple;

	public Texture2D CarKey_Red;

	public Texture2D CarKey_Yellow;

	public Texture2D CarKey_Orange;

	public Texture2D CarKey_Green;

	public Texture2D CarKey_Blue;

	public Texture2D CarKey_Gray;

	public Texture2D CarKey_Brown;

	public Texture2D WPKey_Rainbow;

	public Texture2D WPKey_Pink;

	public Texture2D WPKey_Purple;

	public Texture2D WPKey_Red;

	public Texture2D WPKey_Yellow;

	public Texture2D WPKey_Orange;

	public Texture2D WPKey_Green;

	public Texture2D WPKey_Blue;

	public Texture2D WPKey_Gray;

	public Texture2D WPKey_Brown;

	public Texture2D Pliers_Rainbow;

	public Texture2D Pliers_Pink;

	public Texture2D Pliers_Purple;

	public Texture2D Pliers_Red;

	public Texture2D Pliers_Yellow;

	public Texture2D Pliers_Orange;

	public Texture2D Pliers_Green;

	public Texture2D Pliers_Blue;

	public Texture2D Pliers_Gray;

	public Texture2D Pliers_Brown;

	public Texture2D ChainCutter_Rainbow;

	public Texture2D ChainCutter_Pink;

	public Texture2D ChainCutter_Purple;

	public Texture2D ChainCutter_Red;

	public Texture2D ChainCutter_Yellow;

	public Texture2D ChainCutter_Orange;

	public Texture2D ChainCutter_Green;

	public Texture2D ChainCutter_Blue;

	public Texture2D ChainCutter_Gray;

	public Texture2D ChainCutter_Brown;

	public Texture2D Screwdriver_Rainbow;

	public Texture2D Screwdriver_Pink;

	public Texture2D Screwdriver_Purple;

	public Texture2D Screwdriver_Red;

	public Texture2D Screwdriver_Yellow;

	public Texture2D Screwdriver_Orange;

	public Texture2D Screwdriver_Green;

	public Texture2D Screwdriver_Blue;

	public Texture2D Screwdriver_Gray;

	public Texture2D Screwdriver_Brown;

	public bool GiveCoinBack;

	public TMP_Text ColorRarityText;

	public GameJoltMenu gamejoltMenuScript;

	public Tier[] tiers;

	private void Start()
	{
	}

	public void UpdateRarityUI()
	{
	}

	public void AttemptInsertCoin()
	{
	}

	public bool IsTierFullyUnlocked(Tier tier)
	{
		return false;
	}

	public float AdjustedChance(Tier tier)
	{
		return 0f;
	}

	public float ActiveChance()
	{
		return 0f;
	}

	public void UnlockCertainSkin()
	{
	}

	public void ActivateItem(string itemName, string Color)
	{
	}

	public Texture GetTextureByColor(string color, string Type)
	{
		return null;
	}

	private IEnumerator TimerToyCapsule()
	{
		return null;
	}
}
