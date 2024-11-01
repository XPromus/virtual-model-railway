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

    private void Start() {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(Selected);
        interactable.selectExited.AddListener(Deselected);
    }

    // private void Update()
    // {
    //     var inputDevices = new List<UnityEngine.XR.InputDevice>();
    //     UnityEngine.XR.InputDevices.GetDevices(inputDevices);
    //
    //     foreach (var device in inputDevices)
    //     {
    //         Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
    //     }
    // }

    private void FixedUpdate() {
        if (interactorTransform != null) {
            float angle = Vector3.SignedAngle(interactorTransform.position - transform.position, interactorTransform.forward, Vector3.up);
            float delta = angle - lastFrameAngle;
            transform.Rotate(transform.up, delta*multiplier);           
            lastFrameAngle = angle;
        }
    }

    public void Selected(SelectEnterEventArgs arguments) {        
        interactorTransform = arguments.interactorObject.transform;
        lastFrameAngle = Vector3.SignedAngle(interactorTransform.position - transform.position, interactorTransform.forward, Vector3.up);   
        Debug.Log("selected");
    }

    public void Deselected(SelectExitEventArgs arguments) {        
        interactorTransform = null;        
    }
}