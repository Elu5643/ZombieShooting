using UnityEngine;

public class MiniMapCameraShader : MonoBehaviour
{
    [SerializeField] Camera camera;
    void Start()
    {
        camera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}