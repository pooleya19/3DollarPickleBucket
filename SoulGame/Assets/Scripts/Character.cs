using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Character : MonoBehaviour
{

    public float runSpeed = 5.5f;
    //public GameObject pickupable;
    public LayerMask inventoryItemMask;
    public float inventoryItemPickupRange = 1.0f;
    Rigidbody2D body;
    float horizontal;
    float vertical;
    bool menuIsOn = true;

    public DashState dash_state = DashState.Ready;
    public float dashSpeed = 2.5f;
    public Vector2 preDashVelocity;
    private float dashTime;
    public float startDashTime = 0.2f;
    public float coolDown = 0.0f;
    public bool hasCoolDown = true;
    public GameObject dash_effect;

    public GameObject fire_ball;
    public GameObject statsMenu;
    public GameObject soulsMenu;
    public StatsChar currPlayerStatus;
    public List<TextMeshProUGUI> fields;

    // Start is called before the first frame update
    void Start()
    {
        statsMenu.gameObject.SetActive(false);
        soulsMenu.gameObject.SetActive(false);
        menuIsOn = false;
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
            body.velocity = new Vector2(horizontal, vertical).normalized * runSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, inventoryItemPickupRange, inventoryItemMask);
            if (rangeChecks.Length > 0)
            {
                //There is at least 1 item to pick up
                Transform closestTransform = rangeChecks[0].transform;
                float closestDistance = -1;
                //Find closest item
                for (int i = 0; i < rangeChecks.Length; i++)
                {
                    Transform target = rangeChecks[i].transform;
                    Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
                    Vector2 position = new Vector2(transform.position.x, transform.position.y);
                    Vector2 toTarget = (targetPosition - position);
                    float distance = toTarget.magnitude;
                    if (closestDistance == -1)
                    {
                        closestTransform = target;
                        closestDistance = distance;
                    }
                    else if (distance < closestDistance)
                    {
                        closestTransform = target;
                        closestDistance = distance;
                    }
                }
                if (closestDistance != -1)
                {
                    closestTransform.TryGetComponent<ItemObject>(out ItemObject item);
                    item.OnHandlePickupItem();
                }
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
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("i was pressed");
            if(menuIsOn)
            {
                //Turn every single menu off
                statsMenu.gameObject.SetActive(false);
                soulsMenu.gameObject.SetActive(false);
                menuIsOn = false;
            }
            else if (!menuIsOn)
            {
                Type fieldsType = typeof(StatsChar);

                foreach (TextMeshProUGUI field in fields)
                {
                    string value = fieldsType.GetField(field.name).GetValue(StatusManager.instance.playerStatus).ToString();
                    //Debug.Log("field type" + field.name);
                    //Debug.Log("field " + field.name);
                    //Debug.Log("value: " + value);
                    field.text = value;       
                }
                //Only these two menus turn on
                statsMenu.gameObject.SetActive(true);
                soulsMenu.gameObject.SetActive(true);
                menuIsOn = true;
            }
        }
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}
