using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TableInteraction : MonoBehaviour
{
    public Transform interactorTransform;
    private float lastFrameAngle;
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
            // float angle = Vector3.SignedAngle(interactorTransform.position - transform.position, interactorTransform.forward, Vector3.up);
            // float delta = angle - lastFrameAngle;
            // transform.Rotate(transform.up, delta*multiplier);           
            // lastFrameAngle = angle;
            
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

    public void Selected(SelectEnterEventArgs arguments) {        
        interactorTransform = arguments.interactorObject.transform;
        lastFrameAngle = Vector3.SignedAngle(interactorTransform.position - transform.position, interactorTransform.forward, Vector3.up);   
        lastPosition = interactorTransform.position;
        Debug.Log("selected");
    }

    public void Deselected(SelectExitEventArgs arguments) {        
        interactorTransform = null;        
    }
}