using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class PointOfInterestMain : MonoBehaviour
{
    public GameObject xrOrigin;
    public float scale = 0.05f;
    
    private bool _inPointOfInterest = false;

    public void SetInPointOfInterest(bool inPointOfInterest)
    {
        _inPointOfInterest = inPointOfInterest;
    }

    public bool GetInPointOfInterest()
    {
        return _inPointOfInterest;
    }

    public float GetScale()
    {
        return scale;
    }

    public GameObject GetXrOrigin()
    {
        return xrOrigin;
    }
}