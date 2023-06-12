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
        Stay,   //�~�܂�
        Walk,   //����
        Aim,    //�Ə�
        Reload  //�����[�h
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �e�������A�j���[�V����
        if (Input.GetKey(KeyCode.W) && anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") == false)
        {
            anim.SetInteger("State", (int)State.Walk);
            //  ���������̈�
            if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible)
            {
                anim.SetInteger("State", (int)State.Stay);
            }
        }
        // �������~�܂�����
        else
        {
            anim.SetInteger("State", (int)State.Stay);
        }

        // �G�C���A�j���[�V����
        if (player.IsAim() && anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") == false)
        {
            anim.SetInteger("State", (int)State.Aim);
        }


        // �����[�h�A�j���[�V����
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
