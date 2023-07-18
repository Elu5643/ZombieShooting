using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    // ダメージを各部位に設定する

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
