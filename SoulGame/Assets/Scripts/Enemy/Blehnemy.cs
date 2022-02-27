using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blehnemy : EnemyBehavior
{
    public float highAlertTimer = 4.0f;
    public float chargeTimer = 3.0f;
    
    public float rangeAttackPlayer = 4.0f;
    public float rangeFindPlayer = 2.0f;
    public float rangeLosePlayer = 4.0f;

    public Vector2 idleFidgetDelayRange = new Vector2(1, 8);
    public Vector2 idleFidgetDistanceRange = new Vector2(0, 2);
    public float minFidgetThreshold = 0.1f;

    float lastIdleFidgetTime = 0;
    float idleFidgetDelay = 0;
    Vector2 targetPosition;

    SpriteRenderer spriteRenderer;

    public float HP;
    public float ATK;
    public float MVSP;
    public float ATKSP;
    
    public float radius = 4.0f;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask blockMask;
    
    public bool canSeePlayer;

    public GameObject fire_ball;

    public override void action_START() {
        Debug.Log("transform.forward");
        StartCoroutine(FOVRoutine());

        spriteRenderer = GetComponent<SpriteRenderer>();

        HPRange = new Vector2(5, 15);
        ATKRange = new Vector2(3, 8);
        MVSPRange = new Vector2(1, 3);
        ATKSPRange = new Vector2(0.2f, 0.4f);

        HP = Random.Range(HPRange.x, HPRange.y);
        ATK = Random.Range(ATKRange.x, HPRange.y);
        MVSP = Random.Range(MVSPRange.x, MVSPRange.y);
        ATKSP = Random.Range(ATKSPRange.x, ATKSPRange.y);

        targetPosition = spawnPoint;
    }

    public override EnemyState getState() {
        if (highAlertTimer > 0) {
            highAlertTimer -= Time.deltaTime;
        }

        if (chargeTimer > 0) {
            chargeTimer -= Time.deltaTime;
        }

        //Debug.Log(state);
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(position, playerPosition);
        Debug.Log(distance <= rangeAttackPlayer);
        Debug.Log(distance <= rangeAttackPlayer && state == EnemyState.PURSUE);
        if (distance <= rangeAttackPlayer && state == EnemyState.PURSUE)
        {
            chargeTimer = 3.0f;
            spriteRenderer.color = Color.HSVToRGB(0, 0.7f, 0.0f);

            return EnemyState.CHARGE;
        }
        else if (state == EnemyState.CHARGE && chargeTimer > 0) {            
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            return EnemyState.CHARGE;
        }
        else if (state == EnemyState.CHARGE && chargeTimer <= 0)
        {
            return EnemyState.ATTACK;
        }
        else if (canSeePlayer || (state == EnemyState.HIGH_ALERT && distance <= radius))
        {
            return EnemyState.PURSUE;
        }
        else if (distance <= rangeLosePlayer && state == EnemyState.PURSUE)
        {
            return EnemyState.PURSUE;
        }
        else if (state == EnemyState.PURSUE)
        {
            action_LOSTPLAYER();
            return EnemyState.HIGH_ALERT;
        }
        else if (state == EnemyState.HIGH_ALERT)
        {
            if (highAlertTimer <= 0) {
                Debug.Log("High alert done");
                
                radius = 4.0f;
                spriteRenderer.color = new Color(0, 1, 0, 1);

                return EnemyState.IDLE;
            }

            return EnemyState.HIGH_ALERT;
        }
        else
        {
            return EnemyState.IDLE;
        }
    }

    public override void action_IDLE()
    {
        Vector2 position = new Vector2(playerTransform.position.x, playerTransform.position.y);
        float currentTime = Time.time;
        if (currentTime - lastIdleFidgetTime > idleFidgetDelay)
        {
            //Debug.Log("New Fidget");
            lastIdleFidgetTime = currentTime;
            idleFidgetDelay = Random.Range(idleFidgetDelayRange.x, idleFidgetDelayRange.y);
            Vector2 nextFidgetDirection = Random.insideUnitCircle;
            nextFidgetDirection.Normalize();
            targetPosition = spawnPoint + nextFidgetDirection * Random.Range(idleFidgetDistanceRange.x, idleFidgetDistanceRange.y);
        }
        spriteRenderer.color = new Color(0, 1, 0, 1);
        //Debug.Log("IDLE");
        moveToTargetPosition();
    }

    public override void action_PURSUE()
    {
        spriteRenderer.color = Color.HSVToRGB(0, 0.7f, 0.67f);
        //Debug.Log("PURSUE");
        targetPosition = playerTransform.position;
        moveToTargetPosition();
    }

    public override void action_LOSTPLAYER()
    {
        highAlertTimer = 4.0f;
        radius = 5.0f;
        
        spawnPoint = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    public override void action_ATTACK()
    {
        highAlertTimer = 4.0f;
        radius = 5.0f;
        state = EnemyState.HIGH_ALERT;
        
        spriteRenderer.color = Color.HSVToRGB(0, 1.0f, 0.67f);
        //Debug.Log("ATTACK");
    }

    void moveToTargetPosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 toTargetPosition = targetPosition - position;
        if (toTargetPosition.magnitude > minFidgetThreshold)
        {
            toTargetPosition.Normalize();
            //GetComponent<Rigidbody2D>().transform.rotation = Quaternion.LookRotation(toTargetPosition);
            GetComponent<Rigidbody2D>().velocity = toTargetPosition * MVSP;
            transform.up = GetComponent<Rigidbody2D>().velocity;
            Debug.DrawLine(position, targetPosition, Color.cyan);
            //Debug.Log(position.ToString() + ", " + targetPosition.ToString());
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Debug.Log("Happy");
        }
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeChecks.Length > 0) {
            Transform target = rangeChecks[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2) {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, blockMask)) {
                    canSeePlayer = true;
                } else {
                    canSeePlayer = false;
                }
            } else {
                canSeePlayer = false;
            }
        } else if (canSeePlayer) {
            canSeePlayer = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle/2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle/2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (canSeePlayer) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
