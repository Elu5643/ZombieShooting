using UnityEngine;

public class CameraShader : MonoBehaviour
{
    [SerializeField] Camera camera;

    void Start()
    {
        camera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
    }
}
