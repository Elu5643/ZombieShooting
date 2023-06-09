using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitState : EnemyStateBase
{
    float timer = 0.0f;

    public EnemyWaitState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Wait;
    }


    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time,
                                          Animator anim)
    {
        const float WaitTime = 5.0f;

        EnemyStateBase.Action action = null;
        EnemyStateBase.ActionArg arg;
        float randomPos = 10;   //　移動する動きをランダムにする

        anim.SetInteger("State", (int)EnemyStateBase.Kind.Wait);

        if (actions.TryGetValue("Move", out action) == true)
        {
            arg.pos = enemy.transform.position;
            action(ref arg);
        }

        timer += delta_time;
        if (timer > WaitTime)
        {
            Vector3 newPos = enemy.transform.position;
            Vector3 offSet = new Vector3(Random.Range(-randomPos, randomPos), 0, Random.Range(-randomPos, randomPos));
            newPos += offSet;

            if (actions.TryGetValue("Move", out action) == true)
            {
                arg.pos = newPos;
                action(ref arg);
            }

            return new EnemySaerchState();
        }
        else if (enemy.IsFound())
        {
            return new EnemyChaseState();
        }

        return this;
    }

}
