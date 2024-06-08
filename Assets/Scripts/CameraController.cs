using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0,1)]
    public float smoothTime;

    public Vector3 positionOffset;

    // set camera limits
    [Header("Axis Limitation")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private Vector3 initialPosition;
    private bool followBall = false;
    private bool resetCamera = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
        initialPosition = transform.position; // store init position of the camera
    }

    private void LateUpdate()
    {
        if (followBall)
        {
            Vector3 targetPosition = target.position + positionOffset;
            targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if (resetCamera)
        {
            transform.position = Vector3.SmoothDamp(transform.position, initialPosition, ref velocity, smoothTime);
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f) // Check if close enough to initial position
            {
                transform.position = initialPosition; // Snap to initial position to avoid tiny floating point errors
                resetCamera = false; // Stop resetting the camera
            }
        }

    }

    public void SetFollowBall(bool follow)
    {
        followBall = follow;
    }
    public void ResetCameraPosition()
    {
        resetCamera = true;
    }
}
