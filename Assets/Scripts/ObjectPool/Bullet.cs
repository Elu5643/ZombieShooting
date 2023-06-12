using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Generator.DeleteEvent deleteEvent = null;

    GameObject generator = null;

    Rigidbody rb = null;

    float timer = 0.0f;     //�I�u�W�F�N�g�������鎞�Ԃ��v��

    float shotSpeed = 2000f;    //�e���΂��X�s�[�h

    Vector3 direction;

    float dispersion = 0.02f; // �΂���
    float verticalToHorizontalRatio = 1.5f; // �΂���̏c����
    float angle = 0.05f; // ���ˊp

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
            BulletDisparity(forward, isMove, playerPos, hitPos);    //�e�̂΂炯�
        }
        else
        {
            BulletDisparity(forward, isMove, playerPos, hitPos);    //�e�̂΂炯�
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

            // �c�̂΂��
            float v = Random.Range(-dispersion * verticalToHorizontalRatio - angle, dispersion * verticalToHorizontalRatio + angle);
            if (v >= 0)
            {
                dir = Vector3.Slerp(forward, playerPos.up, v);
            }
            else
            {
                dir = Vector3.Slerp(forward, -playerPos.up, -v);
            }

            // ���̂΂��
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

    // Rigidbody�̐ݒ��Continuous Dynamic�ɂ��Ă��邯�ǂ��蔲�����ۂ̕ی�
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }

    // Rigidbody�̐ݒ��Continuous Dynamic�ɂ��Ă��邯�ǂ��蔲�����ۂ̕ی�
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Maneged>().ExecuteEvent(gameObject);
        }
    }
}
