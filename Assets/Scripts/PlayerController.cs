using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")] 
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("How fast player moves horizontally")] [SerializeField] float xRange = 10f;
    [Tooltip("How fast player moves vertically")] [SerializeField] float yRange = 10f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [Header("Player input based tuning")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    float xThrow;
    float yThrow;

    ParticleSystem ps;

    void Start()   
    {
        ps = GetComponent<ParticleSystem>();    
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
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

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring() 
    {
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            SetLasersActive(true);
        }
        else 
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        // for each of the lasers that we have, activate/deactivate them
        foreach (GameObject laser in lasers) 
        {
            var emmisionModule = laser.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isActive;
        }
    }
}
