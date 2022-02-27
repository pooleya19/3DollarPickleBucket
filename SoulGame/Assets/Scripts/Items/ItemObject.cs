using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickupItem()
    {
        InventorySystem.current.Add(referenceItem); //FIXME: there was a current after system before add
        Destroy(gameObject);
    }
}
