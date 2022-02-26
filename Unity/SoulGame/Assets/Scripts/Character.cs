using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float runSpeed = 2.0f;
    public GameObject pickupable;
    Rigidbody2D body;
    float horizontal;
    float vertical;

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
            pickupable.TryGetComponent<ItemObject>(out ItemObject item);
            if((transform.position - item.transform.position).sqrMagnitude < 25.0f)
            {
                item.OnHandlePickupItem();
                //itemController.SetTargetPosition(item.transform);
                //itemController.Pickup();
            }
        }
    }
}
