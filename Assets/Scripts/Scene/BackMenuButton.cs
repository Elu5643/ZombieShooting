using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
    [SerializeField] AudioClip cheakSe = null;
    AudioSource audioSource;

    float fadeSpeed = 0.3f;        //透明度が変わるスピードを管理
    float red, green, blue, alpa;   //パネルの色、不透明度を管理
    [SerializeField] Image fadeImage;                //透明度を変更するパネルのイメージ
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
        alpa -= fadeSpeed * Time.deltaTime;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alpa <= 0)
        {
            fadeImage.enabled = false;
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;
        alpa += fadeSpeed * Time.deltaTime;        // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
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
