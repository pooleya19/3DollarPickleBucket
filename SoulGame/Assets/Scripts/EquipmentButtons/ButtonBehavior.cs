using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject soulsMenu;
    public GameObject equipmentMenu;

    bool equipping = false;
    bool inventoring = false;
    bool unequipping = false;

    private void OnEnable() {
        Debug.Log("Wakey Wakey");
        equipping = false;
        inventoring = false;
        unequipping = false;
    }

    public void Inventory() {
        Debug.Log("Inventory");
    }

    public void Equip() {
        if (equipping)
        {
            soulsMenu.gameObject.SetActive(false);
            equipmentMenu.gameObject.SetActive(false);
            equipping = false;
        }
        else if (!equipping && !unequipping && !inventoring)
        {
            soulsMenu.gameObject.SetActive(true);
            equipmentMenu.gameObject.SetActive(true);
            equipping = true;
        }
    }

    public void Unequip() {
        Debug.Log("Equip");
    }
}
