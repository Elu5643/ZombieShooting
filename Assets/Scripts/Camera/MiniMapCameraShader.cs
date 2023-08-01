using UnityEngine;

public class MiniMapCameraShader : MonoBehaviour
{
    Camera myCamera = null;
    void Start()
    {
        myCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        myCamera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}