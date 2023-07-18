using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Bullet : MonoBehaviour
{
    public Generator.DeleteEvent deleteEvent = null;

    Maneged maneged = null;

    GameObject generator = null;
    public GameObject Generator
    {
        set
        {
            generator = value;
        }
    }

    Rigidbody rb = null;

    float timer = 0.0f;     //�I�u�W�F�N�g�������鎞�Ԃ��v��

    float shotSpeed = 2000f;    // �e���΂��X�s�[�h

    float dispersion = 0.02f; // �΂���
    float verticalToHorizontalRatio = 1.5f; // �΂���̏c����
    float angle = 0.05f; // ���ˊp

    // �e��łۂ̏�����
    public void Initialize(Vector3 pos, Vector3 forward, bool isMove, Transform playerPos, Vector3 hitPos)
    {
        transform.position = pos;
        transform.forward = forward;

        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);

        Disparity(forward, isMove, playerPos, hitPos);    // �e�̂΂炯�
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        maneged = GetComponent<Maneged>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            maneged.ExecuteEvent(gameObject);
        }
    }

    void Disparity(Vector3 forward, bool isMove, Transform playerPos, Vector3 hitPos)
    {
        if (isMove == false)
        {
            Vector3 direction = hitPos - transform.position;
            rb.AddForce(direction.normalized * shotSpeed, ForceMode.Acceleration);
        }
        else
        {
            Vector3 direction;

            Func<float, Vector3, Vector3, Vector3> GetDisparityDirection = (axis, startPoint, endPoint) =>
            {
                if (axis >= 0)
                {
                    direction = Vector3.Slerp(startPoint, endPoint, axis);
                }
                else
                {
                    direction = Vector3.Slerp(startPoint, -endPoint, axis);
                }

                return direction;
            };

            // �c�̂΂��
            float vertical = UnityEngine.Random.Range(-dispersion * verticalToHorizontalRatio - angle, dispersion * verticalToHorizontalRatio + angle);
            direction = GetDisparityDirection(vertical, forward, playerPos.up);

            // ���̂΂��
            float horizontal = UnityEngine.Random.Range(-dispersion, dispersion);
            direction = GetDisparityDirection(horizontal, direction, playerPos.right);


            rb.AddForce(direction.normalized * shotSpeed, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Ground")
        {
            maneged.ExecuteEvent(gameObject);
        }
    }
}
