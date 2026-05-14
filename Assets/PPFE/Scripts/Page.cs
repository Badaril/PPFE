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

    [SerializeField] private GameManager gameManager;

    public bool IsMoving;

    private void Update()
    {
        //Debug.Log(pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers);
        if (picture != null) { } //Debug.Log(picture.activeSelf + this.gameObject.ToString()); }

        if (pageNumber != 0)
        {
            if (this.GetComponent<Rigidbody>().linearVelocity != new Vector3(0, 0, 0) &
                this.GetComponent<Rigidbody>().angularVelocity != new Vector3(0, 0, 0) &
                this.GetComponent<XRGrabInteractable>().isSelected == false)
            {
                blocNote.actualIndex = pageNumber;
                IsMoving = true;
            }
            if (this.GetComponent<Rigidbody>().linearVelocity == new Vector3(0, 0, 0) &
                this.GetComponent<Rigidbody>().angularVelocity == new Vector3(0, 0, 0) &
                this.GetComponent<XRGrabInteractable>().isSelected == false &
                IsMoving)
            {
                blocNote.FixBlocNote();
                IsMoving = false;
            }
        }
    }
            //Debug.Log(this.gameObject.ToString() + " " + this.GetComponent<Rigidbody>().linearVelocity + " / " + this.GetComponent<Rigidbody>().angularVelocity);


    public void CheckPicture()
    {

        //Debug.Log("je chech " + this.gameObject.ToString());
        
        picture = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;

        picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("InSocket");


        //Debug.Log(picture);

        if (picture.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            //Debug.Log("good");
            picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
            GetComponent<AudioSource>().Play();
        }
    }

    private bool CheckSocket()
    {
        return picture;
    }

    public void SetSocket(bool display)
    {
        //Debug.Log("je set la socket en " + display);
        if (CheckSocket())
        {
            picture.transform.Find("Visual").gameObject.SetActive(display);
            if (!display)
            {
                picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
            }
            else
            {
                picture.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("InSocket");
            }
        }

        if (pageNumber == 0)
        {
            if (display)
            {
                //Debug.Log("premier cas page blanche");
                pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers =
                    InteractionLayerMask.GetMask("Photo", "Locked", "InSocket");
            }
            else
            {
                //Debug.Log("deuxieme cas page blanche");
                pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers =
                    InteractionLayerMask.GetMask("Locked", "InSocket");
            }
        }
        else if (display & Vector3.Dot(Vector3.up, this.gameObject.transform.up) <= 0)
        {
            //Debug.Log("photo + locked + insocket " + this.gameObject.name);
            pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers =
                InteractionLayerMask.GetMask("Photo", "Locked", "InSocket");
        }
        else
        {
            //Debug.Log("locked + insocket " + this.gameObject.name);
            pictureSocket.GetComponent<XRSocketInteractor>().interactionLayers =
                InteractionLayerMask.GetMask("Locked", "InSocket");
        }
    }

    public void UnsetSocket()
    {
        
        if (CheckSocket())
        {
            if (gameManager.startTimer)
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
