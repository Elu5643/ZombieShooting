using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Itemオブジェクト
        ItemBox内で表示するItemオブジェクト
*/
public class Item : MonoBehaviour
{
    [SerializeField] Image itemImage = null;
    [SerializeField] Text numText = null;


    public void Init(GameObject parent, Vector3 pos, ItemData.ID item_id)
    {
        // Parent(ItemBox)との親子関係設定
        transform.SetParent(parent.transform);
        // 座標の設定
        transform.localPosition = pos;
        // アイテム画像の設定
        ItemData item_data = DBManager.Instance.GetItem(item_id);
        itemImage.sprite = item_data.Image;

        // アイテム個数の設定
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
