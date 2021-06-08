using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemIcon : MonoBehaviour
{
    public ItemDefinition definition;
    public Image display;
    public bool empty = true;
    public Vector2 startPosition;
    public Vector2 position;

    public Text amountBack;
    public Text amountFront;

    public int amount;

    public void Start()
    {
        startPosition = display.rectTransform.position;
        position = startPosition;
    }

    public void Update()
    {
        display.enabled = !empty;
        if (definition != null)
        {
            display.sprite = definition.inventorySprite;
            amountBack.text = amount.ToString();
            amountFront.text = amount.ToString();
        }
        if(definition == null || empty)
        {
            amountBack.text = "";
            amountFront.text = "";
        }
    }
}
