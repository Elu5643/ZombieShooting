using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    Canvas myCanvas = null;
    int activeCount = 1;    //アクティブにするか非アクティブ

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
            if (activeCount % 2 == 0)
            {
                myCanvas.enabled = true;
            }
            else if (activeCount % 2 == 1)
            {
                myCanvas.enabled = false;
            }
        }
    }

    public void ClickedButton()
    {
        Application.Quit();
    }

    public void NoButton()
    {
        myCanvas.enabled = false;
        activeCount = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
