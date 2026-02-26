using UnityEngine;
using UnityEngine.XR;

public class HMDPresence : MonoBehaviour
{
    private InputDevice hmd;
    private bool previousPresence = true;

    void Start()
    {
        var devices = new System.Collections.Generic.List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.Head, devices);

        if (devices.Count > 0)
            hmd = devices[0];
    }

    void Update()
    {
        if (!hmd.isValid)
            return;

        if (hmd.TryGetFeatureValue(CommonUsages.userPresence, out bool isPresent))
        {
            if (isPresent != previousPresence)
            {
                previousPresence = isPresent;

                if (!isPresent)
                    Debug.Log("Casque retiré");
                else
                    Debug.Log("Casque remis");
            }
        }
    }
}