using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
    float fadeSpeed = 0.3f;        //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alpa;   //�p�l���̐F�A�s�����x���Ǘ�
    bool isFadeIn = true;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O
    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W

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
        alpa -= fadeSpeed * Time.deltaTime;                //a)�s�����x�����X�ɉ�����
        SetAlpha();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alpa <= 0)
        {
            isFadeIn = false;
        }
    }

    void StartFadeOut()
    {
        alpa += fadeSpeed * Time.deltaTime;        // b)�s�����x�����X�ɂ�����
        SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
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
