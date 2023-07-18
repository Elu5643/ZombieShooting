using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    void Update()
    {
        // 向いている方向取得
        Quaternion targetRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
    }
}
