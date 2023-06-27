using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase
{
    public enum Kind
    {
        Wait,   // �~�܂�
        Saerch, // ���G
        Chase,  // ������
        Attack, // �U��
        Freeze, // ��~
    }

    // Arg => ����
    public struct ActionArg
    {
        public Vector3 pos;
    }

    public delegate void Action(ref ActionArg arg);

    public abstract EnemyStateBase Update(Enemy obj, Dictionary<string, EnemyStateBase.Action> actions, float delta_time,
                                          Animator anim);

    public abstract Kind GetKind();
}
