using System.Collections.Generic;

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

    public override EnemyStateBase Update(Enemy enemy, Dictionary<string, EnemyStateBase.Action> actions, float delta_time)
    {
        const float WaitTime = 5.0f;

        timer += delta_time;
        if (timer > WaitTime)
        {
            return new EnemyWaitState();
        }

        return this;
    }
}
