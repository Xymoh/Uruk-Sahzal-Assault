using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float controlSpeed = 30f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 10f;

    // Pitch (Up & Down) rotation
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -15f;

    // Yaw (side) rotation
    [SerializeField] private float positionYawFactor = 2f;

    // Roll rotation
    [SerializeField] private float controlRollFactor = -20f;

    private float xThrow;
    private float yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        // Getting input axis for hor and ver directions
        // calculating movement by time.deltatime
        // clamping it within given range on x and y axis
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawX = transform.localPosition.x + xOffset;
        float moveX = Mathf.Clamp(rawX, -xRange, xRange);

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawY = transform.localPosition.y + yOffset;
        float moveY = Mathf.Clamp(rawY, -yRange, yRange);

        transform.localPosition = new Vector3(moveX, moveY, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
