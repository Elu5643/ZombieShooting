using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    void Update()
    {
        if(FadeManager.Instance.IsFadeIn)
        {
            FadeManager.Instance.FadeOut();
        }
        if (FadeManager.Instance.IsFadeOut == false)
        {
            SceneManager.LoadScene("InGame");

        }
    }
}
