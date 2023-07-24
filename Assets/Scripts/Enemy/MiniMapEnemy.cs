using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapEnemy : MonoBehaviour
{
    Enemy enemy = null;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        // 向いている方向取得
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);

        if(enemy.HitPoint < 1) 
        {
            gameObject.SetActive(false);
        }
    }
}
