using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    //ダメージを各部位に設定する

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

    // もしすり抜けた際の保険
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.Damage(damage);
        }
    }

    // もしすり抜けた際の保険
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.Damage(damage);
        }
    }
}
