using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �G��|�������Ƀh���b�v����I�u�W�F�N�g
public class BulletObj : MonoBehaviour
{
    GameObject bullet = null;
    MagazineController magazine = null;

    float updownPos;          // ���̂��㉺�ɓ�������

    void Start()
    {
        bullet = GameObject.Find("BulletController");
        magazine = bullet.GetComponent<MagazineController>();

        Vector3 newPos = transform.position;�@          // ���݂̈ʒu���擾 
        Vector3 offSet = new Vector3(0.0f,1.25f,0.0f);  // �������グ��ׂɈʒu����
        newPos += offSet;
        transform.position = newPos;

        updownPos = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, updownPos + Mathf.PingPong(Time.time / 15, 0.1f), transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            magazine.Add();
            Destroy(gameObject);
        }
    }
}
