using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezeState : EnemyStateBase
{
    float timer = 0.0f;

    public EnemyFreezeState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Freeze;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time,
                                           Animator anim)
    {
        const float WaitTime = 2.0f;

        anim.SetInteger("State", (int)EnemyStateBase.Kind.Freeze);

        timer += delta_time;
        if (timer > WaitTime)
        {
            timer = 0.0f;
            return new EnemyChaseState();
        }
        return this;
    }
}
