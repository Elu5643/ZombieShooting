using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    [SerializeField] GameObject rayPos = null;
    [SerializeField] GameObject bulletObj = null;

    [SerializeField] AudioClip walkSE = null;
    [SerializeField] AudioClip destroySE = null;

    NavMeshAgent nav = null;
    Animator anim = null;
    AudioSource audioSource = null;

    RaycastHit hit;

    [SerializeField] int hitPoint = 100;    //　EnemyのHitPoint

    float timer;        //　遷移する時間を計る
    float attackTimer;  //　攻撃のクールタイム

    float randomPos = 10;   //　移動する動きをランダムにする

    bool isMoving = true;    //　Enemyの動きを止める（攻撃を喰らった時）

    public enum State
    {
        Wait,   //　止まる
        Saerch, //　索敵
        Chase,  //　見つけた
        Freeze, //　停止
    }

    public enum AnimState
    {
        Wait,   //　止まる
        Saerch, //　索敵
        Chase,  //　見つけた
        Attack, //　攻撃
        Freeze, //　停止
    }

    State currentState = State.Wait;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            switch (currentState)
            {
                case State.Wait:
                    Wait();
                    break;

                case State.Saerch:
                    Saerch();
                    break;

                case State.Chase:
                    Chase();
                    break;

                case State.Freeze:
                    Freeze();
                    break;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSE);
            }
        }
    }

    // ダメージを喰らった
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

    // プレイヤーを発見したか
    bool IsFound()
    {
        Vector3 playerDirection = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, playerDirection);
        Vector3 direction = playerDirection.normalized;

        if (angle < 100)
        {
            if (Physics.Raycast(rayPos.transform.position, direction, out hit))
            {
                if (hit.transform.gameObject == target)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // 止まる
    void Wait()
    {
        nav.SetDestination(transform.position);

        timer += Time.deltaTime;
        if(timer >= 5)
        {
            timer = 0;
            currentState = State.Saerch;
            anim.SetInteger("State", (int)AnimState.Saerch);

            // 索敵メソッドに入ったときに移動する範囲を指定
            Vector3 newPos = transform.position;
            Vector3 offSet = new Vector3(Random.Range(-randomPos, randomPos), 0, Random.Range(-randomPos, randomPos));
            newPos += offSet;
            nav.destination = newPos;
        }
        else if (IsFound())
        {
            timer = 0;
            currentState = State.Chase;
            anim.SetInteger("State", (int)AnimState.Chase);
        }
    }


    // 索敵
    void Saerch()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            timer = 0;
            currentState = State.Wait;
            anim.SetInteger("State", (int)AnimState.Wait);
        }
        else if(IsFound())
        {
            timer = 0;
            currentState = State.Chase;
            anim.SetInteger("State", (int)AnimState.Chase);
        }
    }


    // 見つけた
    void Chase()
    {
        nav.destination = target.transform.position;
        // 見失った時は止まる
        if(!IsFound())
        {
            currentState = State.Wait;
            anim.SetInteger("State", (int)AnimState.Wait);
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= 1.1f)
        {
            nav.SetDestination(transform.position);
            anim.SetInteger("State", (int)AnimState.Attack);
        }
    }

    // プレイヤーから攻撃を喰らった際は停止
    void Freeze()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 2)
        {
            attackTimer = 0;
            currentState = State.Chase;
            anim.SetInteger("State", (int)AnimState.Chase);
        }
    }

    // アニメーションのイベントで使用
    void FreezeEvent()
    {
        nav.SetDestination(transform.position);
        currentState = State.Freeze;
        anim.SetInteger("State", (int)AnimState.Freeze);
    }

    // アニメーションのイベントで使用
    void MoveEvent()
    {
        isMoving = true;
        currentState = State.Chase;
        anim.SetInteger("State", (int)AnimState.Chase);
    }
}
