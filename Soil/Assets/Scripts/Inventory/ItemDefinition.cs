using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Game Item")]
public class ItemDefinition : ScriptableObject
{
    public Item.Rarity rarity;

    public GameObject dropPrefab;

    public Sprite inventorySprite;

    public string displayName;

    public string description;
}
