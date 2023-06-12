using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText = null;
    [SerializeField] ItemBox itemBox = null;

    const int maxNum = 30;  //����e�̐�

    int mainNum = maxNum;   //���ݏe�ɓ����Ă���e
    int subNum = maxNum;    //�T�u�Œe�������Ă���e


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

    public void AddBullet()
    {
        subNum += 10;
        itemBox.Item.SetNum(subNum, ItemData.ID.Item01);
    }

    public void ShotBullet()
    {
        mainNum -= 1;
        numText.text = mainNum.ToString();
    }

    public void ReloadBullet()
    {
        int num = maxNum;       //���������炷�ׂ̒����ϐ�

        if (subNum >= maxNum)
        {
            num -= mainNum;
            subNum -= num;

            num = maxNum;
            mainNum = maxNum;
        }
        else
        {
            mainNum += subNum;
            if (mainNum > maxNum)
            {
                num -= mainNum;
                subNum += num;

                num = maxNum;
                mainNum = maxNum;
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
