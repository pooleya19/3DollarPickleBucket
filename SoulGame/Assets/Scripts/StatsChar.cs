using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObject/Health", order = 1)]
public class StatsChar : ScriptableObject
{
    public string charName = "Steve";
    public float baseMaxHP = 69;
    public float baseATK = 69;
    public float baseDEF = 69;
    public float baseSPD = 69;
    public float baseLuck = 69;

    public float maxHP = 79;
    public float ATK = 79;
    public float DEF = 79;
    public float SPD = 79;
    public float Luck = 79;
    public float HP = 79;

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
