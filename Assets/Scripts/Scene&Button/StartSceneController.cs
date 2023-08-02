using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    SceneChanger start;

    float waitTime = 2.0f;

    void Awake()
    {
        start = GetComponent<SceneChanger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        start.Change("InGame", waitTime);
    }
}
