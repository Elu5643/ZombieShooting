using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] Player player = null;

    [Range(0.1f, 10f)]
    // カメラ感度、数値が大きいほどより直感的な操作が可能.
    [SerializeField] float lookSensitivity = 5f;

    [Range(0.1f, 10f)]
    // エイムした時のカメラ感度、数値が大きいほどより直感的な操作が可能.
    [SerializeField] float aimSensitivity = 2.5f;

    [Range(0.1f, 1f)]
    // 数値が大きいほどカメラが向きたい方向に向くまでの時間が長くなる.
    [SerializeField] float lookSmooth = 0.1f;

    [SerializeField] Vector2 MinMaxAngle = new Vector2(-90, 90);

    private float yRot;
    private float xRot;

    private float currentYRot;
    private float currentXRot;

    private float yRotVelocity;
    private float xRotVelocity;

    void Update()
    {
        if (!player.IsStop)
        {
            if (!Cursor.visible)
            {
                if(player.IsAim())
                {
                    LookorAim(aimSensitivity);
                }
                else
                {
                    LookorAim(lookSensitivity);
                }

                xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);// 上下の角度移動の最大、最小.

                currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
                currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

                transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
            }
        }
    }

    void LookorAim(float sensitivity)
    {
        yRot += Input.GetAxis("Mouse X") * sensitivity; // マウスの移動.
        xRot -= Input.GetAxis("Mouse Y") * sensitivity; // マウスの移動.
    }
}