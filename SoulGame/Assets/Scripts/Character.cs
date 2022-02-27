using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public GameObject fire_ball;
    public GameObject statsMenu;
    public GameObject optionMenu;
    public StatsChar currPlayerStatus;
    public List<TextMeshProUGUI> fields;
    public List<GameObject> nonStartupLists;

    AudioSource audioData;

    public LayerMask targetMask;
    public LayerMask blockMask;

    public float radius = 4.0f;
    [Range(0, 360)]
    public float angle;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Stats Each Run
        currPlayerStatus.charName = "Steve";
        currPlayerStatus.baseMaxHP = 100;
        currPlayerStatus.baseATK = 10;
        currPlayerStatus.baseDEF = 10;
        currPlayerStatus.baseSPD = 10;
        currPlayerStatus.baseLuck = 10;

        currPlayerStatus.maxHP = 10;
        currPlayerStatus.ATK = 10;
        currPlayerStatus.DEF = 10;
        currPlayerStatus.SPD = 10;
        currPlayerStatus.Luck = 10;
        currPlayerStatus.HP = 100;


        statsMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(false);
        foreach (GameObject currList in nonStartupLists) {
            currList.gameObject.SetActive(false);
        }
        menuIsOn = false;
        // statsMenu.gameObject.SetActive(false);
        // optionMenu.gameObject.SetActive(false);
        // foreach (GameObject currList in nonStartupLists) {
        //     currList.gameObject.SetActive(false);
        // }
        // menuIsOn = false;

        audioData = GetComponent<AudioSource>();
        //audioData.Play(0);

        body = GetComponent<Rigidbody2D>();
        //fire_ball = (GameObject)Resources.Load("Assets/Prefabs/Fireball.prefab");
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Debug.Log(currPlayerStatus.HP);
        
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

        if (Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Attack");

            Collider2D[] enemiesHit = FieldOfViewCheck();

            Debug.Log(enemiesHit.Length);
            foreach (Collider2D i in enemiesHit) {
                Debug.Log(i);

                Destroy(i.gameObject);
            }
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
                optionMenu.gameObject.SetActive(false);
                    foreach (GameObject currList in nonStartupLists) {
                        currList.gameObject.SetActive(false);
                    }
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
                optionMenu.gameObject.SetActive(true);
                menuIsOn = true;
            }
        }
    }

    public void UpdateCharacterStatus()
    {
        Type fieldsType = typeof(StatsChar);

        foreach (TextMeshProUGUI field in fields)
        {
            string value = fieldsType.GetField(field.name).GetValue(StatusManager.instance.playerStatus).ToString();
            field.text = value;       
        }
    }

    private Collider2D[] FieldOfViewCheck() {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        List<Collider2D> enemiesHit = new List<Collider2D>();

        Debug.Log(rangeChecks.Length);

        for (int i = 0; i < rangeChecks.Length; i++) {
            Transform target = rangeChecks[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2) {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, blockMask)) {
                    enemiesHit.Add(rangeChecks[i]);
                }
            }
        }

        return enemiesHit.ToArray();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        audioData.Play(0);

        if (col.gameObject.CompareTag("Enemy")) {
            Debug.Log("Hit Enemy");

            GameObject hitEnemy = col.gameObject;
            EnemyBehavior enemyData = hitEnemy.GetComponent<EnemyBehavior>();
            
            receiveDamage(enemyData.ATK);
        }
    }
    
    private void receiveDamage(float damage) {
        currPlayerStatus.HP -= damage;

        healthBar.fillAmount = Mathf.Clamp(currPlayerStatus.HP / currPlayerStatus.baseMaxHP, 0, 1f);

        if (currPlayerStatus.HP <= 0)
        {
            Destroy(gameObject);
        }
    }
/*
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle/2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle/2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        // if (canSeePlayer) {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawLine(transform.position, playerTransform.position);
        // }
    }
*/
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
