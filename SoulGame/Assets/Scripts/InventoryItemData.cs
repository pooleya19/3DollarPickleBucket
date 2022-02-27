using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory Item Data", order = 1)]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    
    public void Use()
    {
        //this may be virtual
        // this function is supposed to be overriden
    }
 
    public void Drop()
    {
        //may be virtual
        //InventorySystem.instance.RemoveItem(this); //InventorySystem was Inventory
    }
}
