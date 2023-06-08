using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    Canvas myCanvas;
    int activeCount = 0;    //アクティブにするか非アクティブ

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GetComponent<Canvas>();
        myCanvas.enabled = false;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeCount++;
            if (activeCount == 1)
            {
                myCanvas.enabled = true;
            }
            if (activeCount == 2)
            {
                myCanvas.enabled = false;
                activeCount = 0;
            }
        }
    }

    public void YesButton()
    {
        Application.Quit();
    }

    public void NoButton()
    {
        myCanvas.enabled = false;
        activeCount = 0;
    }
}
