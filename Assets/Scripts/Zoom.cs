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
    
    private InputDevice _left;
    private InputDevice _right;
    
    private Vector3 _leftHandPosition;
    private Vector3 _rightHandPosition;

    private float _fovAngle = 1f;
    private float _oldDistance;
    private float _oldFov;
    private bool _holding = false;
    
    private void Update()
    {
        
        if (_right.characteristics == InputDeviceCharacteristics.None ||
            _left.characteristics == InputDeviceCharacteristics.None)
        {
            Debug.Log("Setting up devices");
            SetUpInputDevices();
            return;
        }
        
        bool triggerRight;
        if (_right.TryGetFeatureValue(CommonUsages.triggerButton, out triggerRight) && triggerRight)
        {
            _leftHandPosition = leftController.transform.position;
        }
        else
        {
            _holding = false;
        }
        
        bool triggerLeft;
        if (_left.TryGetFeatureValue(CommonUsages.triggerButton, out triggerLeft) && triggerLeft)
        {
            _rightHandPosition = rightController.transform.position;
        }
        else
        {
            _holding = false;
        }

        if (triggerLeft && triggerRight)
        {
            float distance = Vector3.Distance(_leftHandPosition, _rightHandPosition);
            
            if (!_holding)
            {
                _holding = true;
                _oldDistance = distance;
                _oldFov = _fovAngle;
            }
            
            float difference = distance - _oldDistance;

            float newFov = _oldFov + Mathf.Round(difference * 100f) / 100f * zoomMultiplier;
            _fovAngle = Mathf.Clamp(newFov, 1f, 10f);
            XRDevice.fovZoomFactor = _fovAngle;
        }
        
    }
    
    private void SetUpInputDevices()
    {
        if (_left.characteristics == InputDeviceCharacteristics.None || _right.characteristics == InputDeviceCharacteristics.None)
        {
            var leftHandCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left;
            var leftHandedControllers = new List<InputDevice>();
            var rightHandCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right;
            var rightHandedControllers = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(leftHandCharacteristics, leftHandedControllers);
            InputDevices.GetDevicesWithCharacteristics(rightHandCharacteristics, rightHandedControllers);

            if (leftHandedControllers.Any() && rightHandedControllers.Any())
            {
                _left = leftHandedControllers.First();
                _right = rightHandedControllers.First();
            }
        }
    }
}