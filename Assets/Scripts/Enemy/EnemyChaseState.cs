using System.Collections.Generic;

public class EnemyChaseState : EnemyStateBase
{
    public EnemyChaseState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Chase;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time)
    {
        EnemyStateBase.Action action = null;
        EnemyStateBase.ActionArg arg;

        if (enemy.Target == null)
        {
            return new EnemyWaitState();
        }

        if (actions.TryGetValue("Move", out action) == true)
        {
            arg.pos = enemy.Target.transform.position;
            action(ref arg);
        }

        return this;
    }
}
