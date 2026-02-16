using System.Collections;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Page : MonoBehaviour
{
    public GameObject pictureSocket;
    public TypeOfAnimal animalNeeded;
    public HingeJoint joint;

    [System.Obsolete]
    private void LateUpdate()
    {

        if (!joint.gameObject.active)
        {
            joint.gameObject.SetActive(true);
        }
    }

    public void CheckPicture()
    {
        var photo = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;

        if (photo.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            Debug.Log(photo.GetComponent<XRGrabInteractable>().interactionLayers + "avant");
            photo.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
            Debug.Log(photo.GetComponent<XRGrabInteractable>().interactionLayers + "apres");
            /*photo.GetComponent<Rigidbody>().velocity = Vector3.zero;
            photo.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            photo.GetComponent<Rigidbody>().useGravity = false;
            photo.GetComponent<Rigidbody>().isKinematic = true;*/
        }
    }
}
