using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BlocNote : MonoBehaviour
{
    public Page[] pages;
    public GameObject[] listOfPictures;
    public int actualIndex;

    public void NextPage()
    {
        if (pages[pages[actualIndex].nextPage] != null)
        {
            actualIndex = pages[actualIndex].nextPage;
            var newPage = pages[actualIndex];
            pages[actualIndex].SetAnimalType(newPage.animalNeeded);
            pages[actualIndex].SetMap(newPage.map);
            pages[actualIndex].SetPicture(listOfPictures[newPage.pageNumber]);
            pages[actualIndex].SetText(newPage.title.text, newPage.descriptionText.text);
        }
    }

    public void PreviousPage()
    {
        if (pages[actualIndex].previousPage >= 0)
        {
            actualIndex = pages[actualIndex].previousPage;
            var newPage = pages[actualIndex];
            pages[actualIndex].SetAnimalType(newPage.animalNeeded);
            pages[actualIndex].SetMap(newPage.map);
            pages[actualIndex].SetPicture(listOfPictures[newPage.pageNumber]);
            pages[actualIndex].SetText(newPage.title.text, newPage.descriptionText.text);
        }
    }

    public void AddPictureInBlocnote(GameObject picture)
    {
        listOfPictures[pages[actualIndex].pageNumber] = picture;
    }

    public void OnTriggerEnter(Collider other)
    {
        NextPage();
    }

    public void OnTriggerExit(Collider other)
    {
        PreviousPage();
    }
}
