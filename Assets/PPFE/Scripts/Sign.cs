using Oculus.Platform;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private Collider triggerZone;
    private void Awake()
    {
        triggerZone = GetComponentInChildren<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("je te rentre dedans");
        bool var = other.GetComponent<Photo>();
        if (var & other != null)
        {
            //Debug.Log("efdsuyfdyujweyufgyufguy");
            other.gameObject.GetComponent<Photo>().DisablePhysics();
        }
    }
}
