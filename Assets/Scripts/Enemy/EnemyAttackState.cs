using System.Collections.Generic;

public class EnemyAttackState : EnemyStateBase
{
    public EnemyAttackState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Attack;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time)
    {
        EnemyStateBase.Action action = null;
        EnemyStateBase.ActionArg arg;

        if (actions.TryGetValue("Move", out action) == true)
        {
            arg.pos = enemy.transform.position;
            action(ref arg);
        }

        return this;
    }
}
