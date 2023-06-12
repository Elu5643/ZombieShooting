using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Animator anim = null;

    [SerializeField] Player player = null;
    [SerializeField] Shooter shooter = null;

    enum State
    {
        Stay,   //止まる
        Walk,   //歩く
        Aim,    //照準
        Reload  //リロード
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 銃が動くアニメーション
        if (Input.GetKey(KeyCode.W) && anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") == false)
        {
            anim.SetInteger("State", (int)State.Walk);
            //  歩きうちの為
            if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible)
            {
                anim.SetInteger("State", (int)State.Stay);
            }
        }
        // 動きが止まった際
        else
        {
            anim.SetInteger("State", (int)State.Stay);
        }

        // エイムアニメーション
        if (player.IsAim() && anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") == false)
        {
            anim.SetInteger("State", (int)State.Aim);
        }


        // リロードアニメーション
        if (shooter.Reload())
        {
            anim.SetInteger("State", (int)State.Reload);
        }
    }

    public bool IsNotShot()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
