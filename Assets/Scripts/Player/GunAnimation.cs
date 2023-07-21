using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    Animator anim = null;

    [SerializeField] Player player = null;
    [SerializeField] Shooter shooter = null;

    enum State
    {
        Wait,   // 止まる
        Move,   // 歩く
        Aim,    // 照準
        Reload, // リロード
        Run,    // 走る
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // リロードアニメーション
        if (shooter.Reload())
        {
            anim.SetInteger("State", (int)State.Reload);
        }
        // エイムアニメーション
        else if (player.IsAim())
        {
            anim.SetInteger("State", (int)State.Aim);
        }
        // 動きが止まった際
        else if (Input.GetKey(KeyCode.W) == false)
        {
            anim.SetInteger("State", (int)State.Wait);
        }
        // Wキーを押した際銃を動かす
        else if (Input.GetKey(KeyCode.W) || player.IsStop)
        {
            anim.SetInteger("State", (int)State.Move);
            //  歩きうちの為
            if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible)
            {
                anim.SetInteger("State", (int)State.Wait);
            }
            else if(Input.GetKey(KeyCode.LeftShift) && player.Stm.value > 0)
            {
                anim.SetInteger("State", (int)State.Run);
            }
        }

    }

    public bool IsNotShot()
    {
        // アニメーションが再生時このタイミングでは弾を打たないようにする
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
