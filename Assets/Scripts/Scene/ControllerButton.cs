using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerButton : MonoBehaviour
{

    [SerializeField] AudioClip cheakSe = null;
    AudioSource audioSource;

    float fadeSpeed = 0.3f;        //透明度が変わるスピードを管理
    float red, green, blue, alpa;   //パネルの色、不透明度を管理
    [SerializeField] Image fadeImage;                //透明度を変更するパネルのイメージ
    bool isPush = false;

    [SerializeField] Text colorText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpa = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPush)
        {
            StartFadeOut();
        }
        else
        {
            StartFadeIn();
        }
    }

    public void OnClickControllerButton()
    {
        audioSource.PlayOneShot(cheakSe);
        isPush = true;
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alpa += fadeSpeed * Time.deltaTime;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alpa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isPush = false;
            SceneManager.LoadScene("Controller");
        }
    }

    void StartFadeIn()
    {
        fadeImage.enabled = true;
        alpa -= fadeSpeed * Time.deltaTime;
        SetAlpha();
        if (alpa <= 0)
        {
            fadeImage.enabled = false;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpa);
    }
}
