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
        Wait,   // �~�܂�
        Move,   // ����
        Aim,    // �Ə�
        Run,    // ����
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �����[�h�A�j���[�V����
        if (shooter.Reload())
        {
            anim.SetTrigger("Reload");
        }
        // �G�C���A�j���[�V����
        else if (player.IsAim())
        {
            anim.SetInteger("State", (int)State.Aim);
        }
        // �������~�܂�����
        else if (Input.GetKey(KeyCode.W) == false)
        {
            anim.SetInteger("State", (int)State.Wait);
        }
        // W�L�[���������ۏe�𓮂���
        else if (Input.GetKey(KeyCode.W) || player.IsStop)
        {
            //  ���������̈�
            if (Input.GetKey(KeyCode.Mouse0) && !Cursor.visible)
            {
                anim.SetInteger("State", (int)State.Wait);
            }
            else if(Input.GetKey(KeyCode.LeftShift) && player.Stm.value > 0)
            {
                anim.SetInteger("State", (int)State.Run);
            }
            else
            {
                anim.SetInteger("State", (int)State.Move);
            }
        }
    }

    public bool IsNotShot()
    {
        // �A�j���[�V�������Đ������̃^�C�~���O�ł͒e��ł��Ȃ��悤�ɂ���
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
