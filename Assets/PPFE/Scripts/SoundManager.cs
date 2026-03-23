using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public GameObject[] animalList;
    public GameObject[] environnmentSoundsList;
    [SerializeField] private BlocNote blocnoteRef;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private int actualAnimalIndex;
    public bool isAwake;

    //public AudioSource audioSource;
    public int sampleSize = 128;
    public float width = 5f;
    public float height = 2f;
    public float smoothing = 10f;

    private LineRenderer line;
    private float[] samples;
    private float[] displayed;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        samples = new float[sampleSize];
        displayed = new float[sampleSize];

        line.positionCount = sampleSize;
        line.useWorldSpace = false;
    }

    void Update()
    {
        if (animalList[actualAnimalIndex].GetComponent<AudioSource>() == null) return;

        animalList[actualAnimalIndex].GetComponent<AudioSource>().GetOutputData(samples, 0);

        for (int i = 0; i < sampleSize; i++)
        {
            displayed[i] = Mathf.Lerp(displayed[i], samples[i], Time.deltaTime * smoothing);

            float x = (i / (float)(sampleSize - 1)) * width;
            float y = displayed[i] * height;

            line.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

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
        actualAnimalIndex = blocnoteRef.actualIndex;
        isAwake = true;
        for (int i = 0; i < environnmentSoundsList.Length; i++)
        {
            environnmentSoundsList[i].GetComponent<AudioSource>().volume /= 5f;
        }
        
        animalList[actualAnimalIndex].GetComponent<AudioSource>().enabled = true;
    }

    public void TurnOff() 
    {
        for (int i = 0; i < environnmentSoundsList.Length; i++)
        {
            environnmentSoundsList[i].gameObject.GetComponent<AudioSource>().volume *= 5f;
        }

        animalList[blocnoteRef.actualIndex].GetComponent<AudioSource>().enabled = false;
        isAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ResetPostion();
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void ChangeVolume()
    {
        animalList[actualAnimalIndex].GetComponent<AudioSource>().enabled = false;
        animalList[blocnoteRef.actualIndex].GetComponent<AudioSource>().enabled = true;
        actualAnimalIndex = blocnoteRef.actualIndex;
    }
}
