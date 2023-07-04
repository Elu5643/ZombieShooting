using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyStateBase;

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

    public GameObject Target { get { return target; } }

    int hitPoint = 100;    //　EnemyのHitPoint
    bool isMoving = true;    //　Enemyの動きを止める（攻撃を喰らった時）

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
        currentState = currentState.Update(this, actions, Time.deltaTime, anim);
    }

    void ChangeState(EnemyStateBase newState)
    {
        currentState = newState;
    }

    // ダメージを喰らう
    void Damage(int damage)
    {
        hitPoint -= damage;
        anim.SetTrigger("Damage");
        nav.SetDestination(transform.position);
        isMoving = false;
    }

    // 死亡
    void Destroy()
    {
        audioSource.Stop();
        anim.SetTrigger("Destroy");
        nav.SetDestination(transform.position);
        audioSource.PlayOneShot(destroySE);
        Instantiate(bulletObj, transform.position, Quaternion.identity);
        isMoving = false;
    }

    // 各部位のダメージの引数で持ってくる
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
                audioSource.PlayOneShot(walkSE);
            }
        }
    }

    // Playerを見つけたか？
    public bool IsFound()
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

    // アニメーションのイベントで使用
    void FreezeEvent()
    {
        ChangeState(new EnemyFreezeState());
    }

    // アニメーションのイベントで使用
    void MoveEvent()
    {
        isMoving = true;
        ChangeState(new EnemyChaseState());
    }
}
