using UnityEngine;

public class MiniMapCameraShader : MonoBehaviour
{
    Camera camera = null;
    void Start()
    {
        camera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        camera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}