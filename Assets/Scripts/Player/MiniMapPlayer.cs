using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    void Update()
    {
        // �����Ă�������擾
        Quaternion targetRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
    }
}
