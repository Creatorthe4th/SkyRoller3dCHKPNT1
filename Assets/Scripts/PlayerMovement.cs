using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Speeds")]
    public float baseForwardSpeed = 8f;
    public float baseSideSpeed = 6f;

    [Header("Steering")]
    public float normalSmoothTime = 0.1f;   // responsive steering
    public float slipperySmoothTime = 0.6f;  // sluggish, "ice" steering

    Rigidbody rb;
    Vector2 moveInput;

    float currentSideInput;
    float sideVelocity;

    // Hazard / pickup effects driven by end-times so they compose cleanly
    float boostMultiplier = 1f;
    float boostUntil;

    float slowMultiplier = 1f;
    float slowUntil;

    float slipperyUntil;

    bool controllable = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // ---- Effects (called by hazard / pickup zones) ----

    // Kept compatible with your existing SpeedBoostZone.cs
    public void ActivateSpeedBoost(float boostSpeed, float duration)
    {
        boostMultiplier = boostSpeed / baseForwardSpeed;
        boostUntil = Time.time + duration;
    }

    public void ApplySlow(float multiplier, float duration)
    {
        slowMultiplier = multiplier;       // e.g. 0.4f = 40% speed
        slowUntil = Time.time + duration;
    }

    public void ApplySlippery(float duration)
    {
        slipperyUntil = Time.time + duration;
    }

    public void StopAndDisable()
    {
        controllable = false;
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }

    // ---- Input ----

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // ---- Physics ----

    void FixedUpdate()
    {
        if (!controllable)
        {
            // Freeze horizontal motion on death, let gravity finish any fall
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        float boost = Time.time < boostUntil ? boostMultiplier : 1f;
        float slow  = Time.time < slowUntil  ? slowMultiplier  : 1f;
        bool slippery = Time.time < slipperyUntil;

        float smoothTime = slippery ? slipperySmoothTime : normalSmoothTime;

        currentSideInput = Mathf.SmoothDamp(
            currentSideInput,
            moveInput.x,
            ref sideVelocity,
            smoothTime
        );

        float forwardSpeed = baseForwardSpeed * boost * slow;
        float sideSpeed = baseSideSpeed * slow;

        rb.linearVelocity = new Vector3(
            currentSideInput * sideSpeed,
            rb.linearVelocity.y,
            forwardSpeed
        );
    }
}
