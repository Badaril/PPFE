using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BlocNote : MonoBehaviour
{
    public Page[] pages;
    private GameObject[] listOfPictures;
    public int actualIndex;

    private void Update()
    {
        //Debug.Log("actual index : " + actualIndex);
    }

    public void TurnPage(int pageIndex)
    {
        actualIndex = pageIndex;

        //turn right to left
        if (Vector3.Dot(transform.forward, pages[actualIndex].gameObject.transform.forward) >= 0.98f)
        {
            pages[actualIndex].SetSocket(true);
            if (pages[actualIndex].nextPage != -1)
            {
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = true;
                pages[pages[actualIndex].nextPage].visual.SetActive(true);

                pages[pages[actualIndex].nextPage].SetSocket(true);

                
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = true;
                pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = true;

            }
        }

        //turn left to right
        else
        {
            if (pages[actualIndex].previousPage == 0)
            {
                pages[pages[actualIndex].previousPage].SetSocket(true);
            }

            else if (pages[actualIndex].previousPage > 0)
            {
                pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = true;
                pages[pages[actualIndex].previousPage].visual.SetActive(true);

                    pages[pages[actualIndex].previousPage].SetSocket(true);

                
                pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = true;
                pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void UnsetPage()
    {
        if (Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up) <= 0)
        {
            pages[actualIndex].SetSocket(true);

            if (pages[actualIndex].previousPage == 0)
            {
                pages[pages[actualIndex].previousPage].SetSocket(false);
            }

            else if (pages[actualIndex].previousPage > 0)
            {
                pages[pages[actualIndex].previousPage].GetComponent<MeshRenderer>().enabled = false;

                pages[pages[actualIndex].previousPage].SetSocket(false);

                pages[pages[actualIndex].previousPage].visual.SetActive(false);
                pages[pages[actualIndex].previousPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].previousPage].GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            pages[actualIndex].SetSocket(false);
            if (pages[actualIndex].nextPage != -1)
            {
                pages[pages[actualIndex].nextPage].GetComponent<MeshRenderer>().enabled = false;

                pages[pages[actualIndex].nextPage].SetSocket(false);

                pages[pages[actualIndex].nextPage].visual.SetActive(false);
                pages[pages[actualIndex].nextPage].GetComponent<XRGrabInteractable>().enabled = false;
                pages[pages[actualIndex].nextPage].GetComponent<BoxCollider>().enabled = false;
                actualIndex = pages[actualIndex].previousPage;
            }
        }
    }
}
