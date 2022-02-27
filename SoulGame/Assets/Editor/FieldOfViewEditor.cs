using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Blehnemy))]

public class FieldOfViewEditor : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("yes");
        Blehnemy fov = (Blehnemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
