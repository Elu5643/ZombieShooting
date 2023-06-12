using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    //�_���[�W���e���ʂɐݒ肷��

    Enemy enemy = null;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.Damage(damage);
        }
    }

    // �������蔲�����ۂ̕ی�
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.Damage(damage);
        }
    }

    // �������蔲�����ۂ̕ی�
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.Damage(damage);
        }
    }
}
