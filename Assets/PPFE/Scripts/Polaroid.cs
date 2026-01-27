using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class Polaroid : MonoBehaviour
{
    public GameObject photoPrefab = null;
    public MeshRenderer screenRenderer = null;
    public Transform spawnLocation = null;
    public PlayQuickSound playQuickSound = null;

    private Camera renderCamera = null;
    private InputDevice rightHandDevice;
    private InputDevice leftHandDevice;
    private bool deviceInitialized = false;

    [Header("Zoom Settings")]
    public float minFOV = 1f;
    public float maxFOV = 60f;
    public float zoomSpeed = 30f;

    private float currentFOV;
    private bool pictureAlreadyOut;

    private void Awake()
    {
        renderCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
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

    private void TryInitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
        if (devices.Count > 0)
        {
            rightHandDevice = devices[0];
            //Debug.Log($"Right controller found: {rightHandDevice.name}");
        }

        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, devices);
        if (devices.Count > 0)
        {
            leftHandDevice = devices[0];
            //Debug.Log($"Left controller found: {leftHandDevice.name}");
        }

        deviceInitialized = rightHandDevice.isValid || leftHandDevice.isValid;
    }

    private void CreateRenderTexture()
    {
            RenderTexture newTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
            newTexture.antiAliasing = 4;

            renderCamera.targetTexture = newTexture;
            screenRenderer.material.mainTexture = newTexture;
    }

    public void TakePhoto()
    {
        if (!pictureAlreadyOut)
        {
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
    }

    public void TurnOff()
    {
        renderCamera.enabled = false;
        screenRenderer.material.color = Color.black;
    }

    public void Zoom()
    {
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValueRight))
        {
            currentFOV -= thumbstickValueRight.y * zoomSpeed * Time.deltaTime;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            renderCamera.fieldOfView = currentFOV;
        }
        else if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValueLeft))
        {
            currentFOV -= thumbstickValueLeft.y * zoomSpeed * Time.deltaTime;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            renderCamera.fieldOfView = currentFOV;
        }
    }

    public void SetPictureAlreadyOut(bool value)
    {
        pictureAlreadyOut = value;
    }
}