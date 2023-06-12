using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    GameObject bullet = null;
    BulletController bulletController = null;

    float updownPos;          //���̂��㉺�ɓ�������

    private void Start()
    {
        bullet = GameObject.Find("BulletController");
        bulletController = bullet.GetComponent<BulletController>();

        Vector3 newPos = transform.position;�@          //���݂̈ʒu���擾 
        Vector3 offSet = new Vector3(0.0f,1.25f,0.0f);  //�������グ��ׂɈʒu����
        newPos += offSet;
        transform.position = newPos;

        updownPos = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, updownPos + Mathf.PingPong(Time.time / 15, 0.1f), transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bulletController.AddBullet();
            Destroy(gameObject);
        }
    }
}
