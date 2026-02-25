using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject[] animalList;
    public GameObject[] environnmentSoundsList;
    [SerializeField] private BlocNote blocnoteRef;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
    }

    public void ResetPostion()
    {
        this.gameObject.transform.SetPositionAndRotation(startPosition, startRotation);
    }

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
