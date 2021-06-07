using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemIcon : MonoBehaviour, IPointerUpHandler
{
    public bool dragging;
    public ItemDefinition definition;
    public Image display;
    public bool empty = true;
    public Vector3 startpos;

    public void Start()
    {
        startpos = display.rectTransform.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    public void Update()
    {
        display.enabled = !empty;
        if (definition != null)
        {
            display.sprite = definition.inventorySprite;
            if (dragging)
            {
                display.rectTransform.localPosition = (Input.mousePosition - startpos);
            }
            else
            {
                display.rectTransform.localPosition = startpos;
            }
        }
    }
}
