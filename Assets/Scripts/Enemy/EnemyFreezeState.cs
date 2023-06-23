using System.Collections.Generic;

public class EnemyFreezeState : EnemyStateBase
{
    public EnemyFreezeState()
    {

    }

    public override EnemyStateBase.Kind GetKind()
    {
        return Kind.Freeze;
    }

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time)
    {
        return this;
    }
}
