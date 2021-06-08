using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public ClickableSlot[] clickableSlots;
    public InvSlot[] unclickableSlots;
    public Dictionary<InvSlot, int> slotIndices = new Dictionary<InvSlot, int>();

    public int selected;

    public static InventoryMouseIcon mouse;
    public static ClickableSlot rlickStart;
    public static InvSlot mouseStored = new InvSlot();

    public bool hotbar = false;

    [SerializeField] private GameObject inventoryTab;

    public int mask = 0;
    public bool opened = false;

    public static void SetOpen(GameObject caller, bool open, int mask)
    {
        Inventory[] inventories = caller.GetComponentsInChildren<Inventory>(true);
        for (int i = 0; i < inventories.Length; i++)
        {
            Inventory inv = inventories[i];
            if (inv.hotbar) continue;
            if (inv.mask == mask || mask < 0)
            {
                inv.opened = open;
                inv.inventoryTab.SetActive(inv.opened);
            }
        }
    }

    public static void ToggleOpen(GameObject caller, int mask)
    {
        Inventory[] inventories = caller.GetComponentsInChildren<Inventory>(true);
        for (int i = 0; i < inventories.Length; i++)
        {
            Inventory inv = inventories[i];
            if (inv.hotbar) continue;
            if (inv.mask == mask || mask < 0)
            {
                inv.opened = !inv.opened;
                inv.inventoryTab.SetActive(inv.opened);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < unclickableSlots.Length; i++)
        {
            InvSlot slot = unclickableSlots[i];
            slotIndices.Add(slot, i);
            slot.SetInventory(this);
        }
        for (var i = 0; i < clickableSlots.Length; i++)
        {
            ClickableSlot slot = clickableSlots[i];
            slotIndices.Add(slot.invslot, i + unclickableSlots.Length);
            slot.invslot.SetInventory(this);
            slot.id = i;
            slot.hotbar = hotbar;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (mouse != null)
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(mouse.display.rectTransform, Input.mousePosition, Camera.current, out globalMousePos))
            {
                mouse.position = globalMousePos;
            }
            mouse.definition = mouseStored.contained.prop.def;
            mouse.empty = mouseStored.contained.IsEmpty();
            mouse.amount = mouseStored.contained.count;
        }
    }
}
