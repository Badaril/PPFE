using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject[] animalList;
    public GameObject[] environnmentSoundsList;
    [SerializeField] private BlocNote blocnoteRef;

    public void TurnOn()
    {
        for (int i = 0; i < environnmentSoundsList.Length; i++)
        {
            environnmentSoundsList[i].GetComponent<AudioSource>().volume /= 5f;
        }
        animalList[blocnoteRef.actualIndex].GetComponent<AudioSource>().enabled = true;
    }

    public void TurnOff() 
    {
        for (int i = 0; i < environnmentSoundsList.Length; i++)
        {
            environnmentSoundsList[i].gameObject.GetComponent<AudioSource>().volume *= 5f;
        }
        animalList[blocnoteRef.actualIndex].GetComponent<AudioSource>().enabled = false;
    }
}
