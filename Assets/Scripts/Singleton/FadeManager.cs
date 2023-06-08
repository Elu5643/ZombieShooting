using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }

    float fadeSpeed = 0.5f;             //透明度が変わるスピードを管理
    float red, green, blue, alpa;       //パネルの色、不透明度を管理
    [SerializeField] Image fadeImage;   //透明度を変更するパネルのイメージ

    bool isFadeIn = false;      //FadeInになったかを判定
    public bool IsFadeIn
    {
        get { return isFadeIn; }
    }
    bool isFadeOut = false;     //FadeOutになったかを判定
    public bool IsFadeOut
    {
        get { return isFadeOut; }
    }

    void Start()
    {
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpa = fadeImage.color.a;
    }

    void Update()
    {
        if (!isFadeIn)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        fadeImage.enabled = true;
        isFadeOut = true;
        alpa -= fadeSpeed * Time.deltaTime;
        SetAlpha();
        if (alpa <= 0)
        {
            isFadeIn = true;
            fadeImage.enabled = false;
        }
    }

    public void FadeOut()
    {
        fadeImage.enabled = true;
        alpa += fadeSpeed * Time.deltaTime;
        SetAlpha();
        if (alpa >= 1)
        {
            isFadeOut = false;
            isFadeIn = false;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpa);
    }
}

