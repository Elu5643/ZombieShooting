using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
    float fadeSpeed = 0.3f;        //透明度が変わるスピードを管理
    float red, green, blue, alpa;   //パネルの色、不透明度を管理
    bool isFadeIn = true;   //フェードイン処理の開始、完了を管理するフラグ
    Image fadeImage;                //透明度を変更するパネルのイメージ

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpa = fadeImage.color.a;

        alpa = 1;
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }
        else
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        fadeImage.enabled = true;
        alpa -= fadeSpeed * Time.deltaTime;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alpa <= 0)
        {
            isFadeIn = false;
        }
    }

    void StartFadeOut()
    {
        alpa += fadeSpeed * Time.deltaTime;        // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alpa >= 1)
        {
            SceneManager.LoadScene("InGame");
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpa);
    }
}
