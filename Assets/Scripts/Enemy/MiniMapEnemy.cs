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
        // Œü‚¢‚Ä‚¢‚é•ûŒüŽæ“¾
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);

        if(enemy.IsDestroy) 
        {
            Destroy(gameObject);
        }
    }
}
