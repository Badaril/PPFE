using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BlocNote : MonoBehaviour
{
    public Page[] pages;
    public GameObject[] listOfPictures;
    public int actualIndex;

    private void Update()
    {
        //Debug.Log(actualIndex);
        //Debug.Log(Vector3.Dot(transform.forward, pages[pages[actualIndex].previousPage].gameObject.transform.forward));
    }

    public void TurnPage(int pageIndex)
    {
        actualIndex = pageIndex;
        if (Vector3.Dot(transform.forward, pages[actualIndex].gameObject.transform.forward) >= 0.98f)
        {
            if (pages[actualIndex].nextPage != -1)
            {
                Debug.Log("suivant");
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = true;
                pages[pages[actualIndex].nextPage].visual.SetActive(true);
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = true;
                pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else /*if (Vector3.Dot(transform.forward, pages[pages[actualIndex].previousPage].gameObject.transform.forward) <= 0.98f)*/
        {
            Debug.Log("précédent");
            /*if (pages[actualIndex].previousPage != -1)
            {*/
            pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = true;
            pages[pages[actualIndex].previousPage].visual.SetActive(true);
            pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = true;
            pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = true;
            //}
        }
    }

    public void UnsetPage()
    {
        if (Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up) <= 0)
        {
            if (pages[actualIndex].previousPage != -1)
            {
                pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = false;
                pages[pages[actualIndex].previousPage].visual.SetActive(false);
                pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = false;
                actualIndex = pages[actualIndex].previousPage;
            }
        }
        else /*if (Vector3.Dot(transform.forward, pages[pages[actualIndex].previousPage].gameObject.transform.forward) <= 0)*/
        {
            if (pages[actualIndex].nextPage != -1)
            {
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = false;
                pages[pages[actualIndex].nextPage].visual.SetActive(false);
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    /*public void OnTriggerEnter(Collider other)
    {
        NextPage();
    }

    public void OnTriggerExit(Collider other)
    {
        PreviousPage();
    }*/
}
