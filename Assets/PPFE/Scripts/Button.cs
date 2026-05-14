using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public float deadTime = 1f;
    private bool _deadTimeActive = false;

    public GameObject button;

    public UnityEvent onPressed, onReleased;
    public Transform pressAxis;      // un empty orienté dans l'axe de pression (forward = direction)
    public float pressDistance = 0.015f; // 1.5 cm par ex
    private bool pressed;

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("trigger stay");
        if (pressed) return;

        // position du point de contact (approx)
        Vector3 handPos = other.ClosestPoint(transform.position);

        // profondeur le long de l'axe du bouton
        float depth = Vector3.Dot(handPos - pressAxis.position, pressAxis.forward);

        if (depth > pressDistance)
        {
            pressed = true;
            onPressed?.Invoke();
        }
    }

    void Awake()
    {
        var rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;  // empêche l'endormissement
    }

    void OnCollisionStay(Collision c)
    {
        var rb = GetComponent<Rigidbody>();
        if (rb.IsSleeping()) rb.WakeUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Button") && !_deadTimeActive)
        {
            onPressed?.Invoke();
            //Debug.Log("I have been pressed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            //Debug.Log("I have been released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}