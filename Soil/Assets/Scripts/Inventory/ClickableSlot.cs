using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickableSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InvSlot invslot = new InvSlot();
    public int id;
    public bool takeFrom = true;
    public bool hotbar = false;

    public InventoryItemIcon icon;

    public void Update()
    {
        icon.definition = invslot.contained.prop.def;
        icon.empty = invslot.contained.IsEmpty();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
                if (takeFrom || (hotbar && PlayerController.inventoryOpen))
                {
                    if (Inventory.mouseStored.contained.IsEmpty())
                    {
                        Inventory.mouseStored.contained = invslot.TakeHalf();
                    }
                    else if (invslot.contained.IsEmpty())
                    {
                        invslot.SwapItems(Inventory.mouseStored);
                    }
                }
                break;

            case PointerEventData.InputButton.Left:
                if (takeFrom || (hotbar && PlayerController.inventoryOpen))
                {
                    invslot.SwapItems(Inventory.mouseStored);
                }
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (takeFrom || (hotbar && PlayerController.inventoryOpen))
                {
                    invslot.SwapItems(Inventory.mouseStored);
                }
                break;

            case PointerEventData.InputButton.Middle:
                Item.Drop(invslot.contained);
                invslot.contained = Item.empty;
                break;

        }
    }
}
