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
    public Character currCharacter;

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
                soulFields[currentEquipSlot].text = temp.displayName;
                ++currentEquipSlot;
            }
        }
    }

    public void ClickSelect(int index)
    {
        Debug.Log("You've clicked on an inventory Item");
        Debug.Log("Selected: " + index);
        inventoryPosition = index;
        selecting = true;
    }

    public void ClickEquipment(int equip)
    {
        Debug.Log("starting selecting is: " + selecting);
        if (selecting) 
        { 
            Debug.Log("Equipping to: " + equip);
            selecting = false;
            //Get the current item
            List<InventoryItem> inventoryItems = currInventorySystem.GetInventory();
            InventoryItemData currItem = inventoryItems[inventoryPosition].data;

            //Add it to equipment
            //FIXME: implement a system where it replaces the soul thats currently at that slot
            currEquipManager.Equip((Equipment) currItem, equip);

            //Remove it from items
            currInventorySystem.Remove(currItem);

            //Make sure to display all changes
            inventoryItems = currInventorySystem.GetInventory();
            int currInventorySlot = 0;

            foreach (InventoryItem temp in inventoryItems)
            {
                if (temp.data.IsEquipment())
                {
                    fields[currInventorySlot].text = temp.data.displayName;
                    ++currInventorySlot;
                }
            }
            while (currInventorySlot < 8)
            {
                fields[currInventorySlot].text = "Empty";
                ++currInventorySlot;
            }

            Equipment[] equippedItems = currEquipManager.currentEquipment;
            int currentEquipSlot = 0;

            while (currentEquipSlot < 4)
            {
                Equipment temp = equippedItems[currentEquipSlot];
                if(equippedItems[currentEquipSlot] != null)
                {
                    soulFields[currentEquipSlot].text = temp.displayName;
                    ++currentEquipSlot;
                }
                else
                {
                    soulFields[currentEquipSlot].text = "Empty";
                    ++currentEquipSlot;
                }
            }

            currCharacter.UpdateCharacterStatus();
            Debug.Log("ending selecting is: " + selecting);
            Debug.Log("Finished Moving");
        }
        else
        {
            Debug.Log("Unequipping from: " + equip);
            //Get current item
            
            Equipment[] equippedItems = currEquipManager.currentEquipment;
            List<InventoryItem> inventoryItems = currInventorySystem.GetInventory();
            Equipment currItem = equippedItems[equip];
            
            //remove it from equipment
            currEquipManager.Unequip(currItem, equip);
            
            //add it to items
            currInventorySystem.Add(currItem);

            //Update Stats/Inventory window
            inventoryItems = currInventorySystem.GetInventory();
            int currInventorySlot = 0;
            
            foreach (InventoryItem temp in inventoryItems)
            {
                if (temp.data.IsEquipment())
                {
                    fields[currInventorySlot].text = temp.data.displayName;
                    ++currInventorySlot;
                }
            }
            while (currInventorySlot < 8)
            {
                fields[currInventorySlot].text = "Empty";
                ++currInventorySlot;
            }

            equippedItems = currEquipManager.currentEquipment;
            int currentEquipSlot = 0;

            while (currentEquipSlot < 4)
            {
                Equipment temp = equippedItems[currentEquipSlot];
                if(equippedItems[currentEquipSlot] != null)
                {
                    soulFields[currentEquipSlot].text = temp.displayName;
                    ++currentEquipSlot;
                }
                else
                {
                    soulFields[currentEquipSlot].text = "Empty";
                    ++currentEquipSlot;
                }
            }

            currCharacter.UpdateCharacterStatus();
            Debug.Log("ending selecting is: " + selecting);
            Debug.Log("Finished Moving");
        }
    }
}
