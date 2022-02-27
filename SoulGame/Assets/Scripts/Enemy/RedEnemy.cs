using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : EnemyBehavior
{
    public float rangeAttackPlayer = 1.0f;
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

    public override void action_START()
    {
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

    public override EnemyState getState()
    {
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(position, playerPosition);

        if (distance <= rangeAttackPlayer)
        {
            return EnemyState.ATTACK;
        }
        else if (distance <= rangeFindPlayer)
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
            return EnemyState.IDLE;
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
        spriteRenderer.color = Color.HSVToRGB(0, 0.4f, 0.67f);
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
        spawnPoint = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    public override void action_ATTACK()
    {
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
}
