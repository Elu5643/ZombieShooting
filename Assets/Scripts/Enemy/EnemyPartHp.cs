using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    // �_���[�W���e���ʂɐݒ肷��

    Enemy enemy = null;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.EachPartsDamage(damage);
        }
    }
}
