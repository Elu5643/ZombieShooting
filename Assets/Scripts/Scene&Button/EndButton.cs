using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    Canvas canvas = null;
    int activeCount = 1;    // �A�N�e�B�u�ɂ��邩��A�N�e�B�u

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
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
                canvas.enabled = true;
            }
            else if (activeCount % 2 == 1)
            {
                canvas.enabled = false;
            }
        }
    }

    // �{�^��UI�Ŏg����public
    public void ClickedButton()
    {
        Application.Quit();
    }

    // �{�^��UI�Ŏg����public
    public void NoButton()
    {
        canvas.enabled = false;
        activeCount = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
