using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject rayPos = null;
    [SerializeField] GameObject target = null;
    [SerializeField] GameObject bulletObj = null;

    [SerializeField] AudioClip walkSE = null;
    [SerializeField] AudioClip destroySE = null;

    RaycastHit hit;

    Dictionary<string, EnemyStateBase.Action> actions = null;
    EnemyStateBase currentState = null;
    NavMeshAgent nav = null;
    Animator anim = null;
    AudioSource audioSource = null;

    public GameObject Target { get => target; }

    int hitPoint = 100;    //�@Enemy��HitPoint
    bool isMoving = true;    //�@Enemy�̓������~�߂�i�U�������������j

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentState = new EnemyWaitState();

        actions = new Dictionary<string, EnemyStateBase.Action>();
        actions["Move"] = Move;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Update(this, actions, Time.deltaTime);

        // ���G�͈͂̔��� ���@�A�^�b�N�ƃt���[�Y�̃A�j���[�V�������͓����Ă͂����Ȃ�
        if(IsFound() && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false && 
           anim.GetCurrentAnimatorStateInfo(0).IsName("Freeze") == false)
        {
            // ���G�͈͂̒��ōU���͈͂ɓ�������U��
            if(Vector3.Distance(transform.position, target.transform.position) <= 1.1f) 
            {
                ChangeState(new EnemyAttackState(), (int)EnemyStateBase.Kind.Attack);
            }
            else
            {
                ChangeState(new EnemyChaseState(), (int)EnemyStateBase.Kind.Chase);
            }
        }
        else
        {
            if (currentState.GetKind() == EnemyStateBase.Kind.Chase)
            {
                ChangeState(new EnemyWaitState(), (int)EnemyStateBase.Kind.Wait);
            }
        }
    }

    // �_���[�W����炤
    void Damage(int damage)
    {
        hitPoint -= damage;
        anim.SetTrigger("Damage");
        nav.SetDestination(transform.position);
        isMoving = false;
    }

    // ���S
    void Destroy()
    {
        audioSource.Stop();
        anim.SetTrigger("Destroy");
        nav.SetDestination(transform.position);
        //audioSource.PlayOneShot(destroySE);
        Instantiate(bulletObj, transform.position, Quaternion.identity);
        isMoving = false;
    }

    // �e���ʂ̃_���[�W�̈����Ŏ����Ă���
    public void EachPartsDamage(int damage)
    {
        if (hitPoint < 1)
        {
            if (isMoving)
            {
                Destroy();
            }
        }
        else
        {
            Damage(damage);
        }
    }

    void Move(ref EnemyStateBase.ActionArg arg)
    {
        if (isMoving)
        {
            nav.destination = arg.pos;

            if (!audioSource.isPlaying)
            {
                //audioSource.PlayOneShot(walkSE);
            }
        }
    }

    void ChangeState(EnemyStateBase newState, int animState)
    {
        currentState = newState;
        anim.SetInteger("State", animState);
    }

    bool IsFound()
    {
        Vector3 playerDirection = Target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, playerDirection);
        Vector3 direction = playerDirection.normalized;

        if (angle < 100)
        {
            if (Physics.Raycast(rayPos.transform.position, direction, out hit))
            {
                if (hit.transform.gameObject == Target)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // �A�j���[�V�����̃C�x���g�Ŏg�p
    void FreezeEvent()
    {
        ChangeState(new EnemyFreezeState(), (int)EnemyStateBase.Kind.Freeze);
    }

    // �A�j���[�V�����̃C�x���g�Ŏg�p
    void MoveEvent()
    {
        isMoving = true;
        ChangeState(new EnemyChaseState(), (int)EnemyStateBase.Kind.Chase);
    }
}
