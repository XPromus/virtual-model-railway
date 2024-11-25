using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TableInteraction : MonoBehaviour
{
    public float multiplier = 1;
    public Direction direction = Direction.Up;
    public bool clampRotation = false;
    public float minRotation = 0;
    public float maxRotation = 300;

    private Transform _interactorTransform;
    private Vector3 _lastPosition;
    private Vector3 _centerTable;

    private void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(Selected);
        interactable.selectExited.AddListener(Deselected);

        _centerTable = GetComponent<Renderer>().bounds.center;
    }

    private void FixedUpdate()
    {
        if (_interactorTransform != null)
        {
            Vector3 interactorPosition = _interactorTransform.position;
            Vector3 vecA = _centerTable - _lastPosition;
            Vector3 vecB = _centerTable - interactorPosition;
            float delta = Vector3.Angle(vecA, vecB);
            float cross = Vector3.Cross(vecA, vecB).y;
            float angle = delta;
            Vector3 rotationAxis = GetAxis(direction);
            if (cross < 0)
            {
                angle *= -1;
            }

            if (clampRotation)
            {
                if (GetRotatability(angle * multiplier))
                    transform.Rotate(rotationAxis, angle * multiplier, Space.World);
            }
            else
            {
                transform.Rotate(rotationAxis, angle * multiplier, Space.World);
            }

            _lastPosition = interactorPosition;
        }
    }

    private void Selected(SelectEnterEventArgs arguments)
    {
        _interactorTransform = arguments.interactorObject.transform;
        _lastPosition = _interactorTransform.position;
        Debug.Log("selected");
    }

    private void Deselected(SelectExitEventArgs arguments)
    {
        _interactorTransform = null;
    }

    private Vector3 GetAxis(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right: return transform.right;
            case Direction.Left: return -transform.right;
            case Direction.Up: return transform.up;
            case Direction.Down: return -transform.up;
            case Direction.Foreward: return transform.forward;
            case Direction.Backward: return -transform.forward;
        }

        return transform.up;
    }

    private bool GetRotatability(float rotationAngle)
    {
        float currentRotation = Vector3.Scale(transform.rotation.eulerAngles, GetAxis(direction)).magnitude;
        if (currentRotation + rotationAngle < minRotation || currentRotation + rotationAngle > maxRotation)
        {
            return false;
        }

        return true;
    }
}