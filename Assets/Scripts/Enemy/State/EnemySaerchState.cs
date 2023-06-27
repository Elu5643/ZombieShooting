using System.Collections.Generic;
using UnityEngine;

public class EnemySaerchState : EnemyStateBase
{
    float timer = 0.0f;

    public EnemySaerchState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Saerch;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time,
                                          Animator anim)
    {
        const float WaitTime = 5.0f;

        anim.SetInteger("State", (int)EnemyStateBase.Kind.Saerch);

        timer += delta_time;
        if (timer > WaitTime)
        {
            return new EnemyWaitState();
        }
        else if (enemy.IsFound())
        {
            return new EnemyChaseState();
        }

        return this;
    }
}
