using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
    [SerializeField] AudioClip cheakSe = null;
    AudioSource audioSource;

    float fadeSpeed = 0.3f;        //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alpa;   //�p�l���̐F�A�s�����x���Ǘ�
    [SerializeField] Image fadeImage;                //�����x��ύX����p�l���̃C���[�W
    bool isPush = false;

    [SerializeField] Text colorText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpa = fadeImage.color.a;
    }

    void Update()
    {
        if(isPush == false)
        {
            StartFadeIn();
        }
        else
        {
            StartFadeOut();
        }
    }

    public void OnClickBackMenuButton()
    {
        audioSource.PlayOneShot(cheakSe);
        isPush = true;
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
    }

    void StartFadeIn()
    {
        fadeImage.enabled = true;
        alpa -= fadeSpeed * Time.deltaTime;                //a)�s�����x�����X�ɉ�����
        SetAlpha();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alpa <= 0)
        {
            fadeImage.enabled = false;
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;
        alpa += fadeSpeed * Time.deltaTime;        // b)�s�����x�����X�ɂ�����
        SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
        if (alpa >= 1)
        {
            SceneManager.LoadScene("Start");
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpa);
    }
}
