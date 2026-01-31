using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float smoothTime = 0.2f;
    public Vector2 offset = Vector2.zero;

    [Header("Bounds (Optional)")]
    public bool useBounds = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // Shake variables
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0f;
    private float shakeFrequency = 25f;
    private float shakeTimer = 0f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Base target position
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        // Apply shake if active
        if (shakeTimer > 0)
        {
            float x = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f) * 2f;

            Vector3 shakeOffset = new Vector3(x, y, 0f) * shakeMagnitude * (shakeTimer / shakeDuration);
            smoothPosition += shakeOffset;

            shakeTimer -= Time.deltaTime;
        }

        // Clamp to bounds
        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minBounds.y, maxBounds.y);
        }

        transform.position = smoothPosition;

        // debug
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shake(0.2f, 0.3f, 25f);
        }
    }

    // Call this when an impact happens
    public void Shake(float duration, float magnitude, float frequency = 25f)
    {
        shakeDuration = duration;
        shakeTimer = duration;
        shakeMagnitude = magnitude;
        shakeFrequency = frequency;
    }
}
