using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public delegate void DeleteEvent(GameObject game_obj);

    [SerializeField] GameObject objPrefab = null;
    [SerializeField] int firstPoolNum = 0;    //最初に生成する数
    //C#の動的配列
    public List<GameObject> objects;


    void Start()
    {
        //List<GameObject>(配列用サイズ)
        objects = new List<GameObject>(firstPoolNum);

        for (int i = 0; i < firstPoolNum; i++)
        {
            GameObject obj = Instantiate(objPrefab);

            //親子関係にする
            obj.transform.parent = transform;

            //非アクティブにする
            obj.SetActive(false);

            //名前を変える
            obj.name = objPrefab.name;

            objects.Add(obj);
        }
    }

    public GameObject PullObject(Vector3 pos, Vector3 forward, bool isAim, bool isMove,Transform playerPos, Vector3 hitPos)
    {
        GameObject obj = null;

        if (objects.Count > 0)
        {
            //配列の先頭を使用する
            obj = objects[0];

            //parentをnullにすると関係が解消される
            obj.transform.parent = null;

            //アクティブにする
            obj.SetActive(true);

            //配列から除去する
            objects.Remove(obj);

        }
        else
        {
            //無かったから新しく作る
            obj = Instantiate(objPrefab);
            obj.name = objPrefab.name;
        }

        /*
            delegate変数にPushObj関数を保存
        */
        obj.GetComponent<Maneged>().DeleteEvent = PushObject;

        //初期化状態で返す
        obj.GetComponent<Bullet>().Initialize(pos, forward, isAim, isMove, playerPos, hitPos);

        return obj;
    }

    public void PushObject(GameObject obj)
    {
        if (obj.GetComponent<Maneged>() == null)
        {
            return;
        }

        //親子関係を結びなおす
        obj.transform.parent = transform;

        //非アクティブにする
        obj.SetActive(false);


        //Capacityを超えたら保存させない
        if (objects.Count + 1 <= objects.Capacity)
        {
            objects.Add(obj);
        }
        else
        {
            //あふれた分は削除
            Destroy(obj);
        }
    }
}