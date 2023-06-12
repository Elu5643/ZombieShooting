using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerButton : MonoBehaviour
{

    [SerializeField] AudioClip checkSe = null;
    [SerializeField] Text colorText = null;

    AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickControllerButton()
    {
        StartCoroutine(ClickButton());
    }

    IEnumerator ClickButton()
    {
        audioSource.PlayOneShot(checkSe);
        colorText.color = new Color(0.2f, 0.8f, 0.1f, 1.0f);
        while (true)
        {
            FadeManager.Instance.FadeOut();
            if (FadeManager.Instance.IsFadeOut == false)
            {
                SceneManager.LoadScene("Controller");
                yield break;
            }
            yield return null;
        }

    }
}
