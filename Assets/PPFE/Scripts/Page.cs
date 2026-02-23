using Unity.VisualScripting;
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

    private void Update()
    {
        //Debug.Log(pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers);
        if (picture != null) { Debug.Log(picture.activeSelf + this.gameObject.ToString()); }
    }

    public void CheckPicture()
    {

        Debug.Log("je chech " + this.gameObject.ToString());
        
        picture = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;

        

        //Debug.Log(picture);

        if (picture.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            Debug.Log("good");
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
        picture.transform.Find("Visual").gameObject.SetActive(display);

    }

    public void UnsetSocket()
    {
        
        if (picture != null)
        {
            picture = null;
        }
    }

    public void UpdateBlocnote()
    {
        blocNote.TurnPage(pageNumber);
    }
}
