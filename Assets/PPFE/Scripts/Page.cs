using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Page : MonoBehaviour
{
    public GameObject pictureSocket;
    public GameObject picture;
    public TypeOfAnimal animalNeeded;
    [SerializeField] private BlocNote blocNote;
    public GameObject visual;
    public int pageNumber;
    public int nextPage;
    public int previousPage;
    public XRInteractionManager interactManager;

    public void CheckPicture()
    {
        picture = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;
        picture.transform.SetParent(pictureSocket.transform, true);
        Debug.Log(picture);

        if (picture.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            picture.GetComponent<Rigidbody>().isKinematic = true;
            picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
            GetComponent<AudioSource>().Play();
            //pictureSocket.GetComponent<XRSocketInteractor>().enabled = false;
        }
    }

    public bool CheckSocket()
    {
        return pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject != null;
    }

    public void SetSocket()
    {
        picture.SetActive(true);
        interactManager.SelectEnter(pictureSocket.GetComponent<XRSocketInteractor>(), picture.GetComponent<IXRSelectInteractable>());

    }

    public void UnsetSocket()
    {
        picture.SetActive(false);
    }

    public void UpdateBlocnote()
    {
        blocNote.TurnPage(pageNumber);
    }


}
