using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    public int numOfSouls = 4;
    private int numEquippedSouls = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    // #Singleton
    public Equipment[] currentEquipment;
    public delegate void OnEquipmentChangedCallback();
    public OnEquipmentChangedCallback onEquipmentChangedCallback;

    private void Start()
    {
        currentEquipment = new Equipment[numOfSouls];
    }

    public void Equip(Equipment newItem)
    {
        int currLength = currentEquipment.Length;
        if (currLength >= numOfSouls)
        {
            //Application.Quit();
            Debug.Log("Maximum number of souls has been reached");
        }
        else
        { //FIXME: Verify
            currentEquipment[currLength] = newItem;

            StatusManager.instance.UpdateCharacterStatus(newItem, true);

            onEquipmentChangedCallback.Invoke();
            numEquippedSouls++;
        }
    }

    public void Equip(Equipment newItem, int index)
    {
        Debug.Log("Equipping");
        int currLength = currentEquipment.Length;
        if (currentEquipment[index] != null)
        {
            //Application.Quit();
            Debug.Log("There is an item in this slot");
        }
        else
        { //FIXME: Verify
            Debug.Log("adding equipment to index: " + index);
            currentEquipment[index] = newItem;

            StatusManager.instance.UpdateCharacterStatus(newItem, true);

            //onEquipmentChangedCallback.Invoke();
            numEquippedSouls++;
        }
    }

    public void Unequip(Equipment newItem)
    {
        int currLength = currentEquipment.Length;
        if (currLength == 0)
        {
            //Application.Quit();
            Debug.Log("You have no soul :(");
        }
        else
        { //FIXME: Verify
            currentEquipment[currLength] = null;

            StatusManager.instance.UpdateCharacterStatus(newItem, false);

            //onEquipmentChangedCallback.Invoke();
            numEquippedSouls--;
        }
    }

    public void Unequip(Equipment newItem, int index)
    {
        Debug.Log("2.5");
        int currLength = currentEquipment.Length;
        if (currentEquipment[index] == null)
        {
            //Application.Quit();
            Debug.Log("You have no soul :(");
        }
        else
        { //FIXME: Verify
            currentEquipment[index] = null;

            StatusManager.instance.UpdateCharacterStatus(newItem, false);

            //onEquipmentChangedCallback.Invoke();
            numEquippedSouls--;
        }
    }

    public int getNumEquippedSouls()
    {
        return numEquippedSouls;
    }
}
