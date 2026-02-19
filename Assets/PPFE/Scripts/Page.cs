using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Page : MonoBehaviour
{
    public GameObject pictureSocket;
    public GameObject picture = null;
    public TypeOfAnimal animalNeeded;
    public Texture map;
    public TextMeshPro title;
    public TextMeshPro descriptionText;
    public int pageNumber;
    public int nextPage;
    public int previousPage;

    public void CheckPicture()
    {
        var photo = pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject;

        if (photo.GetComponent<Photo>().animalInPicture == animalNeeded)
        {
            photo.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Locked");
        }
    }

    public void SetText(string titleText, string text)
    {
        title.text = titleText;
        descriptionText.text = text;
    }

    public void SetMap(Texture mapTexture)
    {
        map = mapTexture;
    }

    public void SetPicture(GameObject nextPicture)
    {
        picture = nextPicture;
    }

    public void SetAnimalType(TypeOfAnimal nextAnimal)
    {
        animalNeeded = nextAnimal;
    }

    public bool CheckPictureSocket()
    {
        return pictureSocket.GetComponent<XRSocketInteractor>().firstInteractableSelected.transform.gameObject != null;
    }
}
