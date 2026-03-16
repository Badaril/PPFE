using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class Polaroid : MonoBehaviour
{
    public GameObject photoPrefab = null;
    public MeshRenderer screenRenderer = null;
    public MeshRenderer bigScreenRenderer = null;
    public Transform spawnLocation = null;
    public PlayQuickSound playQuickSound = null;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Camera renderCamera = null;
    private InputDevice rightHandDevice;
    private InputDevice leftHandDevice;
    private bool deviceInitialized = false;

    [Header("Zoom Settings")]
    public float minFOV = 1f;
    public float maxFOV = 60f;
    public float zoomSpeed = 30f;

    private float currentFOV = 20f;
    private bool pictureAlreadyOut;

    [Header("Raycast Origins")]
    public GameObject[] RaycastOrigins;
    private TypeOfAnimal animaltype = TypeOfAnimal.None;

    private void Awake()
    {
        renderCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
        CreateRenderTexture();
        TurnOff();
    }

    private void Update()
    {
        if (!deviceInitialized)
        {
            TryInitializeControllers();
        }

        if (renderCamera.enabled)
        {
            Zoom();
        }
    }

    public void ResetPostion()
    {
        this.gameObject.transform.SetPositionAndRotation(startPosition, startRotation);
    }

    private void TryInitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
        if (devices.Count > 0)
        {
            rightHandDevice = devices[0];
        }

        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, devices);
        if (devices.Count > 0)
        {
            leftHandDevice = devices[0];
        }

        deviceInitialized = rightHandDevice.isValid || leftHandDevice.isValid;
    }

    private void CreateRenderTexture()
    {
        RenderTexture newTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
        newTexture.antiAliasing = 4;

        renderCamera.targetTexture = newTexture;
        screenRenderer.material.mainTexture = newTexture;
        bigScreenRenderer.material.mainTexture = newTexture;
    }

    public void TakePhoto()
    {
        if (!pictureAlreadyOut)
        {
            List<RaycastHit> listOfHit = new List<RaycastHit>();
            List<RaycastHit> listOfHitAnimals = new List<RaycastHit>();
            for (int i = 0; i < RaycastOrigins.Length; i++)
            {
                Ray ray = new Ray(RaycastOrigins[i].transform.position, transform.forward);
                Physics.Raycast(ray, out RaycastHit hit, 500f);
                listOfHit.Add(hit);
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<AnimalType>() != null)
                    {
                        listOfHitAnimals.Add(hit);
                    }
                }
            }

            if (listOfHitAnimals.Count >= listOfHit.Count / 2) 
            {
                animaltype = listOfHitAnimals[0].collider.GetComponent<AnimalType>().type;
                Debug.Log(animaltype.ToString());
            }
            
            playQuickSound.Play();
            Photo newPhoto = CreatePhoto();
            newPhoto.polaroid = this;
            SetPhotoImage(newPhoto);
            pictureAlreadyOut = true;
        }
    }

    private Photo CreatePhoto()
    {
        GameObject photoObject = Instantiate(photoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
        photoObject.GetComponent<Photo>().animalInPicture = animaltype;
        return photoObject.GetComponent<Photo>();
    }

    private void SetPhotoImage(Photo photo)
    {
        Texture2D newTexture = RenderCameraToTexture(renderCamera);
        photo.SetImage(newTexture);
    }

    private Texture2D RenderCameraToTexture(Camera camera)
    {
        camera.Render();
        RenderTexture.active = camera.targetTexture;

        Texture2D photo = new Texture2D(256, 256, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        photo.Apply();

        return photo;
    }

    public void TurnOn()
    {
        renderCamera.enabled = true;
        screenRenderer.material.color = Color.white;
        bigScreenRenderer.material.color = Color.white;
    }

    public void TurnOff()
    {
        renderCamera.enabled = false;
        screenRenderer.material.color = Color.black;
        bigScreenRenderer.material.color = Color.black;
    }

    public void Zoom()
    {
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValueRight))
        {
            currentFOV -= thumbstickValueRight.y * zoomSpeed * Time.deltaTime;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            renderCamera.fieldOfView = currentFOV;
        }
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValueLeft))
        {
            currentFOV -= thumbstickValueLeft.y * zoomSpeed * Time.deltaTime;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            renderCamera.fieldOfView = currentFOV;
        }
    }

    public void SetPictureAlreadyOut(bool value)
    {
        pictureAlreadyOut = value;
        if (!pictureAlreadyOut) 
        {
            animaltype = TypeOfAnimal.None;
        }
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
}