using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StaticManagerController : MonoBehaviour
{
    public List<TextMeshProUGUI> fields;

    private void Awake()
    {
        StatusManager.instance.onStatusChangedCallback += UpdateFields;
    }

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
}
