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
    public List<TextMeshProUGUI> soulFields;
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

        Equipment[] equippedItems = currEquipManager.currentEquipment;
        int currentEquipSlot = 0;

        foreach (Equipment temp in equippedItems)
        {
            if(equippedItems[currentEquipSlot] != null)
            {
                fields[currentEquipSlot].text = temp.displayName;
                ++currentEquipSlot;
            }
        }
    }

    public void ClickSelect(int index)
    {
        Debug.Log("Selected: " + index);
        inventoryPosition = index;
        selecting = true;
    }

    public void ClickEquipment(int equip)
    {
        Debug.Log("Equipping to: " + equip);
        if (selecting) 
        { 
            //Get the current item
            List<InventoryItem> inventoryItems = currInventorySystem.GetInventory();
            InventoryItemData currItem = inventoryItems[inventoryPosition].data;

            //Add it to equipment
            currEquipManager.Equip((Equipment) currItem, equip);

            //Remove it from items
            currInventorySystem.Remove(currItem);

            //Make sure to display all changes
        }
        else
        {
            //Get current item

            //remove it from equipment

            //add it to items
        }
    }
}
