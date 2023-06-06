using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveControlledBob : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] float horizontalBobRange = 0.05f;
    [SerializeField] float verticalBobRange = 0.05f;

    [SerializeField] float walkHorizontalBobRange = 0f;
    [SerializeField] float walkVerticalBobRange = 0.1f;

    [SerializeField] float stopHorizontalBobRange = 0f;
    [SerializeField] float stopVerticalBobRange = 0.05f;

    float changeBobSpeed = 2.0f;


    [SerializeField]
    AnimationCurve Bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                        new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                        new Keyframe(2f, 0f));

    [SerializeField] float VerticaltoHorizontalRatio = 2f;

    float m_CyclePositionX;
    float m_CyclePositionY;
    float m_BobBaseInterval;
    Vector3 m_OriginalCameraPosition;
    float m_Time;


    public void Setup(Camera camera, float bobBaseInterval)
    {
        m_BobBaseInterval = bobBaseInterval;
        m_OriginalCameraPosition = camera.transform.localPosition;

        m_Time = Bobcurve[Bobcurve.length - 1].time;
    }


    public Vector3 DoHeadBob(float speed, bool isWalk)
    {
        if (!isWalk)
        {
            if (horizontalBobRange < walkHorizontalBobRange)
            {
                horizontalBobRange += changeBobSpeed * Time.deltaTime;
            }
            if (verticalBobRange < walkVerticalBobRange)
            {
                verticalBobRange += changeBobSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (horizontalBobRange > stopHorizontalBobRange)
            {
                horizontalBobRange -= changeBobSpeed * Time.deltaTime;
            }
            if (verticalBobRange > stopVerticalBobRange)
            {
                verticalBobRange -= changeBobSpeed * Time.deltaTime;
            }
        }

        float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX) * this.horizontalBobRange);
        float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY) * this.verticalBobRange);

        m_CyclePositionX += (speed * Time.deltaTime) / m_BobBaseInterval;
        m_CyclePositionY += ((speed * Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

        if (m_CyclePositionX > m_Time)
        {
            m_CyclePositionX = m_CyclePositionX - m_Time;
        }
        if (m_CyclePositionY > m_Time)
        {
            m_CyclePositionY = m_CyclePositionY - m_Time;
        }

        return new Vector3(xPos, yPos, 0f);
    }
}
