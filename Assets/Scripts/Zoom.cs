using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Zoom : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public float zoomMultiplier = 1f;
    
    private InputDevice leftHandController;
    private InputDevice rightHandController;
    private List<InputDevice> devices = new List<InputDevice>();
    
    private Vector3 leftHandPosition;
    private Vector3 rightHandPosition;

    private float fovAngle = 1f;
    private float oldDistance;
    private float oldFov;
    private bool holding = false;
    
    private void Start() {
        
    }
    
    private void Update()
    {
        if (leftHandController.characteristics == InputDeviceCharacteristics.None || rightHandController.characteristics == InputDeviceCharacteristics.None)
        {
            var leftHandCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left;
            var leftHandedControllers = new List<InputDevice>();
            var rightHandCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right;
            var rightHandedControllers = new List<InputDevice>();
            
            InputDevices.GetDevices(devices);

            InputDevices.GetDevicesWithCharacteristics(leftHandCharacteristics, leftHandedControllers);
            InputDevices.GetDevicesWithCharacteristics(rightHandCharacteristics, rightHandedControllers);

            if (leftHandedControllers.Any() && rightHandedControllers.Any())
            {
                leftHandController = leftHandedControllers.First();
                rightHandController = rightHandedControllers.First();
            }
        }
        else
        {
            bool triggerRight;
            if (rightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerRight) && triggerRight)
            {
                leftHandPosition = leftController.transform.position;
            }
            else
            {
                holding = false;
            }
            
            bool triggerLeft;
            if (leftHandController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerLeft) && triggerLeft)
            {
                rightHandPosition = rightController.transform.position;
            }
            else
            {
                holding = false;
            }

            if (triggerLeft && triggerRight)
            {
                float distance = Vector3.Distance(leftHandPosition, rightHandPosition);
                
                if (!holding)
                {
                    holding = true;
                    oldDistance = distance;
                    oldFov = fovAngle;
                }
                
                float difference = distance - oldDistance;

                float newFov = oldFov + Mathf.Round(difference * 100f) / 100f * zoomMultiplier;
                fovAngle = Mathf.Clamp(newFov, 1f, 10f);
                //Debug.Log(fovAngle);
                XRDevice.fovZoomFactor = fovAngle;
            }
        }
    }
}