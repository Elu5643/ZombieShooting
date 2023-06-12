using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
    [SerializeField] AudioClip checkSe = null;
    [SerializeField] Text colorText = null;

    AudioSource audioSource = null;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickBackMenuButton()
    {
        StartCoroutine(BackMenuClick());
    }

    IEnumerator BackMenuClick()
    {
        audioSource.PlayOneShot(checkSe);
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
        while (true)
        {
            FadeManager.Instance.FadeOut();
            if (FadeManager.Instance.IsFadeOut == false)
            {
                SceneManager.LoadScene("Menu");
                yield break;
            }
            yield return null;
        }

    }
}
