using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 1f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;
    [SerializeField] float postitionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float PositionYawFactor = -2f;
    [SerializeField] float controlRollFactor = -5f;
    float xThrow;
    float yThrow;
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * postitionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * PositionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(-pitch,yaw,roll);
    }
    void ProcessTranslation()
    {
         xThrow = Input.GetAxis("Horizontal");

         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;

        float yOffset = yThrow * Time.deltaTime * controlSpeed;


        float rawXPos = transform.localPosition.x + xOffset;

        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXpos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYpos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);
    }
}
