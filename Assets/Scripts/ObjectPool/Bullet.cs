using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Generator.DeleteEvent deleteEvent = null;

    GameObject generator = null;
    public GameObject Generator
    {
        set
        {
            generator = value;
        }
    }

    Rigidbody rb = null;

    float timer = 0.0f;     // オブジェクトが消える時間を計る

    float shotSpeed = 2000f;    // 弾を飛ばすスピード

    float dispersion = 0.02f; // ばらつき具合
    float verticalToHorizontalRatio = 1.5f; // ばらつきの縦横比
    float angle = 0.05f; // 発射角

    // 弾を打つ際の初期化
    public void Initialize(Vector3 pos, Vector3 forward, bool isMove, Transform playerPos, Vector3 hitPos)
    {
        timer = 0.0f;
        transform.position = pos;
        transform.forward = forward;

        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);

        BulletDisparity(forward, isMove, playerPos, hitPos);    // 弾のばらけ具合
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }

    void BulletDisparity(Vector3 forward, bool isMove, Transform playerPos, Vector3 hitPos)
    {
        if (isMove == false)
        {
            Vector3 direction = hitPos - transform.position;
            rb.AddForce(direction.normalized * shotSpeed, ForceMode.Acceleration);
        }
        else
        {
            Vector3 direction;

            Func<Vector3, Vector3, float, Vector3> disparityVector = (Vector3 playerForward, Vector3 pos, float verticalHorizontal) =>
            {
                return Vector3.Slerp(playerForward, pos, verticalHorizontal);
            };

            // 縦のばらつき
            float vertical = UnityEngine.Random.Range(-dispersion * verticalToHorizontalRatio - angle, dispersion * verticalToHorizontalRatio + angle);
            if (vertical >= 0)
            {
                direction = disparityVector(forward, playerPos.up, vertical);
            }
            else
            {
                direction = disparityVector(forward, -playerPos.up, -vertical);
            }

            // 横のばらつき
            float horizontal = UnityEngine.Random.Range(-dispersion, dispersion);
            if (horizontal >= 0)
            {
                direction = disparityVector(direction, playerPos.right, horizontal);
            }
            else
            {
                direction = disparityVector(direction, -playerPos.right, -horizontal);
            }

            rb.AddForce(direction.normalized * shotSpeed, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }

    // Rigidbodyの設定でContinuous Dynamicにしているけどすり抜けた際の保険
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }

    // Rigidbodyの設定でContinuous Dynamicにしているけどすり抜けた際の保険
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }
}
