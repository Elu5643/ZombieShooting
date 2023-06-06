using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletObj : MonoBehaviour
{
    GameObject bullet;
    BulletController controller;

    Vector3 newPos;     //���݂̈ʒu���擾 
    Vector3 offSet;     //�������グ��ׂɈʒu����

    float pos;          //���̂��㉺�ɓ�������

    private void Start()
    {
        bullet = GameObject.Find("BulletController");
        controller = bullet.GetComponent<BulletController>();

        newPos = transform.position;
        offSet = new Vector3(0.0f,1.25f,0.0f);
        newPos += offSet;
        transform.position = newPos;

        pos = this.transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, pos + Mathf.PingPong(Time.time / 15, 0.1f), transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            controller.AddBullet();
            Destroy(gameObject);
        }
    }
}
