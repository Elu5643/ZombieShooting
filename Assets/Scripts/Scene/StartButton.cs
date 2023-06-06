using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    [SerializeField] AudioClip cheakSe = null;
    AudioSource audioSource;

    float fadeSpeed = 0.3f;        //�����x���ς��X�s�[�h���Ǘ�
    float red, green, blue, alpa;   //�p�l���̐F�A�s�����x���Ǘ�
    [SerializeField] Image fadeImage;                //�����x��ύX����p�l���̃C���[�W
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
        if(isPush)
        {
            StartFadeOut();
        }
        else
        {
            alpa -= fadeSpeed * Time.deltaTime;
        }
    }

    public void OnClickStartButton()
    {
        audioSource.PlayOneShot(cheakSe);
        isPush = true;
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)�p�l���̕\�����I���ɂ���
        alpa += fadeSpeed * Time.deltaTime;         // b)�s�����x�����X�ɂ�����
        SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
        if (alpa >= 1)
        {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
            SceneManager.LoadScene("Init");
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpa);
    }
}
