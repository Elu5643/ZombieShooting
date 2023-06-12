using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Text resultText = null;

    [SerializeField] AudioSource BGMSource = null;
    [SerializeField] AudioSource SESource = null;

    [SerializeField] AudioClip clearSe = null;
    [SerializeField] AudioClip failedSe = null;

    [SerializeField] Player player = null;

    void Start()
    {
        StartCoroutine(StartFadeIn());
    }

    public void FailureGame()
    {
        StartCoroutine(Result("GameOver", failedSe));
    }

    public void ClearGame()
    {
        StartCoroutine(Result("Clear", clearSe));
    }

    IEnumerator StartFadeIn()
    {
        while (true)
        {
            if (FadeManager.Instance.IsFadeOut == false)
            {
                player.IsStop = false;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator Result(string message, AudioClip se)
    {
        const float staySecond = 3.0f;
        BGMSource.Stop();
        resultText.text = message;
        SESource.PlayOneShot(se);

        yield return new WaitForSeconds(staySecond);

        SceneManager.LoadScene("Menu");
    }
}
