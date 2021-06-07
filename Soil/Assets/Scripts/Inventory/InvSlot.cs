using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvSlot
{
    public Item contained = Item.empty;

    private Inventory owning_inventory;

    public Inventory Owner { get => owning_inventory; private set => owning_inventory = value; }

    public InvSlot(Item item, Inventory inv)
    {
        owning_inventory = inv;
        contained = item;
    }

    public InvSlot(Item item)
    {
        contained = item;
    }

    public InvSlot() {}

    public bool SetInventory(Inventory inv)
    {
        if (owning_inventory != null)
        {
            owning_inventory = inv;
            return true;
        }
        return false;
    }

    public bool SwapItems(InvSlot toswap)
    {
        Item temp = toswap.contained;

        bool equal = temp.prop.Equals(contained.prop);

        if (equal)
        {
            contained = new Item(contained, contained.count + temp.count);
            toswap.contained = Item.empty;
        }
        else
        {
            toswap.contained = contained;
            contained = temp;
        }

        return equal;
    }

    public Item TakeHalf()
    {
        if (!contained.IsEmpty())
        {
            Item newContained = new Item(contained, Mathf.CeilToInt(contained.count / 2));
            Item newItem = new Item(contained, Mathf.FloorToInt(contained.count / 2));

            contained = newContained;

            return newItem;
        }
        return Item.empty;
    }

    public bool Put(Item toput)
    {
        if(contained.IsEmpty())
        {
            contained = toput;
            return true;
        }
        else
        {
            if(contained.Combine(toput,out Item res))
            {
                contained = res;
                return true;
            }
        }

        return false;
    }
}
