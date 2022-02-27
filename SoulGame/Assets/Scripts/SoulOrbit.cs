using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOrbit : MonoBehaviour
{
    public GameObject gameManager;
    EquipmentManager equipmentManager;
    InventorySystem inventorySystem;
    int numEquipmentSlots;
    public float xRadius = 0.5f;
    public float yRadius = 0.3f;
    public float frequency = 0.5f;
    public float scale = 0.4f;

    GameObject empty;
    List<GameObject> orbitingSouls = new List<GameObject>();
    int numOrbitingSouls = 0;

    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = gameManager.GetComponent<EquipmentManager>();
        inventorySystem = gameManager.GetComponent<InventorySystem>();
        empty = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        numEquipmentSlots = equipmentManager.currentEquipment.Length;
        int numEquippedSouls = inventorySystem.inventory.Count; //equipmentManager.getNumEquippedSouls();
        Debug.Log("Num: " + numEquippedSouls);
        if (numOrbitingSouls != numEquippedSouls)
        {
            Debug.Log("Fixing");
            //Update orbitting souls
            for (int i = 0; i < orbitingSouls.Count; i++) Destroy(orbitingSouls[i]);
            orbitingSouls.Clear();

            for (int i = 0; i < numEquippedSouls; i++)
            {
                InventoryItemData equipment = inventorySystem.inventory[i].data;
                GameObject soul = Instantiate(empty, Vector3.zero, Quaternion.identity, transform);
                soul.transform.localScale = new Vector3(scale, scale, scale);
                SpriteRenderer spriteRenderer = soul.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = equipment.icon;
                spriteRenderer.color = equipment.color;
                orbitingSouls.Add(soul);
            }
        }
        Debug.Log("Equipped: " + numEquippedSouls);
        for (int i = 0; i < numEquippedSouls; i++)
        {
            GameObject soul = orbitingSouls[i];
            float angle = frequency * Time.time + 2 * Mathf.PI / numEquippedSouls * i;
            soul.transform.localPosition = new Vector3(xRadius * Mathf.Cos(angle), yRadius * Mathf.Sin(angle), 0);
        }
    }
}
