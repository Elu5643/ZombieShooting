using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Text text;

    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SE;

    [SerializeField] AudioClip bgm = null;
    [SerializeField] AudioClip clearSe = null;
    [SerializeField] AudioClip failedSe = null;

    [SerializeField] Player player;

    public enum GamePart
    {
        Init,         //イントロ
        Main,         //ゲーム中
        Clear,        //クリア
        Failed,       //失敗
    }

    GamePart currentPart = GamePart.Init;
    float timer = 0.0f;

    public void FailedGame()
    {
        currentPart = GamePart.Failed;
        timer = 0.0f;
        text.text = "GameOver";

        BGM.Stop();
        if (!SE.isPlaying)
        {
            SE.PlayOneShot(failedSe);
        }
    }

    public void ClearGame()
    {
        currentPart = GamePart.Clear;
        timer = 0.0f;
        text.text = "Clear";

        BGM.Stop();
        if (!SE.isPlaying)
        {
            SE.PlayOneShot(clearSe);
        }
    }

    void StartFadeIn()
    {
        FadeManager.Instance.FadeOut();
        if (FadeManager.Instance.IsFadeOut == false)
        {
            currentPart = GamePart.Main;
            player.IsStop = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPart)
        {
            case GamePart.Init:
                UpdateInit();
                break;

            case GamePart.Main:
                UpdateMain();
                break;

            case GamePart.Clear:
                UpdateClear();
                break;

            case GamePart.Failed:
                UpdateFailed();
                break;
        }
    }

    void UpdateInit()
    {
        StartFadeIn();
    }

    void UpdateMain()
    {
        if (!BGM.isPlaying)
        {
            BGM.PlayOneShot(bgm);
        }
    }

    void UpdateClear()
    {
        timer += Time.deltaTime;
        const float finish_time = 5.0f;
        if (timer >= finish_time)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void UpdateFailed()
    {
        timer += Time.deltaTime;
        const float finish_time = 5.0f;
        if (timer >= finish_time)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
