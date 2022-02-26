using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public StatsChar playerStatus;
 
    #region Singleton
    public static StatusManager instance;
 
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
 
    #endregion
 
    public void UpdateCharacterStatus(Equipment newItem, bool equipping)
    {
        if (equipping)
        {
            playerStatus.maxHP = playerStatus.maxHP + newItem.hpModifier;
            playerStatus.DEF = playerStatus.DEF + newItem.defModifier;
            playerStatus.ATK = playerStatus.ATK + newItem.atkModifier;
            playerStatus.SPD = playerStatus.SPD + newItem.spdModifier;
        }
        else
        {
            playerStatus.maxHP -= newItem.hpModifier;
            playerStatus.DEF -= newItem.defModifier;
            playerStatus.ATK -= newItem.atkModifier;
            playerStatus.SPD -= newItem.spdModifier;
        }
    }
}
