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
    [SerializeField] private BlocNote blocNote;

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
    [SerializeField] private VideoPlayer VideoPlayerMiddle;
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

        GameDataManager = new GameDataManager();
        GameDatas = GameDataManager.LoadGameData("gameSaveFile.txt");
        ChangeControllersToggle(GameDatas.controllersToggle);

        
        ToggleButton.LateStart(!GameDatas.controllersToggle, this);
        SkipTutoButton.LateStart(GameDatas.skipTutorial, this);

        sphereColor = SphereRoomTranslucent.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
    }

    public void StartGame()
    {
        GameDataManager.SaveGameData(GameDatas, "gameSaveFile.txt");

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
        DigitalTimer.StopTimer();
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
        TextTuto.text = "Bravo ! Vous avez recensé " + blocNote.CheckAllPictures().ToString() + " sur 9 animaux au total en "
            + DigitalTimer.GetTimeRemaning();
        Sounds.SetActive(false);
        Polaroid.SetActive(false);
        Animals.SetActive(false);
        Accessories.SetActive(false);
        DigitalTimer.gameObject.SetActive(false);
        SphereRoom.SetActive(true);
        SphereRoomTranslucent.SetActive(false);
        RestOfRoom.SetActive(true);
        CanvaRef.SetActive(true);
        CanvaRef.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public void UpdateTutoText()
    {
        if (GameDatas.skipTutorial)
        {
            StartGame();
            return;
        }

        if (!currentTutoRow.IsFinished)
        {
            if (currentTutoRow.conditionEnabled && !GameDatas.controllersToggle)
            {
                TextTuto.text = currentTutoRow.text;
                currentTutoRowIndex = currentTutoRow.nextRowIndexByCondition;
                currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
            }
            else
            {
                TextTuto.text = currentTutoRow.text;
                currentTutoRowIndex = currentTutoRow.nextRowIndex;
                currentTutoRow = TutoHUDTextDatas.textRow[currentTutoRowIndex];
            }

            switch (currentTutoRowIndex)
            {
                case 1:
                    VideosCanvas.SetActive(true);
                    VideosCanvas.transform.GetChild(0).gameObject.SetActive(false);
                    VideosCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    VideosCanvas.transform.GetChild(2).gameObject.SetActive(true);
                    VideoPlayerMiddle.clip = ListOfVideoClips[10];

                    break;

                case 2:
                    VideosCanvas.transform.GetChild(0).gameObject.SetActive(true);
                    VideosCanvas.transform.GetChild(1).gameObject.SetActive(true);
                    VideosCanvas.transform.GetChild(2).gameObject.SetActive(false);
                    VideosCanvas.SetActive(false);
                    break;

                case 5:
                    VideosCanvas.SetActive(true);
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(true);
                    VideoPlayerControls.clip = ListOfVideoClips[0];
                    VideoPlayerExample.clip = ListOfVideoClips[1];
                    break;

                case 6:
                    ValidationSound.Play();
                    VideosCanvas.SetActive(false);
                    TutoBat.SetActive(true);
                    TutoRedButton.SetActive(true);
                break;

                case 7:
                    VideosCanvas.SetActive(true);
                    VideoPlayerExample.clip = ListOfVideoClips[2];
                    TutoRedButton.SetActive(false);
                    break;
                
                case 8:
                    VideoPlayerExample.clip = ListOfVideoClips[3];
                    TutoRedButton.gameObject.SetActive(false);
                    TutoWalkieTalkie.SetActive(false);
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

                case 15:
                    ValidationSound.Play();
                    VideosCanvas.SetActive(false);
                    TutoBat.SetActive(true);
                    TutoRedButton.SetActive(true);
                    break;
            }
        }
    }

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

    public void RestartLevel()
    {
        SceneManager.LoadScene("Default_Scene");
    }
}
