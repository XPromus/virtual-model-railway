using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PointOfInterest : MonoBehaviour
{
    private PointOfInterestMain _poiMain;

    private List<InputDevice> _devices;
    private InputDevice _right;
    private InputDevice _left;
    private GameObject _xrOrigin;
    private float _scale;

    private bool _hovering = false;
    private bool _entered = false;
    private bool _rendering = true;
    private Vector3 _lastPosition;

    private readonly float _alphaHovering = 0.7f;
    private readonly float _alphaNotHovering = 0.1f;

    private void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(HoverEnter);
        interactable.hoverExited.AddListener(HoverExit);

        _poiMain = transform.parent.GetComponent<PointOfInterestMain>();
        _scale = _poiMain.GetScale();
        _xrOrigin = _poiMain.GetXrOrigin();
        
        SetAlpha(_alphaNotHovering);
    }

    private void Update()
    {
        if (_right.characteristics == InputDeviceCharacteristics.None ||
            _left.characteristics == InputDeviceCharacteristics.None)
        {
            Debug.Log("Setting up devices");
            SetUpInputDevices();
            return;
        }

        if (_poiMain.GetInPointOfInterest() && _rendering)
        {
            GetComponent<MeshRenderer>().enabled = false;
            _rendering = false;
        }

        if (!_poiMain.GetInPointOfInterest() && !_rendering)
        {
            GetComponent<MeshRenderer>().enabled = true;
            _rendering = true;
        }
        
        bool inPoi = _poiMain.GetInPointOfInterest();
        if (!inPoi && _hovering)
        {
            bool buttonRight;
            if (_right.TryGetFeatureValue(CommonUsages.primaryButton, out buttonRight) && buttonRight)
            {
                _entered = true;
                _poiMain.SetInPointOfInterest(true);
                Debug.Log("Move in");
                MoveIn();
            }

            bool buttonLeft;
            if (_left.TryGetFeatureValue(CommonUsages.primaryButton, out buttonLeft) && buttonLeft)
            {
                _entered = true;
                _poiMain.SetInPointOfInterest(true);
                Debug.Log("Move in");
                MoveIn();
            }
        }
        else if (_entered)
        {
            bool buttonRight;
            if (_right.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonRight) && buttonRight)
            {
                _entered = false;
                _poiMain.SetInPointOfInterest(false);
                Debug.Log("Move out");
                MoveOut();
            }

            bool buttonLeft;
            if (_left.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonLeft) && buttonLeft)
            {
                _entered = false;
                _poiMain.SetInPointOfInterest(false);
                Debug.Log("Move out");
                MoveOut();
            }
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

    private void HoverEnter(HoverEnterEventArgs args)
    {
        if (!(_entered || _poiMain.GetInPointOfInterest()))
        {
            SetAlpha(_alphaHovering);
            _hovering = true;
        }
    }

    private void HoverExit(HoverExitEventArgs args)
    {
        if (!(_entered || _poiMain.GetInPointOfInterest()))
        {
            SetAlpha(_alphaNotHovering);
            _hovering = false;
        }
    }

    private void SetAlpha(float alpha)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material mat = meshRenderer.material;
        Color col = mat.color;
        col.a = alpha;
        mat.color = col;
        meshRenderer.material = mat;
    }

    private void MoveIn()
    {
        _lastPosition = _xrOrigin.transform.position;
        _xrOrigin.transform.position = transform.position;
        _xrOrigin.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    private void MoveOut()
    {
        SetAlpha(_alphaNotHovering);
        _xrOrigin.transform.position = _lastPosition;
        _xrOrigin.transform.localScale = new Vector3(1, 1, 1);
    }
    
    
}