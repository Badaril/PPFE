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

    /*private void Update()
    {
        Debug.Log(picture + this.gameObject.ToString());
    }*/

    public void CheckPicture()
    {
        Debug.Log("je chech " + this.gameObject.ToString());
        picture = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;
        picture.transform.SetParent(this.gameObject.transform, false);
        //picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("InSocket");
        Debug.Log(picture);

        if (picture.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            
            picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
            GetComponent<AudioSource>().Play();
        }
    }

    public bool CheckSocket()
    {
        if (picture != null)
        {
            Debug.Log("le check est true");
            return true; //pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject != null;
        }
        Debug.Log("le check est false");
        return false;
    }

    public void SetSocket(bool display)
    {
        Debug.Log("je set la socket en " + display);
        picture.SetActive(display);
        /*if (display)
        {
            picture.SetActive(true);
            interactManager.SelectEnter(pictureSocket.GetComponent<XRSocketInteractor>(), picture.GetComponent<IXRSelectInteractable>());
        }
        else
        {
            picture.SetActive(false);
            interactManager.SelectExit(pictureSocket.GetComponent<XRSocketInteractor>(), picture.GetComponent<IXRSelectInteractable>());
        }*/

    }

    public void UnsetSocket()
    {
        if (picture != null)
        {
            if (picture.GetComponent<XRGrabInteractable>().interactionLayers.ToString() != "Locked")
            {
                picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Photo");
                picture = null;
            }
        }
    }

    public void UpdateBlocnote()
    {
        blocNote.TurnPage(pageNumber);
    }
}
