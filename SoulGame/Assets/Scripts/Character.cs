using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float runSpeed = 5.0f;
    public GameObject pickupable;
    Rigidbody2D body;
    float horizontal;
    float vertical;

    public DashState dash_state = DashState.Ready;
    public float dashSpeed = 3.0f;
    public Vector2 preDashVelocity;
    private float dashTime;
    public float startDashTime = 0.2f;
    public float coolDown = 0.0f;
    public bool hasCoolDown = true;
    public GameObject dash_effect;

    public GameObject fire_ball;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //fire_ball = (GameObject)Resources.Load("Assets/Prefabs/Fireball.prefab");
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        else if (coolDown <= 0 && dash_state == DashState.Cooldown)
        {
            coolDown = 0.0f;
            dash_state = DashState.Ready;
        }

        if (!hasCoolDown && Input.GetKey(KeyCode.LeftShift))
        {
            Instantiate(dash_effect, transform.position, Quaternion.identity);
            preDashVelocity = body.velocity;
            dash_state = DashState.Dashing;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && body.velocity != Vector2.zero)
        {
            if (dash_state == DashState.Ready)
            {
                Instantiate(dash_effect, transform.position, Quaternion.identity);
                preDashVelocity = body.velocity;
                dash_state = DashState.Dashing;
            }
        }

        if (dash_state == DashState.Dashing)
        {
            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                coolDown = 0.3f;
                dash_state = DashState.Cooldown;
            }
            else
            {
                dashTime -= Time.deltaTime;

                body.velocity = preDashVelocity * dashSpeed;
            }
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pickupable.TryGetComponent<ItemObject>(out ItemObject item);
            if ((transform.position - item.transform.position).sqrMagnitude < 4.0f)
            {
                item.OnHandlePickupItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Instantiate(fire_ball, transform.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fire_ball, transform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1))
        {
            GameObject projectile = Instantiate(fire_ball, transform.position, Quaternion.identity);
            projectile.transform.localScale = new Vector3(3, 3, 1);
        }
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}
