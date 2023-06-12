using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab = null;

    GameObject obj = null;
    Item item = null;

    public Item Item
    {
        get { return item; }
    }

    int activeCount = 1;        //�A�N�e�B�u�ɂ��邩��A�N�e�B�u

    // Start is called before the first frame update
    void Start()
    {
        int id_num = System.Enum.GetValues(typeof(ItemData.ID)).Length;
        int width_num = 5;

        for (int i = 1; i < id_num; i++)
        {
            obj = Instantiate(itemPrefab);

            // Item�\���ʒu�̊���o��
            Vector3 new_position = new Vector3(-360.0f, 240.0f, 0.0f);
            new_position.x += (i - 1) % width_num * 128.0f;
            new_position.y -= (i - 1) / width_num * 128.0f;

            // Item�I�u�W�F�N�g�̏�����
            obj.GetComponent<Item>().Init(gameObject, new_position, (ItemData.ID)i);
        }

        item = GetComponentInChildren<Item>();  //���̃R�[�h�ł���
        //item = obj.GetComponent<Item>();

        obj.SetActive(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            activeCount++;
            if(activeCount % 2 == 0)
            {
                obj.SetActive(true);
            }
            else if(activeCount % 2 == 1)
            {
                obj.SetActive(false);
            }
        }
    }
}