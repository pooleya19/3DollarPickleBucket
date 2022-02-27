using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blehnemy : EnemyBehavior
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask blockMask;
    
    public bool canSeePlayer;

    public override void action_START() {
        Debug.Log("transform.forward");
        StartCoroutine(FOVRoutine());
    }

    public override EnemyState getState() {
        return EnemyState.IDLE;
    }

    public override void action_IDLE() {
        if (canSeePlayer) {
            Debug.Log("I can see you");
        }
    }

    public override void action_PURSUE() {

    }

    public override void action_ATTACK() {

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

            // //Debug.Log("hell");
            // Debug.Log(transform.forward);
            // Debug.Log(directionToTarget);
            // Debug.Log(Vector2.Angle(transform.forward, directionToTarget));

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
