using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject[] animalList;
    private float[] defaultVolumeAnimals;
    public GameObject[] environnmentSoundsList;
    private float[] defaultVolumeEnvironnement;
    [SerializeField] private BlocNote blocnoteRef;

    /*private void Start()
    {
        for (int i = 0; i < animalList.Length; i++)
        {
            defaultVolumeAnimals[i] = animalList[i].GetComponent<AudioSource>().volume;
        }

        for (int i = 0; i < environnmentSoundsList.Length; i++)
        {
            defaultVolumeEnvironnement[i] = environnmentSoundsList[i].GetComponent<AudioSource>().volume;
        }
    }*/

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
