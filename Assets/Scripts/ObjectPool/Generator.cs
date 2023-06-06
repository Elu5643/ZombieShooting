using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public delegate void DeleteEvent(GameObject game_obj);

    [SerializeField] GameObject objPrefab = null;
    [SerializeField] int firstPoolNum = 0;    //�ŏ��ɐ������鐔
    //C#�̓��I�z��
    public List<GameObject> objects;


    void Start()
    {
        //List<GameObject>(�z��p�T�C�Y)
        objects = new List<GameObject>(firstPoolNum);

        for (int i = 0; i < firstPoolNum; i++)
        {
            GameObject obj = Instantiate(objPrefab);

            //�e�q�֌W�ɂ���
            obj.transform.parent = transform;

            //��A�N�e�B�u�ɂ���
            obj.SetActive(false);

            //���O��ς���
            obj.name = objPrefab.name;

            objects.Add(obj);
        }
    }

    public GameObject PullObject(Vector3 pos, Vector3 forward, bool isAim, bool isMove,Transform playerPos, Vector3 hitPos)
    {
        GameObject obj = null;

        if (objects.Count > 0)
        {
            //�z��̐擪���g�p����
            obj = objects[0];

            //parent��null�ɂ���Ɗ֌W�����������
            obj.transform.parent = null;

            //�A�N�e�B�u�ɂ���
            obj.SetActive(true);

            //�z�񂩂珜������
            objects.Remove(obj);

        }
        else
        {
            //������������V�������
            obj = Instantiate(objPrefab);
            obj.name = objPrefab.name;
        }

        /*
            delegate�ϐ���PushObj�֐���ۑ�
        */
        obj.GetComponent<Maneged>().DeleteEvent = PushObject;

        //��������ԂŕԂ�
        obj.GetComponent<Bullet>().Initialize(pos, forward, isAim, isMove, playerPos, hitPos);

        return obj;
    }

    public void PushObject(GameObject obj)
    {
        if (obj.GetComponent<Maneged>() == null)
        {
            return;
        }

        //�e�q�֌W�����тȂ���
        obj.transform.parent = transform;

        //��A�N�e�B�u�ɂ���
        obj.SetActive(false);


        //Capacity�𒴂�����ۑ������Ȃ�
        if (objects.Count + 1 <= objects.Capacity)
        {
            objects.Add(obj);
        }
        else
        {
            //���ӂꂽ���͍폜
            Destroy(obj);
        }
    }
}