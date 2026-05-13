using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
    }

    public void ResetPostion()
    {
        this.gameObject.transform.SetPositionAndRotation(startPosition, startRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ResetPostion();
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
