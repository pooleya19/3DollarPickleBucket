using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOrbit : MonoBehaviour
{
    public GameObject gameManager;
    EquipmentManager equipmentManager;
    int numEquipmentSlots;
    public float xRadius = 0.5f;
    public float yRadius = 0.3f;
    public float frequency = 0.5f;

    GameObject empty;
    List<GameObject> orbitingSouls = new List<GameObject>();
    int numOrbitingSouls = 0;

    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = gameManager.GetComponent<EquipmentManager>();
        empty = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        numEquipmentSlots = equipmentManager.currentEquipment.Length;
        int numEquippedSouls = equipmentManager.getNumEquippedSouls();
        if (numOrbitingSouls != numEquippedSouls)
        {
            //Update orbitting souls
            for (int i = 0; i < orbitingSouls.Capacity; i++) Destroy(orbitingSouls[i]);
            orbitingSouls.Clear();

            for (int i = 0; i < numEquipmentSlots; i++)
            {
                Equipment equipment = equipmentManager.currentEquipment[i];
                GameObject soul = Instantiate(empty, Vector3.zero, Quaternion.identity);
                SpriteRenderer spriteRenderer = soul.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = equipment.icon;
                spriteRenderer.color = equipment.color;
                orbitingSouls.Add(soul);
            }
        }

        for (int i = 0; i < numEquippedSouls; i++)
        {
            GameObject soul = orbitingSouls[i];
            float angle = frequency * Time.time + 2 * Mathf.PI / numEquippedSouls;
            soul.transform.localPosition = new Vector3(xRadius * Mathf.Cos(angle), yRadius * Mathf.Sin(angle), 0);
        }
    }
}
