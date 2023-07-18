using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Text resultText = null;

    [SerializeField] AudioSource BGMSource = null;
    [SerializeField] AudioSource RainSource = null;
    [SerializeField] AudioSource SESource = null;

    [SerializeField] AudioClip clearSE = null;
    [SerializeField] AudioClip failedSE = null;

    [SerializeField] Player player = null;

    void Start()
    {
        StartCoroutine(StartFadeIn());
    }

    // プレイヤー側で死亡した際にこの関数を呼ぶ
    public void FailureGame()
    {
        StartCoroutine(Result("GameOver", failedSE));
    }

    // プレイヤー側でクリアした際にこの関数を呼ぶ
    public void ClearGame()
    {
        StartCoroutine(Result("Clear", clearSE));
    }


    IEnumerator StartFadeIn()
    {
        while (true)
        {
            if (FadeManager.Instance.IsFadeIn == false)
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
        RainSource.Stop();
        resultText.text = message;
        SESource.PlayOneShot(se);

        yield return new WaitForSeconds(staySecond);

        SceneManager.LoadScene("Menu");
    }
}
