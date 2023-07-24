using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab = null;

    GameObject itemObj = null;
    Item item = null;

    public Item Item
    {
        get { return item; }
    }

    int activeCount = 1;        // アクティブにするか非アクティブ

    // Start is called before the first frame update
    void Start()
    {
        int id_num = System.Enum.GetValues(typeof(ItemData.ID)).Length;
        int width_num = 5;
        float displayPos = 128.0f;

        for (int i = 1; i < id_num; i++)
        {
            itemObj = Instantiate(itemPrefab);

            // Item表示位置の割り出し
            Vector3 new_position = new Vector3(-360.0f, 240.0f, 0.0f);
            new_position.x += (i - 1) % width_num * displayPos;
            new_position.y -= (i - 1) / width_num * displayPos;

            // Itemオブジェクトの初期化
            itemObj.GetComponent<Item>().Init(gameObject, new_position, (ItemData.ID)i);
        }

        item = GetComponentInChildren<Item>();  // 下のコードでも可
        // item = obj.GetComponent<Item>();

        itemObj.SetActive(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            activeCount++;
            if(activeCount % 2 == 0)
            {
                itemObj.SetActive(true);
            }
            else if(activeCount % 2 == 1)
            {
                itemObj.SetActive(false);
            }
        }
    }
}