using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour //FIXME: this needs to be an extension of Item
{
    public int equipSlot; //These will eventually be the 4 souls equipped at any time

    public int armorModifier;
    public int damageModifier;

    public InventoryItemData data;// {get; private set;}
    public int stackSize;// {get; private set;}

    public Equipment(InventoryItemData source)
    {
        data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
