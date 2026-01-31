using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float smoothTime = 0.2f;     // Lower = snappier, Higher = smoother
    public Vector2 offset = Vector2.zero;

    [Header("Bounds (Optional)")]
    public bool useBounds = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z // keep camera Z
        );

        // Smoothly move camera
        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        // Clamp to bounds if enabled
        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minBounds.y, maxBounds.y);
        }

        transform.position = smoothPosition;
    }
}
