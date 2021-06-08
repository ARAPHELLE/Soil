using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickableSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public InvSlot invslot = new InvSlot();
    public int id;
    public bool takeFrom = true;
    public bool hotbar = false;

    public bool held;

    public InventoryItemIcon icon;

    public void Update()
    {
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        icon.definition = invslot.contained.prop.def;
        icon.empty = invslot.contained.IsEmpty();
        icon.amount = invslot.contained.count;

        Inventory.mouse.definition = Inventory.mouseStored.contained.prop.def;
        Inventory.mouse.empty = Inventory.mouseStored.contained.IsEmpty();
        Inventory.mouse.amount = Inventory.mouseStored.contained.count;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Middle:
                Item.Drop(invslot.contained);
                invslot.contained = Item.empty;
                UpdateIcon();
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (invslot.contained.IsEmpty() || eventData.button == PointerEventData.InputButton.Middle)
        {
            eventData.pointerDrag = null;
            held = false;
            return;
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {   
            if (Inventory.mouseStored.contained.IsEmpty() || Inventory.mouseStored.contained.prop.Equals(invslot.contained.prop))
            {
                Inventory.mouseStored.contained = invslot.TakeHalf(Inventory.mouseStored.contained.count);
            }
            UpdateIcon();
            held = false;
            return;
        }
        held = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        invslot.contained = Item.Drop(invslot.contained);
        Inventory.mouseStored.contained = Item.Drop(Inventory.mouseStored.contained);
        held = false;
        UpdateIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!held) return;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(icon.display.rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            icon.position = globalMousePos;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        held = false;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InvSlot slot = Inventory.mouseStored;
            if (slot != null)
            {
                ClickableSlot orig = eventData.pointerDrag.GetComponent<ClickableSlot>();
                InvSlot origslot = orig.invslot;
                invslot.SwapItems(slot);
                UpdateIcon();
                orig.invslot = origslot;
                orig.UpdateIcon();
            }
            return;
        }
        if (eventData.pointerDrag != null)
        {
            ClickableSlot slot = eventData.pointerDrag.GetComponent<ClickableSlot>();
            if (slot != null)
            {
                if (slot == this)
                {
                    icon.position = icon.startPosition;
                    UpdateIcon();
                }
                else
                {
                    invslot.SwapItems(slot.invslot);
                    icon.position = icon.startPosition;
                    UpdateIcon();
                }
            }
        }
    }
}
