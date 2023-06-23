using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase
{
    public enum Kind
    {
        Wait,   // ~‚Ü‚é
        Saerch, // õ“G
        Chase,  // Œ©‚Â‚¯‚½
        Attack, // UŒ‚
        Freeze, // ’â~
    }

    public struct ActionArg
    {
        public Vector3 pos;
    }

    public delegate void Action(ref ActionArg arg);

    public abstract EnemyStateBase Update(Enemy obj, Dictionary<string, EnemyStateBase.Action> actions, float delta_time);

    public abstract Kind GetKind();
}
