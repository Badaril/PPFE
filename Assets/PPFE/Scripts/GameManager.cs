using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GameManager : MonoBehaviour
{
    [Header("InGame assets")]
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject Polaroid;
    [SerializeField] private GameObject Animals;
    [SerializeField] private GameObject Accessories;
    [SerializeField] private GameObject RedButton;
    [SerializeField] private GameObject SphereRoom;
    [SerializeField] private GameObject RestOfRoom;

    [Header("Tuto assets")]
    [SerializeField] private HUDTextDatas TutoHUDTextDatas;
    [SerializeField] private TMP_Text TextTuto;
    [SerializeField] private GameObject CanvaRef;
    [SerializeField] private GameObject TutoBat;
    [SerializeField] private GameObject TutoSnail;
    [SerializeField] private GameObject TutoCamera;
    [SerializeField] private GameObject TutoBlocNote;
    [SerializeField] private GameObject TutoWalkieTalkie;
    [SerializeField] private GameObject TutoRedButton;

    public float timer;
    public bool startTimer;
    private Color sphereColor;
    private TextRow currentTutoRow;
    public int currentTutoRowIndex;

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

            if (timer > 30f)
            {
                EndGame();
                timer = 0;
                startTimer = false;
            }
        }
        /*Debug.Log(currentTutoRowIndex + " index");
        Debug.Log(currentTutoRow + " row");*/

    }

    public void StartGame()
    {
        TutoCamera.SetActive(false);
        TutoBat.SetActive(false);
        TutoBlocNote.SetActive(false);
        TutoSnail.SetActive(false);
        TutoWalkieTalkie.SetActive(false);
        TextTuto.text = currentTutoRow.text;

        Sounds.SetActive(true);
        StartCoroutine(ActivateProps());
        Polaroid.SetActive(true);
        Animals.SetActive(true);
        Accessories.SetActive(true);

        // marche pas comme je le souhaite
        /*SphereRoom.GetComponent<MeshRenderer>().material.SetFloat("_Surface", 1);
        SphereRoom.GetComponent<MeshRenderer>().material.SetOverrideTag("RenderType", "Transparent");
        SphereRoom.GetComponent<MeshRenderer>().material.renderQueue =
            (int)UnityEngine.Rendering.RenderQueue.Transparent;*/
        SphereRoom.GetComponent<MeshRenderer>().material.SetOverrideTag("RenderType", "Transparent");
        RedButton.SetActive(false);
        RestOfRoom.SetActive(false);
        CanvaRef.SetActive(false);


        StartCoroutine(OpenTransition(4f));
    }

    public void EndGame()
    {
        // marche pas comme je le souhaite
        /*SphereRoom.GetComponent<MeshRenderer>().material.SetFloat("_Surface", 0);
        SphereRoom.GetComponent<MeshRenderer>().material.SetOverrideTag("RenderType", "Opaque");
        SphereRoom.GetComponent<MeshRenderer>().material.renderQueue =
            (int)UnityEngine.Rendering.RenderQueue.Geometry;*/

        SphereRoom.GetComponent<MeshRenderer>().material.SetOverrideTag("RenderType", "Opaque");
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
            if (elapsedTime > 1)
            {
                sphereColor.a -= 0.5f * Time.deltaTime;
                Mathf.Clamp(sphereColor.a, 0f, 1f);
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
                sphereColor.a += 0.5f * Time.deltaTime;
                Mathf.Clamp(sphereColor.a, 0f, 1f);
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

            
            /*if (currentTutoRow.conditionEnabled) 
            {
                RedButton.SetActive(false);
                TutoCamera.SetActive(true);
                TutoBat.SetActive(true);
                TutoBlocNote.SetActive(true);
                TutoSnail.SetActive(true);
                TutoWalkieTalkie.SetActive(true);
            }*/
            TextTuto.text = currentTutoRow.text;
            currentTutoRowIndex = currentTutoRow.nextRowIndex;
            currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];

            switch (currentTutoRowIndex)
            {
                case 4:
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(true);
                break;

                case 5:
                    TutoBat.SetActive(true);
                break;
                
                case 6:
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(false);
                    //TutoBat.SetActive(false);
                    TutoCamera.SetActive(true);
                    TutoRedButton.gameObject.SetActive(false);
                    break;

                case 7:
                    TutoBlocNote.gameObject.SetActive(true);
                    break;

                case 10:
                    TutoBat.SetActive(false);
                    TutoSnail.SetActive(true);
                    RedButton.SetActive(true);
                    break;
            }
        }
        else
        {
            

            TutoCamera.SetActive(false);
            TutoBat.SetActive(false);
            TutoBlocNote.SetActive(false);
            TutoSnail.SetActive(false);
            TutoWalkieTalkie.SetActive(false);
            TextTuto.text = currentTutoRow.text;
            StartGame();
        }
    }

    public void CheckTutoState()
    {
        switch (currentTutoRowIndex)
        {
            case 3:
                //play sound
                UpdateTutoText();
                break;
        }
    }

    public void EnabledRedButton(bool value)
    {
        TutoRedButton.gameObject.SetActive(value);
    }
}
