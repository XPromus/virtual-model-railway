using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TableInteraction : MonoBehaviour
{
    public Transform interactorTransform;
    public float multiplier = 1;
    
    private Vector3 lastPosition;
    private Vector3 centerTable;

    private void Start() {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(Selected);
        interactable.selectExited.AddListener(Deselected);
        
        centerTable = GetComponent<Renderer>().bounds.center;
    }

    private void FixedUpdate() {
        if (interactorTransform != null) {
            Vector3 interactorPosition = interactorTransform.position;
            Vector3 vecA = centerTable - lastPosition;
            Vector3 vecB = centerTable - interactorPosition;
            float delta = Vector3.Angle(vecA, vecB);
            float cross = Vector3.Cross(vecA, vecB).y;
            float angle = delta;
            if (cross < 0)
            {
                angle *= -1;
            }
            transform.Rotate(transform.up, angle * multiplier);
            lastPosition = interactorPosition;
        }
    }

    private void Selected(SelectEnterEventArgs arguments) {        
        interactorTransform = arguments.interactorObject.transform; 
        lastPosition = interactorTransform.position;
        Debug.Log("selected");
    }

    private void Deselected(SelectExitEventArgs arguments) {        
        interactorTransform = null;        
    }
}