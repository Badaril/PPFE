using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.InputSystem.XR;

public class Blocknote : MonoBehaviour
{

    public GameObject rightHandDevice;
    public GameObject attachPoint;
    private InputDevice leftHandDevice;
    private bool deviceInitialized = false;

    public void TurnPage()
    {
        rightHandDevice.transform.position = attachPoint.transform.position;
    }

}
