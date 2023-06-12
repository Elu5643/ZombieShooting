using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Generator.DeleteEvent deleteEvent = null;

    GameObject generator = null;

    Rigidbody rb = null;

    float timer = 0.0f;     //オブジェクトが消える時間を計る

    float shotSpeed = 2000f;    //弾を飛ばすスピード

    Vector3 direction;

    float dispersion = 0.02f; // ばらつき具合
    float verticalToHorizontalRatio = 1.5f; // ばらつきの縦横比
    float angle = 0.05f; // 発射角

    public GameObject Generator
    {
        set
        {
            generator = value;
        }
    }

    public void Initialize(Vector3 pos, Vector3 forward, bool isAim, bool isMove, Transform playerPos, Vector3 hitPos)
    {
        timer = 0.0f;
        transform.position = pos;
        transform.forward = forward;

        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);

        if (isAim)
        {
            BulletDisparity(forward, isMove, playerPos, hitPos);    //弾のばらけ具合
        }
        else
        {
            BulletDisparity(forward, isMove, playerPos, hitPos);    //弾のばらけ具合
        }
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
            direction = hitPos - transform.position;
            rb.AddForce(direction.normalized * shotSpeed, ForceMode.Acceleration);
        }
        else
        {
            Vector3 dir;

            // 縦のばらつき
            float v = Random.Range(-dispersion * verticalToHorizontalRatio - angle, dispersion * verticalToHorizontalRatio + angle);
            if (v >= 0)
            {
                dir = Vector3.Slerp(forward, playerPos.up, v);
            }
            else
            {
                dir = Vector3.Slerp(forward, -playerPos.up, -v);
            }

            // 横のばらつき
            float h = Random.Range(-dispersion, dispersion);
            if (h >= 0)
            {
                dir = Vector3.Slerp(dir, playerPos.right, h);
            }
            else
            {
                dir = Vector3.Slerp(dir, -playerPos.right, -h);
            }

            rb.AddForce(dir.normalized * shotSpeed, ForceMode.Acceleration);
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
