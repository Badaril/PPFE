using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BlocNote : MonoBehaviour
{
    public Page[] pages;
    private GameObject[] listOfPictures;
    public int actualIndex;

    [SerializeField] private SoundManager talkieWalkie;

    private void Update()
    {
        //Debug.Log("actual index : " + actualIndex + " / " + Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up));
        /*for (int i = 1; i < pages.Length; i++)
        {
            if (pages[i].gameObject.GetComponent<Rigidbody>().linearVelocity != new Vector3(0, 0, 0) &
            pages[i].gameObject.GetComponent<Rigidbody>().angularVelocity != new Vector3(0, 0, 0))
            {
                return;
            }

        }*/
    }

    public void TurnPage(int pageIndex)
    {
        actualIndex = pageIndex;

        //turn right to left
        if (Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up) >= 0.98f)
        {
            Debug.Log("turn right to left");
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
            Debug.Log("tourne de gauche à droite");
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
            Debug.Log("inferieur à 0");
            pages[actualIndex].SetSocket(true);

            if (this.GetComponent<TutoChecker>() != null)
            {
                
                this.gameObject.GetComponent<TutoChecker>().UpdateTutoState();
            }

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
            Debug.Log("sup 0");
            pages[actualIndex].SetSocket(false);
            if (pages[actualIndex].nextPage != -1 & actualIndex != 0)
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

    public void FixBlocNote()
    {
        if (Vector3.Dot(transform.up, pages[actualIndex].gameObject.transform.up) <= -0.98f)
        {
            for (int i = 1; i < pages.Length; i++)
            {
                if (pages[actualIndex].nextPage == i)
                {
                    pages[i].GetComponent<MeshRenderer>().enabled = true;
                    pages[i].visual.SetActive(true);

                    pages[i].SetSocket(true);


                    pages[i].GetComponent<XRGrabInteractable>().enabled = true;
                    pages[i].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    pages[i].GetComponent<MeshRenderer>().enabled = false;
                    pages[i].visual.SetActive(false);

                    pages[i].SetSocket(false);


                    pages[i].GetComponent<XRGrabInteractable>().enabled = false;
                    pages[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
            pages[actualIndex].GetComponent<MeshRenderer>().enabled = true;
            pages[actualIndex].visual.SetActive(true);

            pages[actualIndex].SetSocket(true);

            pages[actualIndex].GetComponent<XRGrabInteractable>().enabled = true;
            pages[actualIndex].GetComponent<BoxCollider>().enabled = true;

            if (talkieWalkie.isAwake)
            {
                talkieWalkie.ChangeVolume();
            }
        }
        else 
        {
            for (int i = 1; i < pages.Length; i++)
            {
                if (pages[actualIndex].previousPage == i)
                {
                    pages[i].GetComponent<MeshRenderer>().enabled = true;
                    pages[i].visual.SetActive(true);

                    pages[i].SetSocket(true);


                    pages[i].GetComponent<XRGrabInteractable>().enabled = true;
                    pages[i].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    pages[i].GetComponent<MeshRenderer>().enabled = false;
                    pages[i].visual.SetActive(false);

                    pages[i].SetSocket(false);


                    pages[i].GetComponent<XRGrabInteractable>().enabled = false;
                    pages[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
            pages[actualIndex].GetComponent<MeshRenderer>().enabled = true;
            pages[actualIndex].visual.SetActive(true);

            pages[actualIndex].SetSocket(true);

            pages[actualIndex].GetComponent<XRGrabInteractable>().enabled = true;
            pages[actualIndex].GetComponent<BoxCollider>().enabled = true;
            actualIndex = pages[actualIndex].previousPage;

            if (talkieWalkie.isAwake)
            {
                talkieWalkie.ChangeVolume();
            }
        }
    }
}
