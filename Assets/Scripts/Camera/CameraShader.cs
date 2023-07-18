using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraShader : MonoBehaviour
{
    [SerializeField] Camera camera;

    void Start()
    {
        camera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}
