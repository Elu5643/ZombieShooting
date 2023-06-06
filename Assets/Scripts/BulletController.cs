using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class BulletController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText = null;
    [SerializeField] ItemBox itemBox = null;


    const int maxNum = 30;  //ãŒÀ’e‚Ì”

    int mainNum = maxNum;   //Œ»Ýe‚É“ü‚Á‚Ä‚¢‚é’e
    int subNum = maxNum;    //ƒTƒu‚Å’e‚ðŽ‚Á‚Ä‚¢‚é’e

    int num = maxNum;       //‹…”‚ðŒ¸‚ç‚·ˆ×

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
        itemBox.Item.Num(subNum, ItemData.ID.Item01);
    }

    public void ShotBullet()
    {
        mainNum -= 1;
        numText.text = mainNum.ToString();
    }

    public void ReloadBullet()
    {
        if(subNum >= maxNum)
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
        itemBox.Item.Num(subNum, ItemData.ID.Item01);
    }
}
