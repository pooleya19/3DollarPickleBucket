using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
 
public class Equipment : InventoryItemData
{
    public int atkModifier;
    public int defModifier;
    public int hpModifier;
    public int spdModifier;

    override public bool IsEquipment()
    {
        return true;
    }
    /*
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        Inventory.instance.RemoveItem(this);
    }
    */
}
