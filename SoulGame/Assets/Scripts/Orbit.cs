using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float xRadius = 1.0f;
    public float yRadius = 0.5f;
    public float frequency = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(xRadius * Mathf.Cos(frequency * Time.time), yRadius * Mathf.Sin(frequency * Time.time), 0);
    }
}
