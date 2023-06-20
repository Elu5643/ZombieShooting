using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] Player player = null;

    [Range(0.1f, 10f)]
    // �J�������x�A���l���傫���قǂ�蒼���I�ȑ��삪�\.
    [SerializeField] float lookSensitivity = 5f;
    [Range(0.1f, 1f)]
    // ���l���傫���قǃJ�������������������Ɍ����܂ł̎��Ԃ������Ȃ�.
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
                yRot += Input.GetAxis("Mouse X") * lookSensitivity; // �}�E�X�̈ړ�.
                xRot -= Input.GetAxis("Mouse Y") * lookSensitivity; // �}�E�X�̈ړ�.
                xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y);// �㉺�̊p�x�ړ��̍ő�A�ŏ�.

                currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
                currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

                transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
            }
        }
    }
}