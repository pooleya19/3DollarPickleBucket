using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    public int numOfSouls = 4;
 
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
        if(currLength >= numOfSouls)
        {
            Application.Quit();
        }
        else
        { //FIXME: Verify
            currentEquipment[currLength] = newItem;
 
            StatusManager.instance.UpdateCharacterStatus(newItem, true);
 
            onEquipmentChangedCallback.Invoke();    
        }
    }

    public void Unequip(Equipment newItem)
    {
        int currLength = currentEquipment.Length;
        if(currLength == 0)
        {
            Application.Quit();
        }
        else
        { //FIXME: Verify
            currentEquipment[currLength] = newItem;
 
            StatusManager.instance.UpdateCharacterStatus(newItem, false);
 
            onEquipmentChangedCallback.Invoke();    
        }
    }

}
