using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameManager : MonoBehaviour
{
    [Header("InGame assets")]
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject Polaroid;
    [SerializeField] private GameObject Animals;
    [SerializeField] private GameObject Accessories;
    [SerializeField] private GameObject RedButton;
    [SerializeField] private GameObject SphereRoom;
    [SerializeField] private GameObject SphereRoomTranslucent;
    [SerializeField] private GameObject RestOfRoom;

    [SerializeField] private XRRayInteractor LeftController;
    [SerializeField] private XRRayInteractor RightController;

    [SerializeField] private SwitchButton ToggleButton;
    [SerializeField] private SwitchButton SkipTutoButton;

    [SerializeField] private DigitalTimer DigitalTimer;
    

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
    [SerializeField] private GameObject VideosCanvas;
    [SerializeField] private VideoPlayer VideoPlayerControls;
    [SerializeField] private VideoPlayer VideoPlayerExample;
    [SerializeField] private PlayQuickSound ValidationSound;
    [SerializeField] private VideoClip[] ListOfVideoClips;


    private GameDataManager GameDataManager;
    public GameData GameDatas;
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

        //GameDatas = ScriptableObject.CreateInstance<GameData>();
        GameDataManager = new GameDataManager();
        GameDatas = GameDataManager.LoadGameData("gameSaveFile.txt");
        ChangeControllersToggle(GameDatas.controllersToggle);

        
        ToggleButton.LateStart(!GameDatas.controllersToggle, this);
        SkipTutoButton.LateStart(GameDatas.skipTutorial, this);

        sphereColor = SphereRoomTranslucent.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
        //UpdateTutoText();
    }

    private void Update()
    {

        Debug.Log(GameDatas.skipTutorial);
        //Debug.Log(currentTutoRow + " row");
    }

    public void StartGame()
    {
        GameDataManager.SaveGameData(GameDatas, "gameSaveFile.txt");

        //TutoCamera.gameObject.transform.GetChild(0).GetComponent<XRGrabInteractable>().enabled = false;
        TutoRedButton.SetActive(false);
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
        DigitalTimer.gameObject.SetActive(true);

        SphereRoom.SetActive(false);
        SphereRoomTranslucent.SetActive(true);
        RedButton.SetActive(false);
        RestOfRoom.SetActive(false);
        CanvaRef.SetActive(false);


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
            SphereRoomTranslucent.transform.localScale += new Vector3(1,1,1) * Time.deltaTime * 50f;
            if (elapsedTime > 1)
            {
                sphereColor.a -= 0.5f * Time.deltaTime;
                Mathf.Clamp(sphereColor.a, 0f, 1f);
                SphereRoomTranslucent.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);
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

        SphereRoomTranslucent.SetActive(false);
        Sounds.transform.GetChild(2).GetComponentInChildren<AudioSource>().volume = 0.05f;
        DigitalTimer.StartTimer(this);
    }

    public IEnumerator CloseTransition(float seconds)
    {
        SphereRoomTranslucent.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            SphereRoomTranslucent.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 50f;

                sphereColor.a += 0.5f * Time.deltaTime;
                Mathf.Clamp(sphereColor.a, 0f, 1f);
                SphereRoomTranslucent.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);

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
        SphereRoomTranslucent.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", sphereColor);
        SceneManager.LoadScene("Default_Scene");
    }

    public void UpdateTutoText()
    {
        if (GameDatas.skipTutorial)
        {
            StartGame();
        }

        if (!currentTutoRow.IsFinished)
        {
            Debug.LogWarning("je suis apellerknje");
            
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
                case 5:
                    VideosCanvas.SetActive(true);
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(true);
                    VideoPlayerControls.clip = ListOfVideoClips[0];
                    VideoPlayerExample.clip = ListOfVideoClips[1];
                    break;

                case 6:
                    ValidationSound.Play();
                    TutoBat.SetActive(true);
                    TutoRedButton.SetActive(true);
                break;

                case 7:
                    VideoPlayerExample.clip = ListOfVideoClips[2];
                    TutoRedButton.SetActive(false);
                    break;
                
                case 8:
                    VideoPlayerExample.clip = ListOfVideoClips[3];
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(false);
                    //TutoBat.SetActive(false);
                    TutoCamera.SetActive(true);
                    break;

                case 9:
                    ValidationSound.Play();
                    VideosCanvas.SetActive(true);
                    VideoPlayerControls.clip = ListOfVideoClips[4];
                    VideoPlayerExample.clip = ListOfVideoClips[5];
                    break;

                case 10:
                    ValidationSound.Play();
                    TutoBlocNote.gameObject.SetActive(true);
                    VideoPlayerControls.clip = ListOfVideoClips[6];
                    VideoPlayerExample.clip = ListOfVideoClips[7];
                    break;

                case 11:
                    ValidationSound.Play();
                    VideoPlayerControls.clip = ListOfVideoClips[0];
                    VideoPlayerExample.clip = ListOfVideoClips[8];
                    break;

                case 12:
                    ValidationSound.Play();
                    VideoPlayerControls.clip = ListOfVideoClips[0];
                    VideoPlayerExample.clip = ListOfVideoClips[9];
                    break;

                case 13:
                    ValidationSound.Play();
                    VideoPlayerControls.clip = null;
                    VideoPlayerExample.clip = null;
                    VideosCanvas.SetActive(false);
                    TutoBat.SetActive(false);
                    TutoSnail.SetActive(true);
                    RedButton.SetActive(true);
                    TutoRedButton.SetActive(false);
                    break;

                /*case 14:
                    TutoBlocNote.SetActive(true);
                    TutoSnail.SetActive(true);
                    RedButton.SetActive(true);
                    TutoRedButton.SetActive(false);
                    break;*/
            }
        }
        else
        {
            
            
            /*TutoCamera.SetActive(false);
            TutoBat.SetActive(false);
            TutoBlocNote.SetActive(false);
            TutoSnail.SetActive(false);
            TutoWalkieTalkie.SetActive(false);
            TextTuto.text = currentTutoRow.text;
            StartGame();*/
        }
    }

    /*public void CheckTutoState()
    {
        switch (currentTutoRowIndex)
        {
            case 3:
                //play sound
                UpdateTutoText();
                break;
        }
    }*/

    public void EnabledRedButton(bool value)
    {
        if (currentTutoRowIndex == 7)
        {
            TutoRedButton.gameObject.SetActive(value);
        }
    }

    public void ChangeControllersToggle(bool value)
    {
        if (value)
        {
            LeftController.selectActionTrigger = XRBaseInputInteractor.InputTriggerType.Toggle;
            RightController.selectActionTrigger = XRBaseInputInteractor.InputTriggerType.Toggle;
        }
        else
        {
            LeftController.selectActionTrigger = XRBaseInputInteractor.InputTriggerType.State;
            RightController.selectActionTrigger = XRBaseInputInteractor.InputTriggerType.State;
        }
        
    }

    public void QuitGame()
    {
        GameDataManager.SaveGameData(GameDatas, "gameSaveFile.txt");
        Application.Quit();
    }
}
