using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Animator anim;

    [SerializeField] Player player = null;
    //[SerializeField] Shooter shooter = null;
    [SerializeField] BulletController bulletController = null;

    enum State
    {
        Stay,   //é~Ç‹ÇÈ
        Walk,   //ï‡Ç≠
        Aim,    //è∆èÄ
        Reload  //ÉäÉçÅ[Éh
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", (int)State.Walk);
            if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible)
            {
                anim.SetInteger("State", (int)State.Stay);
            }
        }
        else
        {
            anim.SetInteger("State", (int)State.Stay);
        }

        if (player.IsAim())
        {
            anim.SetInteger("State", (int)State.Aim);
        }


        if (bulletController.MainNum < 30 && bulletController.SubNum > 0 &&
           Input.GetKeyDown(KeyCode.R) && !Cursor.visible)
        {
            anim.SetInteger("State", (int)State.Reload);
            bulletController.ReloadBullet();
        }
    }

    public bool IsReload()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
