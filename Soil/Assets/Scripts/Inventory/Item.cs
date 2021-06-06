using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Item
{
    public enum Rarity { Crap, Common, Uncommon, Rare, Amazing, Legendary}

    public Rarity rarity;

    public GameObject dropPrefab;

    public Texture2D inventorySprite;

    public string displayName;

    public string description;
}
