using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EquipmentAdder : MonoBehaviour
{
    public InventorySystem currInventorySystem;
    public List<TextMeshProUGUI> fields;
    bool selecting = false;

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
        int invPos = index;
        selecting = true;
    }

    public void ClickEquipment(int equip)
    {
        if (selecting) 
        {
            
        }
        else
        {

        }
    }
}
