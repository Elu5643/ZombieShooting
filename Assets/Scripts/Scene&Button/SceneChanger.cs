using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class SceneChanger : MonoBehaviour
{
    // �ėp���ő��̃V�[���`�F���W�ł��g����悤��
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

        while (FadeManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextName);
    }
}
