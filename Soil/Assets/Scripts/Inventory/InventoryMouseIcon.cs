﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryMouseIcon : MonoBehaviour
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
        Inventory.mouse = this;

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
        else
        {
            amountBack.text = "";
            amountFront.text = "";
        }
        display.rectTransform.position = new Vector3(position.x, position.y, -1);
    }
}
