using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    [SerializeField] AudioClip checkSE = null;
    [SerializeField] Text colorText = null;

    AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ボタンUIで使う為public
    public void OnClickControllerButton()
    {
        StartCoroutine(ClickButton());
    }

    IEnumerator ClickButton()
    {
        audioSource.PlayOneShot(checkSE);
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
        FadeManager.Instance.FadeOut();
        while (FadeManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene("Controller");
    }
}
