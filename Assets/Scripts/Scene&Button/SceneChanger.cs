using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class SceneChanger : MonoBehaviour
{
    // 汎用性で他のシーンチェンジでも使えるように
    public void Change(string nextName, float waitTime)
    {
        StartCoroutine(WaitChange(nextName, waitTime));
    }

    IEnumerator WaitChange(string nextName,float waitTime)
    {
        while (true)
        {
            if(FadeManager.Instance.IsFadeIn == false)
            {
                yield return new WaitForSeconds(waitTime); // ここで処理を時間停止だったりする

                FadeManager.Instance.FadeOut();
                break;
            }
            yield return null;
        }

        while (FadeManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextName);
    }
}
