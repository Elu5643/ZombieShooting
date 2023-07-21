using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// ƒvƒŒƒCƒ„[‚Ì‹…”‚ğ§Œä
public class MagazineController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText = null;
    [SerializeField] ItemBox itemBox = null;

    const int maxNum = 30;  // ãŒÀ’e‚Ì”

    int mainNum = maxNum;   // Œ»İe‚É“ü‚Á‚Ä‚¢‚é’e
    int subNum = maxNum;    // ƒTƒu‚Å’e‚ğ‚Á‚Ä‚¢‚é’e


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

    // ƒTƒu‚Ì‹…”‚ğ’Ç‰Á
    public void Add()
    {
        subNum += 10;
        itemBox.Item.itemDisplay(subNum, ItemData.ID.Item01);
    }

    // e‚ğ‘Å‚Â‚½‚Ñ‚É’e‚ğŒ¸‚ç‚·
    public void Shot()
    {
        mainNum -= 1;
        numText.text = mainNum.ToString();
    }

    // ƒŠƒ[ƒh
    public void Reload()
    {
        Action<bool> AdjustBulletNum = (bool is_subtraction) =>
        {
            int num = maxNum;   // ‹…”‚ğŒ¸‚ç‚·ˆ×‚Ì’²®•Ï”

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
