using Oculus.Platform;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private Collider triggerZone;
    private void Awake()
    {
        triggerZone = GetComponentInChildren<Collider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OnTriggerEnter();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("je te rentre dedans");
        bool var = other.GetComponent<Photo>();
        if (var & other != null)
        {
            Debug.Log("efdsuyfdyujweyufgyufguy");
            other.gameObject.GetComponent<Photo>().DisablePhysics();
        }
    }
}
