using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public Vector2 HPRange;
    public Vector2 ATKRange;
    public Vector2 MVSPRange;
    public Vector2 ATKSPRange;

    protected Vector2 spawnPoint;

    protected GameObject player;
    protected Transform playerTransform;

    protected EnemyState state;

    public enum EnemyState
    {
        IDLE, PURSUE, ATTACK
    }

    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player");
        action_START();
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = player.transform;
        state = getState();
        switch (state)
        {
            case (EnemyState.IDLE):
                action_IDLE();
                break;
            case (EnemyState.PURSUE):
                action_PURSUE();
                break;
            case (EnemyState.ATTACK):
                action_ATTACK();
                break;
        }
    }

    public abstract void action_START();
    public abstract EnemyState getState();
    public abstract void action_IDLE();
    public abstract void action_PURSUE();
    public abstract void action_ATTACK();
}
