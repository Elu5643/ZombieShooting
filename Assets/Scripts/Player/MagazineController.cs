using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// プレイヤーの球数を制御
public class MagazineController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText = null;
    [SerializeField] ItemBox itemBox = null;

    const int maxNum = 30;  // 上限弾の数

    int mainNum = maxNum;   // 現在銃に入っている弾
    int subNum = maxNum;    // サブで弾を持っている弾


    public int MainNum
    {
        get { return mainNum; }
    }

    public int SubNum
    {
        get { return subNum; }
    }

    // Start is called before the first frame update
    void Start()
    {
        numText.text = mainNum.ToString();
    }

    // サブの球数を追加
    public void Add()
    {
        subNum += 10;
        itemBox.Item.SetNum(subNum, ItemData.ID.Item01);
    }

    // 銃を打つたびに弾を減らす
    public void Shot()
    {
        mainNum -= 1;
        numText.text = mainNum.ToString();
    }

    // リロード
    public void Reload()
    {
        Action<bool> AdjustBulletNum = (bool is_subtraction) =>
        {
            int num = maxNum;   // 球数を減らす為の調整変数

            num -= mainNum;
            if (is_subtraction)
            {
                num *= -1;
            }
            subNum += num;
        
            mainNum = maxNum;
        };
        
        if (subNum + mainNum >= maxNum)
        {
            AdjustBulletNum(true);
        }
        else
        {
            mainNum += subNum;
            if (mainNum > maxNum)
            {
                AdjustBulletNum(false);
            }
            else
            {
                subNum = 0;
            }
        }

        numText.text = mainNum.ToString();
        itemBox.Item.SetNum(subNum, ItemData.ID.Item01);
    }
}
