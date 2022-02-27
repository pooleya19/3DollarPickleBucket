using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : EnemyBehavior
{
    public float rangeClose = 2.0f;
    public Vector2 HPRange = new Vector2(5, 15);

    public override void action_START()
    {
        HPRange = new Vector2(5, 15);
        ATKRange = new Vector2(3, 8);
        MVSPRange = new Vector2(1, 3);
        ATKSPRange = new Vector2(0.2f, 0.4f);
        //Range = 2.0f;
    }

    public override EnemyState getState()
    {
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);


        return EnemyState.IDLE;
    }

    public override void action_IDLE()
    {

    }

    public override void action_PURSUE()
    {

    }

    public override void action_ATTACK()
    {

    }
}
