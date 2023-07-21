using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// �v���C���[�̋����𐧌�
public class MagazineController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText = null;
    [SerializeField] ItemBox itemBox = null;

    const int maxNum = 30;  // ����e�̐�

    int mainNum = maxNum;   // ���ݏe�ɓ����Ă���e
    int subNum = maxNum;    // �T�u�Œe�������Ă���e


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

    // �T�u�̋�����ǉ�
    public void Add()
    {
        subNum += 10;
        itemBox.Item.itemDisplay(subNum, ItemData.ID.Item01);
    }

    // �e��ł��тɒe�����炷
    public void Shot()
    {
        mainNum -= 1;
        numText.text = mainNum.ToString();
    }

    // �����[�h
    public void Reload()
    {
        Action<bool> AdjustBulletNum = (bool is_subtraction) =>
        {
            int num = maxNum;   // ���������炷�ׂ̒����ϐ�

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
        itemBox.Item.itemDisplay(subNum, ItemData.ID.Item01);
    }
}
