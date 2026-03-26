using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject Polaroid;
    [SerializeField] private GameObject Animals;
    [SerializeField] private GameObject Accessories;
    [SerializeField] private GameObject RedButton;
    [SerializeField] private GameObject SphereRoom;
    [SerializeField] private GameObject RestOfRoom;
    [SerializeField] private HUDTextDatas TutoHUDTextDatas;
    [SerializeField] private TMP_Text TextTuto;

    public float timer;
    public bool startTimer;
    private Color sphereColor;
    private TextRow currentTutoRow;
    private int currentTutoRowIndex;

    private void Start()
    {
        Sounds.SetActive(false);
        Polaroid.SetActive(false);
        Animals.SetActive(false);
        Accessories.SetActive(false);
        sphereColor = SphereRoom.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
        UpdateTutoText();
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer > 300f)
            {
                EndGame();
                timer = 0;
                startTimer = false;
            }
        }
    }

    public void StartGame()
    {
        Sounds.SetActive(true);
        StartCoroutine(ActivateProps());
        Polaroid.SetActive(true);
        Animals.SetActive(true);
        Accessories.SetActive(true);

        RedButton.SetActive(false);
        RestOfRoom.SetActive(false);
        TextTuto.text = "";

        StartCoroutine(OpenTransition(4f));
    }

    public void EndGame()
    {
        StartCoroutine(CloseTransition(4f));
        
    }

    public IEnumerator ActivateProps()
    {
        for (int i = 0; i < Sounds.transform.childCount; i++)
        {
            new WaitForSeconds(0.5f);
            Sounds.transform.GetChild(i).GetComponentInChildren<AudioSource>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator OpenTransition(float seconds)
    {
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            SphereRoom.transform.localScale += new Vector3(1,1,1) * Time.deltaTime * 50f;
            if (elapsedTime > 3)
            {
                sphereColor.a -= Time.deltaTime;
                SphereRoom.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);
            }
            Sounds.transform.GetChild(0).GetComponentInChildren<AudioSource>().volume += Time.deltaTime * 0.2f;
            Mathf.Clamp(Sounds.transform.GetChild(0).GetComponentInChildren<AudioSource>().volume, 0, 1f);
            Sounds.transform.GetChild(1).GetComponentInChildren<AudioSource>().volume += Time.deltaTime * 0.2f;
            Mathf.Clamp(Sounds.transform.GetChild(1).GetComponentInChildren<AudioSource>().volume, 0, 1f);
            Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume += Time.deltaTime * 0.01f;
            Mathf.Clamp(Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume, 0, 0.05f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        SphereRoom.SetActive(false);
        Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume = 0.05f;
        startTimer = true;
    }

    public IEnumerator CloseTransition(float seconds)
    {
        SphereRoom.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            SphereRoom.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 50f;
            if (elapsedTime < 1)
            {
                sphereColor.a += Time.deltaTime;
                SphereRoom.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);
            }
            Sounds.transform.GetChild(0).GetComponentInChildren<AudioSource>().volume -= Time.deltaTime * 0.2f;
            Mathf.Clamp(Sounds.transform.GetChild(0).GetComponentInChildren<AudioSource>().volume, 0, 1f);
            Sounds.transform.GetChild(1).GetComponentInChildren<AudioSource>().volume -= Time.deltaTime * 0.2f;
            Mathf.Clamp(Sounds.transform.GetChild(1).GetComponentInChildren<AudioSource>().volume, 0, 1f);
            Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume -= Time.deltaTime * 0.01f;
            Mathf.Clamp(Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume, 0, 0.05f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        sphereColor.a = 1;
        SphereRoom.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);
        SceneManager.LoadScene("Default_Scene");
    }

    public void UpdateTutoText()
    {
        if (!currentTutoRow.IsFinished)
        {
            TextTuto.text = currentTutoRow.text;
            currentTutoRowIndex = currentTutoRow.nextRowIndex;
            currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
        }
        else
        {
            TextTuto.text = currentTutoRow.text;
        }
    }
}
