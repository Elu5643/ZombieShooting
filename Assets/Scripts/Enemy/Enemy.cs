using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    [SerializeField] GameObject rayPos = null;
    [SerializeField] GameObject BulletObj = null;

    [SerializeField] AudioClip walkSe = null;
    [SerializeField] AudioClip destroySe = null;

    NavMeshAgent nav = null;
    Animator anim = null;
    AudioSource audioSource = null;

    RaycastHit hit;

    [SerializeField] int hitPoint = 100;    //Enemy‚ÌHitPoint

    float timer;        //‘JˆÚ‚·‚éŠÔ‚ğŒv‚é
    float attackTimer;  //UŒ‚‚ÌƒN[ƒ‹ƒ^ƒCƒ€

    float randomPos = 10;   //ˆÚ“®‚·‚é“®‚«‚ğƒ‰ƒ“ƒ_ƒ€‚É‚·‚é

    bool isMoving = true;    //Enemy‚Ì“®‚«‚ğ~‚ß‚éiUŒ‚‚ğ‹ò‚ç‚Á‚½j

    public enum State
    {
        Wait,   //~‚Ü‚é
        Saerch, //õ“G
        Cahse,  //Œ©‚Â‚¯‚½
        Freeze, //’â~
    }

    public enum AnimState
    {
        Wait,   //~‚Ü‚é
        Saerch, //õ“G
        Cahse,  //Œ©‚Â‚¯‚½
        Attack, //UŒ‚
        Freeze, //’â~
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

                case State.Cahse:
                    Cahse();
                    break;

                case State.Freeze:
                    Freeze();
                    break;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSe);
            }
        }
    }

    void Damage()
    {
        anim.SetTrigger("Damage");
        nav.SetDestination(transform.position);
        isMoving = false;
    }

    void Destroy()
    {
        audioSource.Stop();
        anim.SetTrigger("Destroy");
        nav.SetDestination(transform.position);
        audioSource.PlayOneShot(destroySe);
        Instantiate(BulletObj, transform.position, Quaternion.identity);
        isMoving = false;
    }

    public void Damage(int damage)
    {
        if (hitPoint <= 1)
        { 
            if (isMoving)
            {
                Destroy();
            }
        }
        else
        {
            Damage();
            hitPoint -= damage;
        }
    }

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

    void Wait()
    {
        nav.SetDestination(transform.position);

        timer += Time.deltaTime;
        if(timer >= 5)
        {
            timer = 0;
            currentState = State.Saerch;
            anim.SetInteger("State", (int)AnimState.Saerch);

            Vector3 newPos = this.transform.position;
            Vector3 offSet = new Vector3(Random.Range(-randomPos, randomPos), 0, Random.Range(-randomPos, randomPos));
            newPos += offSet;
            nav.destination = newPos;
        }
        else if (IsFound())
        {
            timer = 0;
            currentState = State.Cahse;
            anim.SetInteger("State", (int)AnimState.Cahse);
        }
    }

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
            currentState = State.Cahse;
            anim.SetInteger("State", (int)AnimState.Cahse);
        }
    }

    void Cahse()
    {
        nav.destination = target.transform.position;
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

    void Freeze()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 2)
        {
            attackTimer = 0;
            currentState = State.Cahse;
            anim.SetInteger("State", (int)AnimState.Cahse);
        }
    }

    void FreezeEvent()
    {
        nav.SetDestination(transform.position);
        currentState = State.Freeze;
        anim.SetInteger("State", (int)AnimState.Freeze);
    }
    void MoveEvent()
    {
        isMoving = true;
        currentState = State.Cahse;
        anim.SetInteger("State", (int)AnimState.Cahse);
    }
}
