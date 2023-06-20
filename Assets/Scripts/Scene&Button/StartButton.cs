using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] AudioClip checkSE = null;
    [SerializeField] Text colorText = null;

    AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ƒ{ƒ^ƒ“UI‚ÅŽg‚¤ˆ×public
    public void OnClickStartButton()
    {
        StartCoroutine(ClickStart());
    }

    IEnumerator ClickStart()
    {
        audioSource.PlayOneShot(checkSE);
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
        FadeManager.Instance.FadeOut();
        while (FadeManager.Instance.IsFadeOut)
        {
            yield return null;
        }
        SceneManager.LoadScene("Start");
    }
}
