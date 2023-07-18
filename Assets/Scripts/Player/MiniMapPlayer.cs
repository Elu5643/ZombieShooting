using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    void Update()
    {
        // Œü‚¢‚Ä‚¢‚é•ûŒüŽæ“¾
        Quaternion targetRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
    }
}
