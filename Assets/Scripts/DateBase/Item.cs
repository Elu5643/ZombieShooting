using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Item�I�u�W�F�N�g
        ItemBox���ŕ\������Item�I�u�W�F�N�g
*/
public class Item : MonoBehaviour
{
    [SerializeField] Image itemImage = null;
    [SerializeField] Text numText = null;


    public void Init(GameObject parent, Vector3 pos, ItemData.ID item_id)
    {
        // Parent(ItemBox)�Ƃ̐e�q�֌W�ݒ�
        transform.SetParent(parent.transform);
        // ���W�̐ݒ�
        transform.localPosition = pos;
        // �A�C�e���摜�̐ݒ�
        ItemData item_data = DBManager.Instance.GetItem(item_id);
        itemImage.sprite = item_data.Image;

        // �A�C�e�����̐ݒ�
        ItemData item_num = DBManager.Instance.GetItem(item_id);
        numText.text = item_num.Num.ToString();
    }

    public void SetNum(int num, ItemData.ID item_id)
    {
        switch(item_id)
        {
            case ItemData.ID.Item01:
                numText.text = num.ToString();
                break;
        }
    }
}
