using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    //�_���[�W���e���ʂɐݒ肷��

    private Enemy enemy;

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
}
