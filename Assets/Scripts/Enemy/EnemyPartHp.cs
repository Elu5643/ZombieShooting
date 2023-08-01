using UnityEngine;

public class EnemyPartHp : MonoBehaviour
{
    [SerializeField] int damage;    // ダメージを各部位に設定する

    Enemy enemy = null;
    Transform player = null;
    CapsuleCollider myCollider = null;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        myCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float colliderDistance = 20;

        if (distance < colliderDistance) 
        {
            myCollider.enabled = true;
        }
        else
        {
            myCollider.enabled = false;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.EachPartsDamage(damage);
        }
    }
}
