using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] AudioClip cheakSe = null;
    [SerializeField] Text colorText = null;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStartButton()
    {
        StartCoroutine("ClickStart");
    }

    IEnumerator ClickStart()
    {
        audioSource.PlayOneShot(cheakSe);
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
        while (true)
        {
            FadeManager.Instance.FadeOut();
            if (FadeManager.Instance.IsFadeOut == false)
            {
                SceneManager.LoadScene("Start");
                yield break;
            }
            yield return null;
        }

    }
}
