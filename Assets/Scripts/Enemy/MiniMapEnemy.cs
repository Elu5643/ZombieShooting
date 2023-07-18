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
        // �����Ă�������擾
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);

        if(enemy.IsDestroy) 
        {
            Destroy(gameObject);
        }
    }
}
