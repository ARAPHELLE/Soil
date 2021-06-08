using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemProperties 
{ 
    public ItemDefinition def;
    public bool renamed;
    public string customname;

    public ItemProperties(ItemDefinition indef)
    {
        renamed = false;
        customname = "";
        def = indef;
    }
}


[System.Serializable]
public struct Item
{
    public static Item empty = new Item(null, 0);

    public enum Rarity { Crap, Common, Uncommon, Rare, Amazing, Legendary}

    public ItemProperties prop;

    public int count;

    public bool IsEmpty()
    {
        return GetHashCode() == Item.empty.GetHashCode();
    }

    public bool Combine(Item combineWith, out Item result)
    {
        if(combineWith.prop.Equals(prop))
        {
            result = new Item(this, count + combineWith.count);
            return true;
        }
        result = Item.empty;
        return false;
    }

    public static Item Drop(Item contained)
    {
        return empty;
    }

    public Item(ItemProperties inprop, int incount)
    {
        prop = inprop;
        count = incount;
        if (count <= 0) this = empty;
    }

    public Item(Item tocopy)
    {
        prop = tocopy.prop;
        count = tocopy.count;
        if (count <= 0) this = empty;
    }
    public Item(Item tocopy, int amount)
    {
        prop = tocopy.prop;
        count = amount;
        if (count <= 0) this = empty;
    }

    public Item(ItemDefinition definition, int amount)
    {
        prop = new ItemProperties(definition);
        count = amount;
        if (count <= 0) this = empty;
    }
}
