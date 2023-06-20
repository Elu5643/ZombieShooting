using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveControlledBob : MonoBehaviour
{
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

    float cyclePositionX;
    float cyclePositionY;
    float bobBaseInterval;
    Vector3 originalCameraPosition;
    float time;

    // ç≈èâÇÃÉJÉÅÉâÇÃóhÇÍÇí≤êÆ
    public void Setup(Camera camera, float bobBaseInterval)
    {
        this.bobBaseInterval = bobBaseInterval;
        originalCameraPosition = camera.transform.localPosition;

        time = Bobcurve[Bobcurve.length - 1].time;
    }

    // ï‡Ç¢ÇƒÇ¢ÇÈÇ©îªíËÇÇµÉJÉÅÉâÇÃóhÇÍÇí≤êÆ
    public Vector3 DoHeadBob(float speed, bool isWalk)
    {
        if (isWalk)
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

        float xPos = originalCameraPosition.x + (Bobcurve.Evaluate(cyclePositionX) * horizontalBobRange);
        float yPos = originalCameraPosition.y + (Bobcurve.Evaluate(cyclePositionY) * verticalBobRange);

        cyclePositionX += (speed * Time.deltaTime) / bobBaseInterval;
        cyclePositionY += ((speed * Time.deltaTime) / bobBaseInterval) * VerticaltoHorizontalRatio;

        if (cyclePositionX > time)
        {
            cyclePositionX = cyclePositionX - time;
        }
        if (cyclePositionY > time)
        {
            cyclePositionY = cyclePositionY - time;
        }

        return new Vector3(xPos, yPos, 0f);
    }
}
