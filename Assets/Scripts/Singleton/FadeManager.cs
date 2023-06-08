using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }

    float fadeSpeed = 0.5f;             //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alpa;       //�p�l���̐F�A�s�����x���Ǘ�
    [SerializeField] Image fadeImage;   //�����x��ύX����p�l���̃C���[�W

    bool isFadeIn = false;      //FadeIn�ɂȂ������𔻒�
    public bool IsFadeIn
    {
        get { return isFadeIn; }
    }
    bool isFadeOut = false;     //FadeOut�ɂȂ������𔻒�
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

