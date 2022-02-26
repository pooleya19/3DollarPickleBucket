using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float runSpeed = 2.0f;
    public GameObject pickupable;
    public GameObject nonEquipment;
    Rigidbody2D body;
    float horizontal;
    float vertical;
    bool itemNotFound = true;
    bool item2NotFound = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (itemNotFound)
            {
                pickupable.TryGetComponent<ItemObject>(out ItemObject item);
                if((transform.position - item.transform.position).sqrMagnitude < 4.0f)
                {
                    item.OnHandlePickupItem();
                    //itemController.SetTargetPosition(item.transform);
                    //itemController.Pickup();
                    itemNotFound = false;
                }
            }
            if (item2NotFound)
            {
                nonEquipment.TryGetComponent<ItemObject>(out ItemObject item2);
                if((transform.position - item2.transform.position).sqrMagnitude < 4.0f)
                {
                    item2.OnHandlePickupItem();
                    //itemController.SetTargetPosition(item.transform);
                    //itemController.Pickup();
                    item2NotFound = false;
                }
            }
        }
    }
}
