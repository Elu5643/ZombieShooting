using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyStateBase
{
    public EnemyChaseState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Chase;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time,
                                          Animator anim)
    {
        EnemyStateBase.Action action = null;
        EnemyStateBase.ActionArg arg;

        anim.SetInteger("State", (int)EnemyStateBase.Kind.Chase);

        if (actions.TryGetValue("Move", out action) == true)
        {
            arg.pos = enemy.Target.transform.position;
            action(ref arg);

            float attackRange = 1.1f;
            if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) <= attackRange)
            {
                return new EnemyAttackState();
            }
        }

        // “G‚ðŒ©Ž¸‚Á‚½ê‡
        if(enemy.IsFound() == false)
        {
            return new EnemyWaitState();
        }

        return this;
    }
}
