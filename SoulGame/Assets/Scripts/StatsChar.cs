using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObject/Health", order = 1)]
public class StatsChar : ScriptableObject
{
    public string charName = "Steve";
    public int baseMaxHP = 69;
    public int baseATK = 69;
    public int baseDEF = 69;
    public int baseSPD = 69;
    public int baseLuck = 69;

    public int maxHP = 79;
    public int ATK = 79;
    public int DEF = 79;
    public int SPD = 79;
    public int Luck = 79;
    public int HP = 79;

    //Maybe not
    /*
    public List<TextMeshProUGUI> fields;

    void UpdateFields()
    {
        Type fieldsType = typeof(StatsChar);
 
        foreach (TextMeshProUGUI field in fields)
        {
            string value = fieldsType.GetField(field.name).GetValue(StatusManager.instance.playerStatus).ToString();
            Debug.Log("field type" + field.name);
            Debug.Log("field " + field.name);
            Debug.Log("value: " + value);
            field.text = value;       
        }
    }
    */
}
