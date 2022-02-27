using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EquipmentAdder : MonoBehaviour
{
    public InventorySystem currInventorySystem;
    public EquipmentManager currEquipManager;
    public List<TextMeshProUGUI> fields;
    bool selecting = false;
    int inventoryPosition;

    private void OnEnable() {

        List<InventoryItem> inventoryItems = currInventorySystem.GetInventory();
        int currInventorySlot = 0;

        foreach (InventoryItem temp in inventoryItems)
        {
            if (temp.data.IsEquipment())
            {
                fields[currInventorySlot].text = temp.data.displayName;
                ++currInventorySlot;
            }
        }
    }

    public void ClickSelect(int index)
    {
        inventoryPosition = index;
        selecting = true;
    }

    public void ClickEquipment(int equip)
    {
        if (selecting) 
        { 
            //Get the current item
            List<InventoryItem> inventoryItems = currInventorySystem.GetInventory();
            InventoryItemData currItem = inventoryItems[inventoryPosition].data;

            //Add it to equipment
            currEquipManager.Equip((Equipment) currItem, equip);

            //Remove it from items
            currInventorySystem.Remove(currItem);
        }
        else
        {
            //Get current item

            //remove it from equipment

            //add it to items
        }
    }
}
