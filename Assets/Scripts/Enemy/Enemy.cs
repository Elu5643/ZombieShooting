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

    [SerializeField] int hitPoint = 100;    //�@Enemy��HitPoint

    float timer;        //�@�J�ڂ��鎞�Ԃ��v��
    float attackTimer;  //�@�U���̃N�[���^�C��

    float randomPos = 10;   //�@�ړ����铮���������_���ɂ���

    bool isMoving = true;    //�@Enemy�̓������~�߂�i�U�������������j

    public enum State
    {
        Wait,   //�@�~�܂�
        Saerch, //�@���G
        Chase,  //�@������
        Freeze, //�@��~
    }

    public enum AnimState
    {
        Wait,   //�@�~�܂�
        Saerch, //�@���G
        Chase,  //�@������
        Attack, //�@�U��
        Freeze, //�@��~
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

    // �_���[�W��������
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
        audioSource.PlayOneShot(destroySE);
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

    // �v���C���[�𔭌�������
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

    // �~�܂�
    void Wait()
    {
        nav.SetDestination(transform.position);

        timer += Time.deltaTime;
        if(timer >= 5)
        {
            timer = 0;
            currentState = State.Saerch;
            anim.SetInteger("State", (int)AnimState.Saerch);

            // ���G���\�b�h�ɓ������Ƃ��Ɉړ�����͈͂��w��
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


    // ���G
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


    // ������
    void Chase()
    {
        nav.destination = target.transform.position;
        // �����������͎~�܂�
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

    // �v���C���[����U�����������ۂ͒�~
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

    // �A�j���[�V�����̃C�x���g�Ŏg�p
    void FreezeEvent()
    {
        nav.SetDestination(transform.position);
        currentState = State.Freeze;
        anim.SetInteger("State", (int)AnimState.Freeze);
    }

    // �A�j���[�V�����̃C�x���g�Ŏg�p
    void MoveEvent()
    {
        isMoving = true;
        currentState = State.Chase;
        anim.SetInteger("State", (int)AnimState.Chase);
    }
}
