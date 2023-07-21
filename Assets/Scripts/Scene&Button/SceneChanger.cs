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
    public void Change(string nextName)
    {
        StartCoroutine(WaitChange(nextName));
    }

    IEnumerator WaitChange(string nextName)
    {
        while (true)
        {
            if(FadeManager.Instance.IsFadeIn == false)
            {
                FadeManager.Instance.FadeOut();
                break;
            }
            yield return null;
        }

        // yield return new WaitForSeconds(); ここで処理を時間停止だったり一時停止したりできる

        while (FadeManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextName);
    }
}
