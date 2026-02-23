using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BlocNote : MonoBehaviour
{
    public Page[] pages;
    public GameObject[] listOfPictures;
    public int actualIndex;

    private void Update()
    {
        //Debug.Log("actual index : " + actualIndex);
    }

    public void TurnPage(int pageIndex)
    {
        actualIndex = pageIndex;
        if (Vector3.Dot(transform.forward, pages[actualIndex].gameObject.transform.forward) >= 0.98f)
        {
            if (pages[actualIndex].nextPage != -1)
            {
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = true;
                pages[pages[actualIndex].nextPage].visual.SetActive(true);
                if (pages[pages[actualIndex].nextPage].CheckSocket())
                {
                    pages[pages[actualIndex].nextPage].SetSocket(true);
                }
                
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = true;
                /* jsp pourquoi cest la*/ pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else
        {
            if (pages[actualIndex].previousPage > 0)
            {
                pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = true;
                pages[pages[actualIndex].previousPage].visual.SetActive(true);
                if (pages[pages[actualIndex].previousPage].CheckSocket())
                {
                    pages[pages[actualIndex].previousPage].SetSocket(true);
                }
                
                pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = true;
                pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void UnsetPage()
    {
        if (Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up) <= 0)
        {
            if (pages[actualIndex].previousPage > 0)
            {
                pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = false;
                if (pages[pages[actualIndex].previousPage].CheckSocket())
                {
                    pages[pages[actualIndex].previousPage].SetSocket(false);
                }
                pages[pages[actualIndex].previousPage].visual.SetActive(false);
                pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = false;
                
            }
        }
        else
        {
            if (pages[actualIndex].nextPage != -1)
            {
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = false;
                if (pages[pages[actualIndex].nextPage].CheckSocket())
                {
                    pages[pages[actualIndex].nextPage].SetSocket(false);
                }
                pages[pages[actualIndex].nextPage].visual.SetActive(false);
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = false;
                actualIndex = pages[actualIndex].previousPage;
            }
        }
    }
}
